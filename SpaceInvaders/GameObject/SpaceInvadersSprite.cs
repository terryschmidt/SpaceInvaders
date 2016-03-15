using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SpaceInvadersSprite : AlienCategory
    {
         public SpaceInvadersSprite(GameObject.Name nameArg, GameSprite.Name spriteName, int indexArg, float positionX, float positionY)
            : base(nameArg, spriteName, indexArg, AlienCategory.Type.SpaceInvaders)
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
