using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class Manager
    {
        // data:
        protected MLink active;
        private MLink reserve;
        public int numOfActiveNodes;
        public int numOfReserveNodes;
        public int totalNumNodes;
        public int maxNumActiveNodes;
        public int reserveGrowBy;
        public int reserveNum;

        protected Manager(int reserveNum = 3, int reserveGrow = 1)
        {
            Debug.Assert(reserveNum > 0); // nobody should be passing this 0
            Debug.Assert(reserveGrow > 0); // nobody should be passing this 0

            this.reserveGrowBy = reserveGrow;
            this.reserveNum = reserveNum;
            this.numOfActiveNodes = 0;
            this.numOfReserveNodes = 0;
            this.totalNumNodes = 0;
            this.maxNumActiveNodes = 0;
            this.active = null;
            this.reserve = null;
            this.fillTheReservePool(reserveNum);
        }

        // base methods

        protected MLink baseAdd()
        {
            // check if there are nodes on the reserve list
            if (this.reserve == null)
            {
                this.fillTheReservePool(reserveGrowBy);
            }

            // take from the reserve
            MLink node = this.pullFromReserve();
            Debug.Assert(node != null);
            node.Clear();
            this.addToActive(node);
            return node;
        }

        protected MLink basePop()
        {
            // check reserve list
            if (reserve == null)
            {
                fillTheReservePool(reserveGrowBy);
            }

            MLink node = this.pullFromReserve();
            Debug.Assert(node != null);
            node.Clear();
            return node;
        }

        protected MLink pullAndReturn()
        {
            // check if there are nodes on the reserve list
            if (this.reserve == null)
            {
                this.fillTheReservePool(reserveGrowBy);
            }

            // take from the reserve
            MLink nodeToUse = this.pullFromReserve();
            Debug.Assert(nodeToUse != null);
            nodeToUse.Clear();
            nodeToUse.status = MLink.Status.Active;
            this.incrementCurrentNumActive();
            return nodeToUse;
        }

        protected void baseInsertSorted(TimerEvent e)
        {
            //Debug.Assert(node != null);
            //Debug.Assert(node.status == MLink.Status.Uninitialized);
            //this.addToFront(ref active, node);
            //node.status = MLink.Status.Active;
            //numOfActiveNodes++;

            Sort(e);
            
        }

        protected void baseDestroy()
        {
            MLink node;
            MLink tmp;

            node = this.active;

            while (node != null)
            {
                // walk list
                tmp = node;
                node = node.next;

                // node to clean
                this.removeFromActive(tmp);
                tmp = null;
                totalNumNodes--;
            }

            node = this.reserve;

            while (node != null)
            {
                tmp = node;
                node = node.next;

                //node to clean
                this.removeFromReserve(tmp);
                tmp = null;
                totalNumNodes--;
            }
        }

        virtual protected Boolean Sort(TimerEvent nodeToAdd)
        {
            return false;
        }

        protected void baseRemove(MLink node)
        {
            Debug.Assert(node != null);
            this.removeFromActive(node);
            this.addToReserve(node);
        }

        protected MLink baseFind(MLink node)
        {
            MLink tmp = this.active;

            while (tmp != null)
            {
                if (Compare(node, tmp))
                {
                    break;
                }
                tmp = tmp.next;
            }

            return tmp;
        }

        protected void baseDump(string message)
        {
            Debug.WriteLine("");
            Debug.WriteLine("********* Manager Dump ****************************************", message);
            DumpNodes();
            DumpStats();
        }

        protected void baseDumpStats(string message)
        {
            Debug.WriteLine("*********** Stats **************", message);
            Debug.WriteLine("     currNumActiveNodes: " + numOfActiveNodes);
            Debug.WriteLine("    currNumReserveNodes: " + numOfReserveNodes);
            Debug.WriteLine("          totalNumNodes: " + totalNumNodes);
            Debug.WriteLine("      maxNumActiveNodes: " + maxNumActiveNodes);
            Debug.WriteLine("            reserveGrow: " + reserveGrowBy);
            Debug.WriteLine("             reserveNum: " + reserveNum);
        }

        // abstract methods

        abstract protected MLink CreateNode();
        abstract protected void DumpNode(MLink link);
        abstract protected Boolean Compare(MLink first, MLink second);

        // private methods

        private void fillTheReservePool(int numNodesToAdd)
        {
            Debug.Assert(numNodesToAdd > 0);  // why add 0 or less?
            totalNumNodes = totalNumNodes + numNodesToAdd; // update total

            for (int i = 0; i < numNodesToAdd; i++)
            {
                MLink node = this.CreateNode();  // CreateNode is implemented in concrete class
                Debug.Assert(node != null);
                Debug.Assert(node.status == MLink.Status.Uninitialized);
                this.addNodeToReserve(node);
            }
        }

        private void addNodeToReserve(MLink node)
        {
            Debug.Assert(node != null);

            addToFront(ref this.reserve, node);
            node.status = MLink.Status.Reserve;
            numOfReserveNodes++;
        }

        private void addToFront(ref MLink head, MLink node)
        {
            Debug.Assert(node != null);

            if (head == null)
            {
                // push to front
                head = node;
                node.next = null;
                node.prev = null;
            }
            else
            {
                // push to front
                node.prev = null;
                node.next = head;
                head.prev = node;
                head = node;
            }
        }

        private MLink pullFromReserve()
        {
            Debug.Assert(reserve != null);

            MLink node = pullFromFront(ref reserve);

            Debug.Assert(node != null);
            Debug.Assert(node.status == MLink.Status.Reserve);

            node.status = MLink.Status.Uninitialized;
            numOfReserveNodes--;
            return node;
        }

        private MLink pullFromFront(ref MLink head)
        {
            Debug.Assert(head != null);

            MLink node = head;

            // update to new head
            head = head.next;
            if (head != null)
            {
                head.prev = null;
            }

            node.next = null;
            node.prev = null;

            return node;
        }

        private void addToActive(MLink node)
        {
            Debug.Assert(node != null);
            Debug.Assert(node.status == MLink.Status.Uninitialized);

            addToFront(ref active, node);
            node.status = MLink.Status.Active;
            numOfActiveNodes++;
        }

        private void removeFromActive(MLink node)
        {
            Debug.Assert(node != null);

            this.removeNode(ref active, node);
            node.status = MLink.Status.Uninitialized;
            this.numOfActiveNodes--;
        }

        private void addToReserve(MLink node)
        {
            Debug.Assert(node != null);

            addToFront(ref reserve, node);
            node.status = MLink.Status.Reserve;
            numOfReserveNodes++;
        }

        private void removeNode(ref MLink head, MLink node)
        {
            if (node.prev != null)
            {
                node.prev.next = node.next;
            }
            else
            {
                head = node.next;
            }

            if (node.next != null)
            {
                node.next.prev = node.prev;
            }
        }

        private void removeFromReserve(MLink node)
        {
            Debug.Assert(node != null);

            removeNode(ref reserve, node);
            node.status = MLink.Status.Uninitialized;
            numOfReserveNodes--;
        }

        private void incrementCurrentNumActive()
        {
            numOfActiveNodes++;
            if (this.numOfActiveNodes > this.maxNumActiveNodes)
            {
                this.maxNumActiveNodes = this.numOfActiveNodes;
            } 
        }

        // printing methods

        private void DumpNodes()
        {
            Debug.WriteLine("");
            Debug.WriteLine("\t------ Active List: ---------------------------\n");


            MLink pNode = this.active;
            if (pNode == null)
            {
                Debug.WriteLine("\t\t<empty>");
            }

            int i = 0;
            while (pNode != null)
            {
                Debug.WriteLine("\t{0}: -----------------", i);
                DumpNode(pNode);
                i++;
                pNode = pNode.next;
            }

            Debug.WriteLine("");
            Debug.WriteLine("\t------ Reserve List: ---------------------------\n");

            pNode = this.reserve;
            if (pNode == null)
            {
                Debug.WriteLine("\t\t<empty>");
            }
            i = 0;
            while (pNode != null)
            {
                Debug.WriteLine("\t{0}: -----------------", i);
                DumpNode(pNode);
                i++;
                pNode = pNode.next;
            }
        }

        private void DumpStats()
        {
            Debug.WriteLine("");
            Debug.WriteLine("\t------ Stats: ---------------------------\n");

            Debug.WriteLine("\t     total: {0}", totalNumNodes);
            Debug.WriteLine("\t    active: {0}", numOfActiveNodes);
            Debug.WriteLine("\t   reserve: {0}", numOfReserveNodes);
        }
    }
}
