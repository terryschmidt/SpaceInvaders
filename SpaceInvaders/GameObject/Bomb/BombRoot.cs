using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class BombRoot: BombCategory
    {
        public BombRoot(GameObject.Name gameNameArg, GameSprite.Name spriteNameArg, int indexArg, float xArg, float yArg)
            : base(gameNameArg, spriteNameArg, indexArg, BombCategory.Type.BombRoot)
        {
            this.x = xArg;
            this.y = yArg;
            this.colObj.colSprite.SetLineColor(1, 1, 1);
        }

        ~BombRoot()
        {

        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitBombRoot(this);
        }

        public override void Update()
        {
            base.baseUpdateBoundingBox();
            base.Update();
        }
    }
}
