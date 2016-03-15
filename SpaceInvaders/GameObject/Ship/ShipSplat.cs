using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShipSplat : ShipCategory
    {
        public ShipSplat(GameObject.Name gameName, GameSprite.Name spriteName, int indexArg, float positionX, float positionY)
            : base(gameName, spriteName, indexArg, ShipCategory.Type.Splat)
        {
            this.x = positionX;
            this.y = positionY;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Accept(CollisionVisitor other)
        {
            
        }
    }
}
