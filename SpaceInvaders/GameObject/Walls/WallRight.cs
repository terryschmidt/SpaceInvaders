using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class WallRight : WallCategory
    {
        public WallRight(GameObject.Name gameNameArg, GameSprite.Name spriteNameArg, int indexArg, float xArg, float yArg, float widthArg, float heightArg)
            : base(gameNameArg, spriteNameArg, indexArg, WallCategory.Type.Right)
        {
            this.colObj.colRect.Set(xArg, yArg, widthArg, heightArg);
            this.x = xArg;
            this.y = yArg;
            //this.colObj.colSprite.SetLineColor(1, 0, 0);
        }

        ~WallRight()
        {

        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitWallRight(this);
        }

        public override void Update()
        {
            base.Update();
        }

        public override void VisitBomb(Bomb b)
        {
            Debug.WriteLine("WallRight.VisitBomb()");
        }

        public override void VisitGrid(Grid a)
        {
            //Debug.WriteLine("WallRight, VisitGrid");

            CollisionPair cp = CollisionPairManager.GetActivePair();
            Debug.Assert(cp != null);
            cp.SetCollision(a, this);
            cp.NotifyListeners();
        }
    }
}
