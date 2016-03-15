using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class FakeShip : ShipCategory
    {
        public FakeShip(GameObject.Name nameArg, GameSprite.Name spriteName, int indexArg, float positionX, float positionY)
            : base(nameArg, spriteName, indexArg, ShipCategory.Type.FakeShip)
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
