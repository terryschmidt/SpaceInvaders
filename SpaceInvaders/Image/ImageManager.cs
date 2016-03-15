using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ImageManager : Manager
    {
        // data: 
        private static ImageManager instance = null;
        private Image referenceNode;

        private ImageManager(int reserveNum = 3, int reserveGrow = 1) 
            : base(reserveNum, reserveGrow)
        {
            referenceNode = (Image)this.CreateNode();
        }

        protected override MLink CreateNode()
        {
            MLink node = new Image();
            return node;
        }

        public static void Create(int reserveNum = 3, int reserveGrow = 1)
        {
            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);
            Debug.Assert(instance == null);

            if (instance == null)
            {
                instance = new ImageManager(reserveNum, reserveGrow);
                // add null texture to manager to allow find
                Image pImage = ImageManager.Add(Image.Name.NullObject, Texture.Name.NullObject, 0, 0, 1, 1);
                Debug.Assert(pImage != null);
            }
        }

        public static void Destroy()
        {
            ImageManager inst = ImageManager.getInstance();
            inst.baseDestroy();
        }


        public static Image Add(Image.Name name, Texture.Name tName, float x, float y, float width, float height)
        {
            ImageManager inst = ImageManager.getInstance();
            Image node = (Image)inst.baseAdd();
            Debug.Assert(node != null);
            node.Set(name, tName, x, y, width, height);
            return node;
        }

        public static void Remove(Image node)
        {
            Debug.Assert(node != null);
            ImageManager inst = ImageManager.getInstance();
            inst.baseRemove(node);
        }

        public static Image Find(Image.Name name)
        {
            ImageManager inst = ImageManager.getInstance();
            inst.referenceNode.name = name;
            Image data = (Image)inst.baseFind(inst.referenceNode);
            return data;
        }

        public static void Dump()
        {
            ImageManager inst = ImageManager.getInstance();
            inst.baseDump("ImageManager");
        }

        protected override bool Compare(MLink first, MLink second)
        {
            Debug.Assert(first != null);
            Debug.Assert(second != null);
            Image firstImage = (Image)first;
            Image secondImage = (Image)second;

            if (firstImage.name == secondImage.name)
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
            node.Dump();
        }

        // private
        private static ImageManager getInstance()
        {
            Debug.Assert(instance != null);
            return instance;
        }
    }
}
