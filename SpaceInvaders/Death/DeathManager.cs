using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class DeathManager : Manager
    {
        // data:
        private static DeathManager instance = null;
        private DeathNode referenceNode;

        private DeathManager(int reserveNum = 3, int reserveGrow = 1)
            : base(reserveNum, reserveGrow)
        {
            this.referenceNode = (DeathNode)this.CreateNode();
        }

        ~DeathManager()
        {
            this.referenceNode = null;
            DeathManager.instance = null;
        }

        public static void Destroy()
        {
            DeathManager inst = DeathManager.getInstance();
            inst.baseDestroy();
        }

        public static void Create(int reserveNum = 3, int reserveGrow = 1)
        {
            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);
            Debug.Assert(instance == null);

            if (instance == null)
            {
                instance = new DeathManager(reserveNum, reserveGrow);
            }
        }

        public static DeathNode Attach(object objArg)
        {
            DeathManager inst = DeathManager.getInstance();
            DeathNode dNode = (DeathNode)inst.baseAdd();
            Debug.Assert(dNode != null);
            Debug.Assert(objArg != null);
            dNode.Set(objArg);
            return dNode;
        }

        public static void Dump()
        {
            DeathManager inst = DeathManager.getInstance();
            inst.baseDump("DeathManager");
        }

        public static void DumpStats()
        {
            DeathManager inst = DeathManager.getInstance();
            inst.baseDumpStats("DeathManager");
        }

        protected override bool Compare(MLink first, MLink second)
        {
            Debug.Assert(first != null);
            Debug.Assert(second != null);

            DeathNode deathFirst = (DeathNode)first;
            DeathNode deathSecond = (DeathNode)second;

            if (deathFirst.obj == deathSecond.obj)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override MLink CreateNode()
        {
            MLink node = new DeathNode();
            Debug.Assert(node != null);
            return node;
        }

        protected override void DumpNode(MLink link)
        {
            Debug.Assert(link != null);
            DeathNode node = (DeathNode)link;
            Debug.Assert(node != null);
            node.Dump();
        }

        // private

        private static DeathManager getInstance()
        {
            Debug.Assert(instance != null);
            return instance;
        }
    }
}
