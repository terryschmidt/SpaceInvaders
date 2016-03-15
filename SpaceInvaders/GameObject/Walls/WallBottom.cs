using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class WallBottom : WallCategory
    {
        public WallBottom(GameObject.Name gameNameArg, GameSprite.Name spriteNameArg, int indexArg, float xArg, float yArg, float widthArg, float heightArg)
            : base(gameNameArg, spriteNameArg, indexArg, WallCategory.Type.Top)
        {
            this.colObj.colRect.Set(xArg, yArg, widthArg, heightArg);
            this.x = xArg;
            this.y = yArg;
            this.colObj.colSprite.SetLineColor(0, 240, 0);
        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitWallBottom(this);
        }

        public override void VisitMissileRoot(MissileRoot m)
        {
            CollisionPair.Collide((GameObject)m.pChild, this);
        }

        public override void VisitMissile(Missile m)
        {
            // remove octo from game object manager
            // remove octo from sprite batch list
            // remove octo from boxsprite list
            // this.Remove();
            // this.colObj.colSprite.SetScreenRect(10000, 10000, 0, 0);

            /*CollisionSubject sub = CollisionPairManager.GetActivePair().subject;

            GameObject parent;

            parent = sub.gameObjA.pParent as GameObject;
            sub.gameObjA.Remove();

            if (parent.pChild == null)
            {
                parent.Remove();
            }
            */


            //Debug.WriteLine("WallBottom.VisitMissile()");
        }

        public override void VisitGrid(Grid a)
        {
            CollisionPair cp = CollisionPairManager.GetActivePair();
            Debug.Assert(cp != null);
            cp.SetCollision(a, this);
            cp.NotifyListeners();
        }

        public override void VisitBomb(Bomb b)
        {
            CollisionPair cp = CollisionPairManager.GetActivePair();
            cp.SetCollision(b, this);
            cp.NotifyListeners();
        }
    }
}
