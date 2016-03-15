using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShieldBrick : ShieldCategory
    {
        public ShieldBrick(GameObject.Name gameNameArg, GameSprite.Name spriteNameArg, int indexArg, float xArg, float yArg)
            : base(gameNameArg, spriteNameArg, indexArg, ShieldCategory.Type.Brick)
        {
            this.x = xArg;
            this.y = yArg;
            //this.SetCollisionColor(0.0f, 1.0f, 0.0f);
            //this.colObj.colSprite.SetLineColor(0, 0, 0);
        }

        ~ShieldBrick()
        {

        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitShieldBrick(this);
        }

        public override void VisitMissile(Missile m)
        {
            CollisionPair cp = CollisionPairManager.GetActivePair();
            cp.SetCollision(m, this);
            cp.NotifyListeners();
        }

        public override void VisitBomb(Bomb b)
        {
            //Debug.WriteLine("ShieldBrick.VisitBomb()");
            CollisionPair cp = CollisionPairManager.GetActivePair();
            cp.SetCollision(b, this);
            cp.NotifyListeners();
        }

        public override void VisitBombRoot(BombRoot b)
        {
            //CollisionPair cp = CollisionPairManager.GetActivePair();
            //cp.SetCollision(b, this);
            //cp.NotifyListeners();
        }

        public override void VisitGrid(Grid a)
        {
            // alien grid vs wallroot
            CollisionPair.Collide((GameObject)a.pChild, this);

            //GameObject curr = (GameObject)this.pChild;
            //while (curr != null)
            //{
            //    CollisionPair.Collide(curr, a);
            //    curr = curr.pSibling as GameObject;
            //}
        }

        public override void VisitColumn(Column a)
        {
            CollisionPair.Collide((GameObject)a.pChild, this);


            //GameObject curr = (GameObject)this.pChild;
            //while (curr != null)
            //{
            //    CollisionPair.Collide(curr, a);
            //    curr = curr.pSibling as GameObject;
            //}
        }

        public override void VisitOctopus(Octopus a)
        {
            CollisionPair cp = CollisionPairManager.GetActivePair();
            cp.SetCollision(a, this);
            cp.NotifyListeners();
        }

        public override void VisitCrab(Crab a)
        {
            CollisionPair cp = CollisionPairManager.GetActivePair();
            cp.SetCollision(a, this);
            cp.NotifyListeners();
        }

        public override void VisitSquid(Squid a)
        {
            CollisionPair cp = CollisionPairManager.GetActivePair();
            cp.SetCollision(a, this);
            cp.NotifyListeners();
        }
        

        public override void Update()
        {
            base.Update();
        }
    }
}
