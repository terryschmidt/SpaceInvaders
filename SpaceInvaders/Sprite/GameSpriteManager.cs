using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class GameSpriteManager : Manager
    {
        // data:
        private static GameSpriteManager instance = null;
        private GameSprite referenceNode;

        private GameSpriteManager(int reserveNum = 3, int reserveGrow = 1)
            : base(reserveNum, reserveGrow)
        {
            this.referenceNode = (GameSprite)this.CreateNode();
        }

        public static void Create(int reserveNum = 3, int reserveGrow = 1)
        {
            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);
            Debug.Assert(instance == null);
            if (instance == null)
            {
                instance = new GameSpriteManager(reserveNum, reserveGrow);
                // add null sprite to allow find
                GameSprite pGSprite = GameSpriteManager.Add(GameSprite.Name.NullObject, Image.Name.NullObject, 0, 0, 0, 0);
                Debug.Assert(pGSprite != null);
            }
        }

        public static void Destroy()
        {
            GameSpriteManager inst = GameSpriteManager.getInstance();
            inst.baseDestroy();
        }


        public static GameSprite Add(GameSprite.Name name, Image.Name iName, float x, float y, float width, float height)
        {
            GameSpriteManager inst = GameSpriteManager.getInstance();
            GameSprite node = (GameSprite)inst.baseAdd();
            Debug.Assert(node != null);
            node.Set(name, iName, x, y, width, height);
            return node;
        }

        public static void Remove(GameSprite node)
        {
            Debug.Assert(node != null);
            GameSpriteManager inst = GameSpriteManager.getInstance();
            inst.baseRemove(node);
        }

        public static GameSprite Find(GameSprite.Name nameToFind)
        {
            GameSpriteManager inst = GameSpriteManager.getInstance();
            inst.referenceNode.name = nameToFind;
            GameSprite data = (GameSprite)inst.baseFind(inst.referenceNode);
            return data;
        }

        public static void Dump()
        {
            GameSpriteManager inst = GameSpriteManager.getInstance();
            inst.baseDump("GameSpriteManager");
        }

        protected override bool Compare(MLink first, MLink second)
        {
            Debug.Assert(first != null);
            Debug.Assert(second != null);
            GameSprite firstSprite = (GameSprite)first;
            GameSprite secondSprite = (GameSprite)second;

            if (firstSprite.name == secondSprite.name)
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
            MLink node = new GameSprite();
            return node;
        }

        protected override void DumpNode(MLink link)
        {
            Debug.Assert(link != null);
            GameSprite node = (GameSprite)link;
            node.Dump();
        }

        private static GameSpriteManager getInstance()
        {
            Debug.Assert(instance != null);
            return instance;
        }
    }
}
