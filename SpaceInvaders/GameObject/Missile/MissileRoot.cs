using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class MissileRoot : MissileCategory
    {
        public MissileRoot(GameObject.Name nameArg, GameSprite.Name spriteName, int indexArg, float positionX, float positionY)
            : base(nameArg, spriteName, indexArg, MissileCategory.Type.MissileRoot)
        {
            this.x = positionX;
            this.y = positionY;
            this.colObj.colSprite.SetLineColor(0, 240, 0);
        }

        ~MissileRoot()
        {

        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitMissileRoot(this);
        }

        public override void VisitBombRoot(BombRoot b)
        {
            CollisionPair.Collide((GameObject)b.pChild, this);
        }

        public override void VisitBomb(Bomb b)
        {
            CollisionPair.Collide(b, (GameObject)this.pChild);
        }

        public override void Update()
        {
            base.baseUpdateBoundingBox();
            base.Update();
        }
    }
}
