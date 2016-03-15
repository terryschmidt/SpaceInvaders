using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class Container : MLink
    {
        // data:
        public CLink active;
        public CLink reserve;
        public int numOfActiveNodes;
        public int numOfReserveNodes;
        public int totalNumNodes;
        public int maxNumActiveNodes;
        public int reserveGrowBy;
        public int reserveNum;

        // constructor

        protected Container(int reserveNum = 3, int reserveGrow = 1)
            : base()
        {
            Debug.Assert(reserveNum >= 0);
            Debug.Assert(reserveGrow >= 0);

            reserveGrowBy = reserveGrow;
            this.reserveNum = reserveNum;
            numOfActiveNodes = 0;
            numOfReserveNodes = 0;
            totalNumNodes = 0;
            maxNumActiveNodes = 0;

            active = null;
            reserve = null;

            fillTheReservePool(reserveNum);
        }

        protected void baseSetReserve(int reserve, int grow)
        {
            reserveGrowBy = grow;
            if (reserve > this.reserveNum)
            {
                fillTheReservePool(reserve - this.reserveNum);
            }
        }

        protected CLink baseAdd()
        {
            // any nodes on the reserve?

            if (this.reserve == null)
            {
                fillTheReservePool(reserveGrowBy);
            }

            CLink node = pullFromReserve();

            Debug.Assert(node != null);

            node.Clear();

            addToActive(node);
            return node;
        }

        protected void baseRemove(CLink node)
        {
            Debug.Assert(node != null);
            removeFromActive(node);
            addToReserve(node);
        }

        protected CLink baseFind(CLink node)
        {
            CLink tmp = active;

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

        protected void baseDump()
        {
            Debug.WriteLine("");
            Debug.WriteLine("********* Manager Dump ****************************************");
            this.DumpNodes();
            this.DumpStats();
        }

        // abstract methods

        abstract protected CLink CreateNode();
        abstract protected void DumpNode(CLink node);
        abstract protected Boolean Compare(CLink first, CLink second);

        // private

        private void fillTheReservePool(int numNodes)
        {
            Debug.Assert(numNodes >= 0);

            totalNumNodes = totalNumNodes + numNodes;

            for (int i = 0; i < numNodes; i++)
            {
                CLink node = this.CreateNode();
                Debug.Assert(node != null);
                Debug.Assert(node.status == CLink.Status.Uninitialized);
                addToReserve(node);
            }
        }

        private void removeFromActive(CLink node)
        {
            Debug.Assert(node != null);
            //Debug.Assert(node.status == CLink.Status.Active);
            removeNode(ref active, node);
            node.status = CLink.Status.Uninitialized;
            numOfActiveNodes--;
        }

        private void addToActive(CLink node)
        {
            Debug.Assert(node != null);
            Debug.Assert(node.status == CLink.Status.Uninitialized);
            addToFront(ref active, node);
            node.status = CLink.Status.Active;
            numOfActiveNodes++;
        }

        private void removeFromReserve(CLink node)
        {
            Debug.Assert(node != null);
            Debug.Assert(node.status == CLink.Status.Reserve);
            removeNode(ref reserve, node);
            node.status = CLink.Status.Uninitialized;
            numOfReserveNodes--;
        }

        private void addToReserve(CLink node)
        {
            Debug.Assert(node != null);
            Debug.Assert(node.status == CLink.Status.Uninitialized);
            addToFront(ref reserve, node);
            node.status = CLink.Status.Reserve;
            numOfReserveNodes++;
        }

        private CLink pullFromReserve()
        {
            Debug.Assert(reserve != null);
            CLink node = pullFromFront(ref reserve);
            Debug.Assert(node != null);
            node.status = CLink.Status.Uninitialized;
            numOfReserveNodes--;
            return node;
        }

        private void addToFront(ref CLink head, CLink node)
        {
            Debug.Assert(node != null);

            if (head == null)
            {
                head = node;
                node.next = null;
                node.prev = null;
            }
            else
            {
                node.prev = null;
                node.next = head;
                head.prev = node;
                head = node;
            }
        }

        private CLink pullFromFront(ref CLink head)
        {
            Debug.Assert(head != null);

            CLink node = head;

            head = head.next;
            if (head != null)
            {
                head.prev = null;
            }

            node.next = null;
            node.prev = null;

            return node;
        }

        private void removeNode(ref CLink head, CLink node)
        {
            Debug.Assert(node != null);

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

        // print functions

        private void DumpNodes()
        {
            Debug.WriteLine("");
            Debug.WriteLine("\t------ Active List: ---------------------------\n");


            CLink pNode = this.active;
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

            Debug.WriteLine("\t     total: {0}", this.totalNumNodes);
            Debug.WriteLine("\t    active: {0}", this.numOfActiveNodes);
            Debug.WriteLine("\t   reserve: {0}", this.numOfReserveNodes);
        }
    }
}
