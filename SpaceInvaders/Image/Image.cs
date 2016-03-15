using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Image : MLink
    {
        // data:

        public Name name;
        private Azul.Rect rect;
        private Texture texture;

        public enum Name
        {
            GreenAlien,
            WhiteAlien,
            Stitch,
            RedBird,
            YellowBird,
            UFOSplat,
            MissileBombSplat2,
            GreenBird,
            SpaceInvaders,
            WhiteBird,
            UFOBomb,
            BlueBird,
            BombStraight,
            StaticShip,
            MissileBombSplat,
            BombZigZag,
            BombCross,
            ShipSplat,
            Brick,
            MissileWallSplat,
            BrickLeft_Top0,
            BrickLeft_Top1,
            BrickLeft_Bottom,
            BrickRight_Top0,
            BrickRight_Top1,
            BrickRight_Bottom,
            Ship,
            Crab,
            Crab2,
            Squid,
            Squid2,
            Splat,
            Splat2,
            Splat3,
            Splat4,
            Splat5,
            Octopus,
            Octopus2,
            UFO,
            Missile,
            NullObject,
            Uninitialized
        }

        public Image()
            : base()
        {
            this.name = Name.Uninitialized;
            this.texture = null;
            this.rect = new Azul.Rect();
        }

        ~Image()
        {
            this.name = Name.Uninitialized;
            this.rect = null;
            this.texture = null;
        }

        public void Set(Image.Name name, Texture.Name tName, float x, float y, float width, float height)
        {
            Debug.Assert(this.rect != null);
            this.name = name;
            this.texture = TextureManager.Find(tName);
            Debug.Assert(this.texture != null);
            this.rect.Set(x, y, width, height);
        }

        public Azul.Rect getRect()
        {
            Debug.Assert(rect != null);
            return rect;
        }

        public Azul.Texture getTexture()
        {
            Debug.Assert(texture != null);
            return texture.azulTexture;
        }

        // print function

        public void Dump()
        {
            // Data:
            Debug.WriteLine("\t\tname: {0} ({1})", this.name, this.GetHashCode());
            Debug.WriteLine("\t\t   pTexture: {0}", this.texture.name);
            Debug.WriteLine("\t\t      pRect: {0}, {1}, {2}, {3}", this.rect.x, this.rect.y, this.rect.width, this.rect.height);
            //  Debug.WriteLine("\t\t----------");

            if (this.next == null)
            {
                Debug.WriteLine("\t\tnext: null");
            }
            else
            {
                Texture pTmp = (Texture)this.next;
                Debug.WriteLine("\t\tnext: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            }

            if (this.prev == null)
            {
                Debug.WriteLine("\t\tprev: null");
            }
            else
            {
                Texture pTmp = (Texture)this.prev;
                Debug.WriteLine("\t\tprev: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            }
        }
    }
}
