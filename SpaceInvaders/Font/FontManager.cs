using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class FontManager : Manager
    {
        // data:
        private static FontManager instance = null;
        private Font referenceNode;

        private FontManager(int reserveNum = 3, int reserveGrow = 1)
            : base(reserveNum, reserveGrow)
        {
            this.referenceNode = (Font)this.CreateNode();
        }

        ~FontManager()
        {
            this.referenceNode = null;
            FontManager.instance = null;
        }

        public static void Create(int reserveNum = 3, int reserveGrow = 1)
        {
            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);
            Debug.Assert(instance == null);

            if (instance == null)
            {
                instance = new FontManager(reserveNum, reserveGrow);
            }
        }

        public static void Destroy()
        {
            FontManager inst = FontManager.getInstance();
            inst.baseDestroy();
        }

        public static Font Add(Font.Name fontNameArg, SpriteBatch.Name batchNameArg, String messageArg, Character.Name charNameArg, float xStart, float yStart)
        {
            FontManager inst = FontManager.getInstance();
            Font node = (Font)inst.baseAdd();
            Debug.Assert(node != null);
            node.Set(fontNameArg, messageArg, charNameArg, xStart, yStart);
            SpriteBatch sb = SpriteBatchManager.Find(batchNameArg);
            Debug.Assert(sb != null);
            Debug.Assert(node.fontSprite != null);
            sb.Attach(node.fontSprite);
            return node;
        }

        public static void AddXML(Character.Name charNameArg, String asset, Texture.Name textNameArg)
        {
            CharacterManager.AddXML(charNameArg, asset, textNameArg);
        }

        public static void Remove(Character nodeArg)
        {
            Debug.Assert(nodeArg != null);
            FontManager inst = FontManager.getInstance();
            inst.baseRemove(nodeArg);
        }

        public static Font Find(Font.Name nameArg)
        {
            FontManager inst = FontManager.getInstance();
            inst.referenceNode.name = nameArg;
            Font font = (Font)inst.baseFind(inst.referenceNode);
            return font;
        }

        public static void Dump()
        {
            FontManager inst = FontManager.getInstance();
            inst.baseDump("FontManager");
        }

        public static void DumpStats()
        {
            FontManager inst = FontManager.getInstance();
            inst.baseDumpStats("FontManager");
        }

        protected override bool Compare(MLink first, MLink second)
        {
            Debug.Assert(first != null);
            Debug.Assert(second != null);
            Font firstFont = (Font)first;
            Font secondFont = (Font)second;

            if (firstFont.name == secondFont.name)
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
            MLink node = new Font();
            Debug.Assert(node != null);
            return node;
        }

        protected override void DumpNode(MLink link)
        {
            Debug.Assert(link != null);
            Font node = (Font)link;
            Debug.Assert(node != null);
            node.Dump();
        }

        // private

        private static FontManager getInstance()
        {
            Debug.Assert(instance != null);
            return instance;
        }
    }
}
