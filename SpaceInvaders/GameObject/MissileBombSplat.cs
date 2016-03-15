using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class MissileBombSplat : AlienCategory
    {
          public MissileBombSplat(GameObject.Name gameName, GameSprite.Name spriteName, int indexArg, float positionX, float positionY)
            : base(gameName, spriteName, indexArg, AlienCategory.Type.MissileBombSplat)
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
