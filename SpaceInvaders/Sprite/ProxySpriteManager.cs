using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ProxySpriteManager : Manager
    {
        // data:
        private static ProxySpriteManager instance = null;
        private ProxySprite referenceNode;

        private ProxySpriteManager(int reserveNum = 3, int reserveGrow = 1)
            : base(reserveNum, reserveGrow)
        {
            this.referenceNode = (ProxySprite)this.CreateNode();
        }

        ~ProxySpriteManager()
        {
            this.referenceNode = null;
            ProxySpriteManager.instance = null;
        }

        public static void Destroy()
        {
            ProxySpriteManager inst = ProxySpriteManager.getInstance();
            inst.baseDestroy();
        }

        public static void Create(int reserveNum = 3, int reserveGrow = 1)
        {
            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);
            Debug.Assert(instance == null);

            if (instance == null)
            {
                instance = new ProxySpriteManager(reserveNum, reserveGrow);
            }
        }

        public static ProxySprite Add(GameSprite.Name nameArg)
        {
            ProxySpriteManager inst = ProxySpriteManager.getInstance();
            ProxySprite node = (ProxySprite)inst.baseAdd();
            Debug.Assert(node != null);
            node.Set(nameArg);
            return node;
        }

        public static void Remove(ProxySprite nodeArg)
        {
            Debug.Assert(nodeArg != null);
            ProxySpriteManager inst = ProxySpriteManager.getInstance();
            inst.baseRemove(nodeArg);
        }

        public static ProxySprite Find(ProxySprite.Name nameArg)
        {
            ProxySpriteManager inst = ProxySpriteManager.getInstance();
            inst.referenceNode.name = nameArg;
            ProxySprite data = (ProxySprite)inst.baseFind(inst.referenceNode);
            return data;
        }

        public static void Dump()
        {
            ProxySpriteManager inst = ProxySpriteManager.getInstance();
            inst.baseDump("ProxySpriteManager");
        }

        public static void DumpStats()
        {
            ProxySpriteManager inst = ProxySpriteManager.getInstance();
            inst.baseDumpStats("ProxySpriteManager");
        }

        protected override bool Compare(MLink first, MLink second)
        {
            Debug.Assert(first != null);
            Debug.Assert(second != null);

            ProxySprite firstSp = (ProxySprite)first;
            ProxySprite secondSp = (ProxySprite)second;

            if (firstSp.name == secondSp.name)
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
            MLink node = new ProxySprite();
            return node;
        }

        protected override void DumpNode(MLink link)
        {
            Debug.Assert(link != null);
            ProxySprite node = (ProxySprite)link;
            node.Dump();
        }

        // private

        private static ProxySpriteManager getInstance()
        {
            Debug.Assert(instance != null);
            return instance;
        }
    }
}
