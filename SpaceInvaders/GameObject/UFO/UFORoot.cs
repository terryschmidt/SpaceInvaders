using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class UFORoot : UFOCategory
    {
        public UFORoot(GameObject.Name nameArg, GameSprite.Name spriteName, int indexArg, float positionX, float positionY)
            : base(nameArg, spriteName, indexArg, UFOCategory.Type.UFORoot)
        {
            this.x = positionX;
            this.y = positionY;
            this.colObj.colSprite.SetLineColor(1, 0, 0);
        }

        ~UFORoot()
        {

        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitUFORoot(this);
        }

        public override void Update()
        {
            base.baseUpdateBoundingBox();
            base.Update();
        }

        public override void VisitMissileRoot(MissileRoot m)
        {
            CollisionPair.Collide(m, (GameObject)this.pChild);
        }
    }
}
