using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShipRoot : ShipCategory
    {
        public ShipRoot(GameObject.Name nameArg, GameSprite.Name spriteName, int indexArg, float positionX, float positionY)
            : base(nameArg, spriteName, indexArg, ShipCategory.Type.ShipRoot)
        {
            this.x = positionX;
            this.y = positionY;
            this.colObj.colSprite.SetLineColor(0, 1, 0);
        }

        ~ShipRoot()
        {

        }

        public override void Accept(CollisionVisitor other)
        {
            //other.VisitMissileRoot(this);
            other.VisitShipRoot(this);
        }

        public override void Update()
        {
            base.baseUpdateBoundingBox();
            base.Update();
        }

        public override void VisitBombRoot(BombRoot b)
        {
            CollisionPair.Collide((GameObject)b.pChild, this);
        }

        public override void VisitBomb(Bomb b)
        {
            CollisionPair.Collide(b, (GameObject)this.pChild);
        }

        public override void VisitGrid(Grid a)
        {
            // alien grid vs wallroot
            CollisionPair.Collide(a, (GameObject)this.pChild);
        }
    }
}
