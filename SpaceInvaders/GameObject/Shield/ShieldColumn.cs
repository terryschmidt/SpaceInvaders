using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShieldColumn : ShieldCategory
    {
        public ShieldColumn(GameObject.Name gameNameArg, GameSprite.Name spriteNameArg, int indexArg, float xArg, float yArg)
            : base(gameNameArg, spriteNameArg, indexArg, ShieldCategory.Type.Column)
        {
            this.x = xArg;
            this.y = yArg;
            //this.colObj.colSprite.SetLineColor(0, 0, 0);
        }

        ~ShieldColumn()
        {

        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitShieldColumn(this);
        }

        public override void VisitMissile(Missile m)
        {
            // missile vs shield column
            CollisionPair.Collide(m, (GameObject)this.pChild);
        }

        public override void VisitBombRoot(BombRoot b)
        {
            CollisionPair.Collide(b, (GameObject)this.pChild);
        }

        public override void VisitBomb(Bomb b)
        {
            CollisionPair.Collide(b, (GameObject)this.pChild);
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

        public override void Update()
        {
            base.baseUpdateBoundingBox();
            base.Update();
        }
    }
}
