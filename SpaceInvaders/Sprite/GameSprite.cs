using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class GameSprite : SpriteBase
    {
        // data:
        public Name name;
        private Azul.Sprite azulSprite;
        public Image image;
        private Azul.Color color;
        private Azul.Rect rect;
        public float x;
        public float y;
        public float sx;
        public float sy;
        public float angle;

        public override Enum GetName()
        {
            return this.name;
        }

        public enum Name
        {
            GreenAlien,
            WhiteAlien,
            UFOSplat,
            Stitch,
            RedBird,
            ShipSplat,
            SpaceInvaders,
            Splat2,
            UFOBomb,
            Splat3,
            MissileBombSplat2,
            Splat4,
            Splat5,
            FakeShip,
            YellowBird,
            GreenBird,
            StaticShip,
            BombSplat,
            MissileWallSplat,
            WhiteBird,
            BlueBird,
            MissileBombSplat,
            Missile,
            BombZigZag,
            BombStraight,
            BombDagger,
            Ship,
            Splat,
            Crab,
            Crab2,
            Squid,
            Squid2,
            Octopus,
            Octopus2,
            Brick,
            Brick_LeftTop0,
            Brick_LeftTop1,
            Brick_LeftBottom,
            Brick_RightTop0,
            Brick_RightTop1,
            Brick_RightBottom,
            UFO,
            UFORoot,
            NullObject,
            Uninitialized
        }

        public GameSprite()
            : base()
        {
            this.azulSprite = new Azul.Sprite();
            this.rect = new Azul.Rect();
            this.color = new Azul.Color(1.0f, 1.0f, 1.0f);
            this.image = null;
        }

        ~GameSprite()
        {
            this.azulSprite = null;
            this.rect = null;
            this.color = null;
            this.image = null;
        }

        public void Set(GameSprite.Name name, Image.Name iName, float x, float y, float width, float height)
        {
            Debug.Assert(this.color != null);
            this.color.Set(1.0f, 1.0f, 1.0f, 1.0f);
            Debug.Assert(this.rect != null);
            this.rect.Set(x, y, width, height);
            this.image = ImageManager.Find(iName);
            Debug.Assert(this.image != null);
            this.name = name;
            Debug.Assert(this.azulSprite != null);
            this.azulSprite.Swap(image.getTexture(), image.getRect(), this.rect, this.color);

            this.x = azulSprite.x;
            this.y = azulSprite.y;
            this.sx = azulSprite.sx;
            this.sy = azulSprite.sy;
            this.angle = azulSprite.angle;
        }

        public void SetColor(float r, float g, float b, float a = 1.0f)
        {
            Debug.Assert(this.color != null);
            this.color.Set(r, g, b, a);
        }

        public void SwapImage(Image newImage)
        {
            Debug.Assert(azulSprite != null);
            Debug.Assert(newImage != null);
            this.image = newImage;
            this.azulSprite.SwapTexture(this.image.getTexture());
            this.azulSprite.SwapTextureRect(this.image.getRect());
        }

        public override void Update()
        {
            Debug.Assert(this.azulSprite != null);
            this.azulSprite.x = this.x;
            this.azulSprite.y = this.y;
            this.azulSprite.sx = this.sx;
            this.azulSprite.sy = this.sy;
            this.azulSprite.angle = this.angle;
            this.azulSprite.SwapColor(this.color);
            this.azulSprite.Update();
        }

        public override void Render()
        {
            Debug.Assert(this.azulSprite != null);
            this.azulSprite.Render();
        }

        public void Dump()
        {
            // Data:
            Debug.WriteLine("\t\tname: {0} ({1})", this.name, this.GetHashCode());

            if (this.image != null)
            {
                Debug.WriteLine("\t\t        pImage: {0}", this.image.name);
            }
            else
            {
                Debug.WriteLine("\t\t        pImage: null");
            }


            Debug.WriteLine("\t\t        pColor: {0}, {1}, {2}, {3}", this.color.red, this.color.green, this.color.blue, this.color.alpha);
            Debug.WriteLine("\t\t         pRect: {0}, {1}, {2}, {3}", this.rect.x, this.rect.y, this.rect.width, this.rect.height);
            Debug.WriteLine("\t\t   pAzulSprite: {0}", this.azulSprite.GetHashCode());
        }

        public Azul.Rect GetScreenRect()
        {
            Debug.Assert(this.rect != null);
            return this.rect;
        }
    }
}
