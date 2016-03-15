using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class CollisionPairManager : Manager
    {
        // data:
        private static CollisionPairManager instance = null;
        private CollisionPair referenceNode;
        private CollisionPair activePair;

        private CollisionPairManager(int numReserve, int reserveGrow)
            : base(numReserve, reserveGrow)
        {
            this.referenceNode = (CollisionPair)this.CreateNode();
            this.activePair = null;
        }

        public static void Create(int reserveNum = 3, int reserveGrow = 1)
        {
            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);
            Debug.Assert(instance == null);

            if (instance == null)
            {
                instance = new CollisionPairManager(reserveNum, reserveGrow);
            }
        }

        public static void Destroy()
        {
            CollisionPairManager inst = CollisionPairManager.getInstance();
            inst.baseDestroy();
        }

        ~CollisionPairManager()
        {
            CollisionPairManager.instance = null;
        }

        public static CollisionPair Add(CollisionPair.Name nameArg, GameObject treeRootA, GameObject treeRootB)
        {
            return CollisionPairManager.Add(nameArg, 0, treeRootA, treeRootB);
        }

        public static CollisionPair Add(CollisionPair.Name nameArg, int indexArg, GameObject treeRootA, GameObject treeRootB)
        {
            CollisionPairManager inst = CollisionPairManager.getInstance();
            CollisionPair cp = (CollisionPair)inst.baseAdd();
            Debug.Assert(cp != null);
            cp.Set(nameArg, indexArg, treeRootA, treeRootB);
            return cp;
        }

        public static CollisionPair Find(CollisionPair.Name nameArg, int indexArg = 0)
        {
            CollisionPairManager inst = CollisionPairManager.getInstance();
            inst.referenceNode.name = nameArg;
            CollisionPair data = (CollisionPair)inst.baseFind(inst.referenceNode);
            return data;
        }

        public static void Remove(CollisionPair nodeArg)
        {
            Debug.Assert(nodeArg != null);
            CollisionPairManager inst = CollisionPairManager.getInstance();
            inst.baseRemove(nodeArg);
        }

        public static void Process()
        {
            CollisionPairManager inst = CollisionPairManager.getInstance();
            CollisionPair cp = (CollisionPair)inst.active;

            while (cp != null)
            {
                inst.activePair = cp;
                cp.Process(); // check for a single pair
                cp = (CollisionPair)cp.next; // advance to next
            }
        }

        public static CollisionPair GetActivePair()
        {
            CollisionPairManager inst = CollisionPairManager.getInstance();
            return inst.activePair;
        }

        protected override bool Compare(MLink first, MLink second)
        {
            Debug.Assert(first != null);
            Debug.Assert(second != null);
            Image firstData = (Image)first;
            Image secondData = (Image)second;

            if (firstData.name == secondData.name)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override void DumpNode(MLink link)
        {
            Debug.Assert(link != null);
            Image node = (Image)link;
            Debug.Assert(node != null);
            node.Dump();
        }

        protected override MLink CreateNode()
        {
            MLink node = new CollisionPair();
            Debug.Assert(node != null);
            return node;
        }

        // private

        private static CollisionPairManager getInstance()
        {
            Debug.Assert(instance != null);
            return instance;
        }
    }
}
