using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Character : MLink
    {
        // data:
        public Name name;
        public int key;
        private Azul.Rect azulSubRect;
        private Texture texture;

        public enum Name
        {
            Consolas36pt,
            Consolas24pt,
            Futura,
            ComicSans,
            NullObject,
            Uninitialized
        }

        public Character()
            : base()
        {
            this.name = Name.Uninitialized;
            this.azulSubRect = new Azul.Rect();
            this.key = 0;
            this.texture = null;
        }

        ~Character()
        {
            this.name = Name.Uninitialized;
            this.azulSubRect = null;
            this.texture = null;
        }

        public void Set(Character.Name charNameArg, int keyArg, Texture.Name textNameArg, float xArg, float yArg, float widthArg, float heightArg)
        {
            Debug.Assert(this.azulSubRect != null);
            this.name = charNameArg;
            this.texture = TextureManager.Find(textNameArg);
            Debug.Assert(this.texture != null);
            this.azulSubRect.Set(xArg, yArg, widthArg, heightArg);
            this.key = keyArg;
        }

        public void Dump()
        {
            Debug.WriteLine("\t\tname: {0} ({1})", this.name, this.GetHashCode());
            Debug.WriteLine("\t\t\tkey: {0}", this.key);
            if (this.texture != null)
            {
                Debug.WriteLine("\t\t   pTexture: {0}", this.texture.name);
            }
            else
            {
                Debug.WriteLine("\t\t   pTexture: null");
            }
            Debug.WriteLine("\t\t      pRect: {0}, {1}, {2}, {3}", this.azulSubRect.x, this.azulSubRect.y, this.azulSubRect.width, this.azulSubRect.height);
        }

        public Azul.Rect GetAzulSubRect()
        {
            Debug.Assert(this.azulSubRect != null);
            return this.azulSubRect;
        }

        public Azul.Texture GetAzulTexture()
        {
            Debug.Assert(this.texture != null);
            return this.texture.azulTexture;
        }
    }
}
