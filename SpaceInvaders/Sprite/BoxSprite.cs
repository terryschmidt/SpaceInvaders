using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class BoxSprite : SpriteBase
    {
        // data:
        public Name name;
        private Azul.Rect rect;
        private Azul.Color color;
        private Azul.SpriteBox azulBox;

        public float x;
        public float y;
        public float sx;
        public float sy;
        public float angle;

        public enum Name
        {
            Box,
            Uninitialized
        }

        public override Enum GetName()
        {
            return name;
        }

        public BoxSprite()
            : base()
        {
            name = BoxSprite.Name.Uninitialized;
            color = new Azul.Color(1.0f, 1.0f, 1.0f);
            rect = new Azul.Rect();
            azulBox = new Azul.SpriteBox();

            this.x = 0.0f;
            this.y = 0.0f;
            this.sx = 1.0f;
            this.sy = 1.0f;
            this.angle = 0.0f;
        }

        public void Set(BoxSprite.Name name, float x, float y, float width, float height)
        {
            Debug.Assert(color != null);
            Debug.Assert(rect != null);
            this.name = name;
            this.rect.Set(x, y, width, height);
            Debug.Assert(azulBox != null);
            this.azulBox.Swap(rect, color);

            this.x = azulBox.x;
            this.y = azulBox.y;
            this.sx = azulBox.sx;
            this.sy = azulBox.sy;
            this.angle = azulBox.angle;
        }

        public void SetLineColor(float r, float g, float b, float a = 1.0f)
        {
            color.Set(r, g, b, a);
        }

        public void SetScreenRect(float x, float y, float width, float height)
        {
            this.Set(this.name, x, y, width, height);

        }

        public override void Update()
        {
            this.azulBox.x = this.x;
            this.azulBox.y = this.y;
            this.azulBox.sx = this.sx;
            this.azulBox.sy = this.sy;
            this.azulBox.angle = this.angle;

            this.azulBox.SwapColor(this.color);
            this.azulBox.Update();
        }

        public override void Render()
        {
            azulBox.Render();
        }

        public void Dump()
        {

        }

    }
}
