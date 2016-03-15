using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShieldGrid : ShieldCategory
    {
        public ShieldGrid(GameObject.Name gameNameArg, GameSprite.Name spriteNameArg, int indexArg, float xArg, float yArg)
            : base(gameNameArg, spriteNameArg, indexArg, ShieldCategory.Type.Grid)
        {
            this.x = xArg;
            this.y = yArg;
        }

        ~ShieldGrid()
        {

        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitShieldGrid(this);
        }

        public override void VisitGrid(Grid a)
        {
            // alien grid vs wallroot
            CollisionPair.Collide(a, (GameObject)this.pChild);

            //GameObject curr = (GameObject)this.pChild;
            //while (curr != null)
            //{
            //    CollisionPair.Collide(curr, a);
            //    curr = curr.pSibling as GameObject;
            //}
        }

        public override void VisitMissile(Missile m)
        {
            CollisionPair.Collide(m, (GameObject)this.pChild);
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
