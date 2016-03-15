using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShieldRoot : ShieldCategory
    {
        public ShieldRoot(GameObject.Name gameNameArg, GameSprite.Name spriteNameArg, int indexArg, float xArg, float yArg)
            : base(gameNameArg, spriteNameArg, indexArg, ShieldCategory.Type.Root)
        {
            this.x = xArg;
            this.y = yArg;
            SetCollisionColor(1, 0, 0);
        }

        ~ShieldRoot()
        {

        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitShieldRoot(this);
        }

        public override void VisitGrid(Grid a)
        {
            // alien grid vs wallroot
            CollisionPair.Collide(a, (GameObject)this.pChild);

            //    GameObject curr = (GameObject)this.pChild;
            //    while (curr != null)
            //    {
            //        CollisionPair.Collide(curr, a);
            //        curr = curr.pSibling as GameObject;
            //    }
            //}
        }

        public override void VisitColumn(Column a)
        {
            CollisionPair.Collide(a, (GameObject)this.pChild);

            //GameObject curr = (GameObject)this.pChild;
            //while (curr != null)
            //{
            //    CollisionPair.Collide(curr, a);
            //    curr = curr.pSibling as GameObject;
            //}
        }

        public override void VisitMissileRoot(MissileRoot m)
        {
            CollisionPair.Collide((GameObject)m.pChild, this);
        }

        public override void VisitMissile(Missile m)
        {
            CollisionPair.Collide(m, (GameObject)this.pChild);
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
