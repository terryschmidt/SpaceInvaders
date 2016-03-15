using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class UFOSplat : AlienCategory
    {
        public UFOSplat(GameObject.Name gameName, GameSprite.Name spriteName, int indexArg, float positionX, float positionY)
            : base(gameName, spriteName, indexArg, AlienCategory.Type.UFOSplat)
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
