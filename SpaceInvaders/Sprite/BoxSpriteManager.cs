using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class BoxSpriteManager : Manager
    {
        // data:
        private static BoxSpriteManager instance = null;
        private BoxSprite referenceNode;

        private BoxSpriteManager(int reserveNum = 3, int reserveGrow = 1)
            : base(reserveNum, reserveGrow)
        {
            this.referenceNode = (BoxSprite)this.CreateNode();
        }

        protected override MLink CreateNode()
        {
            MLink node = new BoxSprite();
            return node;
        }

        public static void Create(int reserveNum = 3, int reserveGrow = 1)
        {
            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);
            Debug.Assert(instance == null);

            if (instance == null)
            {
                instance = new BoxSpriteManager(reserveNum, reserveGrow);
            }
        }

        public static void Destroy()
        {
            BoxSpriteManager inst = BoxSpriteManager.getInstance();
            inst.baseDestroy();
        }


        public static BoxSprite Add(BoxSprite.Name name, float x, float y, float width, float height)
        {
            BoxSpriteManager inst = BoxSpriteManager.getInstance();
            BoxSprite node = (BoxSprite)inst.baseAdd();
            Debug.Assert(node != null);
            node.Set(name, x, y, width, height);
            return node;
        }

        public static BoxSprite Add(BoxSprite.Name nameArg, Azul.Rect rectArg)
        {
            Debug.Assert(rectArg != null);
            BoxSpriteManager inst = BoxSpriteManager.getInstance();
            BoxSprite node = (BoxSprite)inst.baseAdd();
            Debug.Assert(node != null);
            node.Set(nameArg, rectArg.x, rectArg.y, rectArg.width, rectArg.height);
            return node;
        }

        public static void Remove(BoxSprite node)
        {
            Debug.Assert(node != null);
            BoxSpriteManager inst = BoxSpriteManager.getInstance();
            inst.baseRemove(node);
        }

        public static BoxSprite Find(BoxSprite.Name name)
        {
            BoxSpriteManager inst = BoxSpriteManager.getInstance();
            inst.referenceNode.name = name;
            BoxSprite data = (BoxSprite)inst.baseFind(inst.referenceNode);
            return data;
        }

        public static void Dump()
        {
            BoxSpriteManager inst = BoxSpriteManager.getInstance();
            inst.baseDump("BoxSpriteManager");
        }

        protected override bool Compare(MLink first, MLink second)
        {
            Debug.Assert(first != null);
            Debug.Assert(second != null);
            BoxSprite firstBox = (BoxSprite)first;
            BoxSprite secondBox = (BoxSprite)second;

            if (firstBox.name == secondBox.name)
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
            BoxSprite node = (BoxSprite)link;
            node.Dump();
        }

        // private

        private static BoxSpriteManager getInstance()
        {
            Debug.Assert(instance != null);
            return instance;
        }
    }
}
