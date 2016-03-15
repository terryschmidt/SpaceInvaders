using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class WallLeft : WallCategory
    {
        public WallLeft(GameObject.Name gameNameArg, GameSprite.Name spriteNameArg, int indexArg, float xArg, float yArg, float widthArg, float heightArg)
            : base(gameNameArg, spriteNameArg, indexArg, WallCategory.Type.Left)
        {
            this.colObj.colRect.Set(xArg, yArg, widthArg, heightArg);
            this.x = xArg;
            this.y = yArg;
            //this.colObj.colSprite.SetLineColor(1, 0, 0);
        }

        ~WallLeft()
        {

        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitWallLeft(this);
        }

        public override void Update()
        {
            base.Update();
        }

        public override void VisitBomb(Bomb b)
        {
            Debug.WriteLine("WallLeft.VisitBomb()");
        }

        public override void VisitGrid(Grid a)
        {
            //Debug.WriteLine("WallLeft, VisitGrid");

            CollisionPair cp = CollisionPairManager.GetActivePair();
            Debug.Assert(cp != null);
            cp.SetCollision(a, this);
            cp.NotifyListeners();
        }

        public override void VisitMissile(Missile m)
        {
            //Debug.WriteLine("WallLeft.VisitMissile()");
        }
    }
}
