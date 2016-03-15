using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class TextureManager : Manager
    {
        // data:
        private static TextureManager instance = null;
        private Texture referenceNode;

        private TextureManager(int reserveNum = 3, int reserveGrow = 1)
            : base(reserveNum, reserveGrow)
        {
            referenceNode = (Texture)this.CreateNode();
        }

        ~TextureManager()
        {
            this.referenceNode = null;
            TextureManager.instance = null;
        }

        public static void Create(int reserveNum = 3, int reserveGrow = 1)
        {
            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);
            Debug.Assert(instance == null);

            if (instance == null)
            {
                instance = new TextureManager(reserveNum, reserveGrow);
                // add null texture into manager to allow find
                Texture pTex = TextureManager.Add(Texture.Name.NullObject, "HotPink.tga");
                Debug.Assert(pTex != null);
            }
        }

        public static void Destroy()
        {
            TextureManager inst = TextureManager.getInstance();
            inst.baseDestroy();
        }


        public static Texture Add(Texture.Name name, String assetName, Azul.Texture_Filter min = Azul.Texture_Filter.NEAREST, Azul.Texture_Filter mag = Azul.Texture_Filter.NEAREST)
        {
            TextureManager inst = TextureManager.getInstance();
            Texture node = (Texture)inst.baseAdd();
            Debug.Assert(node != null);
            node.Set(name, assetName, min, mag);
            return node;
        }

        public static void Remove(Texture node)
        {
            Debug.Assert(node != null);
            TextureManager inst = TextureManager.getInstance();
            inst.baseRemove(node);
        }

        public static Texture Find(Texture.Name name)
        {
            TextureManager inst = TextureManager.getInstance();
            inst.referenceNode.name = name;
            Texture data = (Texture)inst.baseFind(inst.referenceNode);
            return data;
        }

        public static void Dump()
        {
            TextureManager inst = TextureManager.getInstance();
            inst.baseDump("TextureManager");
        }

        protected override bool Compare(MLink first, MLink second)
        {
            Debug.Assert(first != null);
            Debug.Assert(second != null);
            Texture firstData = (Texture)first;
            Texture secondData = (Texture)second;

            if (firstData.name == secondData.name)
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
            MLink node = new Texture();
            return node;
        }

        protected override void DumpNode(MLink link)
        {
            Debug.Assert(link != null);
            Texture node = (Texture)link;
            node.Dump();
        }

        private static TextureManager getInstance()
        {
            Debug.Assert(instance != null);
            return instance;
        }
    }
}
