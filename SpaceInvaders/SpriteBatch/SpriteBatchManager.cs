using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SpriteBatchManager : Manager
    {
        // data:
        private static SpriteBatchManager instance = null;
        private SpriteBatch referenceNode;
        public static  Boolean shouldDrawBoxes = false;

        private SpriteBatchManager(int reserveNum = 3, int reserveGrow = 1)
            : base(reserveNum, reserveGrow)
        {
            this.referenceNode = (SpriteBatch)this.CreateNode();
        }

        protected override MLink CreateNode()
        {
            MLink node = new SpriteBatch();
            return node;
        }

        public static void Create(int reserveNum = 3, int reserveGrow = 1)
        {
            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);
            Debug.Assert(instance == null);

            if (instance == null)
            {
                instance = new SpriteBatchManager(reserveNum, reserveGrow);
            }
        }

        public static void Destroy()
        {
            SpriteBatchManager inst = SpriteBatchManager.getInstance();
            inst.baseDestroy();
        }


        public static SpriteBatch Add(SpriteBatch.Name sbName, int reserveNum = 3, int reserveGrow = 1)
        {
            SpriteBatchManager inst = SpriteBatchManager.getInstance();
            SpriteBatch node = (SpriteBatch)inst.baseAdd();
            Debug.Assert(node != null);
            node.Set(sbName, reserveNum, reserveGrow);
            return node;
        }

        public static void Remove(SpriteBatch node)
        {
            Debug.Assert(node != null);
            SpriteBatchManager inst = SpriteBatchManager.getInstance();
            inst.baseRemove(node);
        }

        public static void Remove(SpriteBatchNode spriteBatchNodeArg)
        {
            Debug.Assert(spriteBatchNodeArg != null);
            SpriteBatch batch = spriteBatchNodeArg.GetSpriteBatch();

            Debug.Assert(batch != null);
            batch.Remove(spriteBatchNodeArg);
        }

        public static SpriteBatch Find(SpriteBatch.Name sbName)
        {
            SpriteBatchManager inst = SpriteBatchManager.getInstance();
            inst.referenceNode.name = sbName;
            SpriteBatch data = (SpriteBatch)inst.baseFind(inst.referenceNode);
            return data;
        }

        public static void Draw()
        {
            SpriteBatchManager inst = SpriteBatchManager.getInstance();
            SpriteBatch sb = (SpriteBatch)inst.active;

            while (sb != null)
            {
                if (shouldDrawBoxes == false && sb.name == SpriteBatch.Name.Boxes)
                {
                    sb = (SpriteBatch)sb.next;
                }
                else
                {
                    sb.Draw();
                    sb = (SpriteBatch)sb.next;
                }
            }
        }

        public static void Dump()
        {
            SpriteBatchManager inst = SpriteBatchManager.getInstance();
            inst.baseDump("SpriteBatchManager");
        }

        protected override bool Compare(MLink first, MLink second)
        {
            Debug.Assert(first != null);
            Debug.Assert(second != null);
            SpriteBatch firstSB = (SpriteBatch)first;
            SpriteBatch secondSB = (SpriteBatch)second;

            if (firstSB.name == secondSB.name)
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
            SpriteBatch node = (SpriteBatch)link;
            node.Dump();
        }

        // private

        private static SpriteBatchManager getInstance()
        {
            Debug.Assert(instance != null);
            return instance;
        }
    }
}
