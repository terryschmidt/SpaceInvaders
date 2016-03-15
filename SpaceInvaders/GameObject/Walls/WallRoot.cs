using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class WallRoot : WallCategory
    {
        public WallRoot(GameObject.Name gameNameArg, GameSprite.Name spriteNameArg, int indexArg, float xArg, float yArg)
            : base(gameNameArg, spriteNameArg, indexArg, WallCategory.Type.WallRoot)
        {
            this.x = xArg;
            this.y = yArg;

            this.colObj.colSprite.SetLineColor(0, 0, 1);
        }

        ~WallRoot()
        {

        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitWallRoot(this);
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

        public override void VisitMissileRoot(MissileRoot m)
        {
            // missileroot vs wallroot
            CollisionPair.Collide((GameObject)m.pChild, this);

        }

        public override void VisitMissile(Missile m)
        {
            CollisionPair.Collide(m, (GameObject)this.pChild);
        }
    }
}