using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class CollisionObject
    {
        // data:
        public BoxSprite colSprite;
        public CollisionRectangle colRect;

        public CollisionObject(ProxySprite psArg)
        {
            Debug.Assert(psArg != null);
            GameSprite sprite = psArg.realSprite;
            Debug.Assert(sprite != null);
            this.colRect = new CollisionRectangle(sprite.GetScreenRect());
            Debug.Assert(this.colRect != null);

            // create sprite
            this.colSprite = BoxSpriteManager.Add(BoxSprite.Name.Box, this.colRect);
            Debug.Assert(this.colSprite != null);
            this.colSprite.SetLineColor(1, 1, 1);
        }

        public void PushPos(float x, float y)
        {
            this.colRect.x = x;
            this.colRect.y = y;
            this.colSprite.x = this.colRect.x;
            this.colSprite.y = this.colRect.y;
            this.colSprite.SetScreenRect(this.colRect.x, this.colRect.y, this.colRect.width, this.colRect.height);
            this.colSprite.Update();
        }
    }
}
