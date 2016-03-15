using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class WallTop : WallCategory
    {
        public WallTop(GameObject.Name gameNameArg, GameSprite.Name spriteNameArg, int indexArg, float xArg, float yArg, float widthArg, float heightArg)
            : base(gameNameArg, spriteNameArg, indexArg, WallCategory.Type.Top)
        {
            this.colObj.colRect.Set(xArg, yArg, widthArg, heightArg);
            this.x = xArg;
            this.y = yArg;
            //this.colObj.colSprite.SetLineColor(1, 1, 0);
        }

        ~WallTop()
        {

        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitWallTop(this);
        }

        public override void Update()
        {
            base.Update();
        }

        public override void VisitMissileRoot(MissileRoot m)
        {
            CollisionPair.Collide((GameObject)m.pChild, this);
        }

        public override void VisitMissile(Missile m)
        {
            //Missile vs WallTop
            CollisionPair cp = CollisionPairManager.GetActivePair();
            cp.SetCollision(m, this);
            cp.NotifyListeners();


            //Debug.WriteLine("WallTop.VisitMissile()");
        }

        public override void VisitGrid(Grid a)
        {
            //Debug.WriteLine("WallTop.VisitGrid()");
        }
    }
}
