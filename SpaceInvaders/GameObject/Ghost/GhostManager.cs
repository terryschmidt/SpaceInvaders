using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class GhostManager : Manager
    {
        // data:
        private static GhostManager instance = null;
        private GhostNode referenceNode;

        private GhostManager(int reserveNum = 3, int reserveGrow = 1)
            : base(reserveNum, reserveGrow)
        {
            this.referenceNode = (GhostNode)this.CreateNode();
            Debug.Assert(this.referenceNode != null);
            this.referenceNode.gameObject = new NullGameObject();
            Debug.Assert(this.referenceNode.gameObject != null);
        }

        ~GhostManager()
        {
            this.referenceNode = null;
            GhostManager.instance = null;
        }

        public static void Create(int reserveNum = 3, int reserveGrow = 1)
        {
            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);
            Debug.Assert(instance == null);

            if (instance == null)
            {
                instance = new GhostManager(reserveNum, reserveGrow);
            }
        }

        public static void Destroy()
        {
            GhostManager inst = GhostManager.getInstance();
            inst.baseDestroy();
        }


        public static GhostNode Add(GameObject pGameObj)
        {
            GhostManager inst = GhostManager.getInstance();

            GhostNode node = (GhostNode)inst.baseAdd();
            Debug.Assert(node != null);

            node.Set(pGameObj);
            return node;
        }

        public static void Remove(GameObject node)
        {
            Debug.Assert(node != null);
            GhostManager inst = GhostManager.getInstance();

            inst.referenceNode.gameObject.name = node.name;
            GhostNode data = (GhostNode)inst.baseFind(inst.referenceNode);

            data.gameObject = null;
            inst.baseRemove(data);
        }

        public static GameObject Find(GameObject.Name nameArg)
        {
            GhostManager inst = GhostManager.getInstance();

            inst.referenceNode.gameObject.name = nameArg;

            GhostNode data = (GhostNode)inst.baseFind(inst.referenceNode);
            Debug.Assert(data != null);
            return data.gameObject;
        }

        public static void Dump()
        {
            GhostManager inst = GhostManager.getInstance();
            inst.baseDump("GhostManager");
        }

        public static void DumpStats()
        {
            GhostManager inst = GhostManager.getInstance();
            inst.baseDumpStats("GhostManager");
        }

        protected override bool Compare(MLink first, MLink second)
        {
            Debug.Assert(first != null);
            Debug.Assert(second != null);

            GhostNode firstData = (GhostNode)first;
            Debug.Assert(firstData != null);
            GhostNode secondData = (GhostNode)second;
            Debug.Assert(secondData != null);

            if (firstData.gameObject.name == secondData.gameObject.name)
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
            MLink node = new GhostNode();
            Debug.Assert(node != null);
            return node;
        }

        protected override void DumpNode(MLink link)
        {
            Debug.Assert(link != null);
            GhostNode node = (GhostNode)link;

            Debug.Assert(node != null);
            node.Dump();
        }

        // private

        private static GhostManager getInstance()
        {
            Debug.Assert(instance != null);
            return instance;
        }

    }
}
