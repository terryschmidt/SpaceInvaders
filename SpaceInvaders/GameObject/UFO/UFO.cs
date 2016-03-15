using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class UFO : UFOCategory
    {
        // data:
        public float UFOspeed;

         public UFO(GameObject.Name nameArg, GameSprite.Name spriteName, int indexArg, float positionX, float positionY)
            : base(nameArg, spriteName, indexArg, UFOCategory.Type.UFO)
        {
            this.x = positionX;
            this.y = positionY;
            //this.hit = false;
            //this.delta = 1.0f;
            this.UFOspeed = Values.UFOspeed;
        }

        public override void Update()
        {
            if (this.x > 960 || this.x < -50)
            {
                Values.ufoIsActive = false;
                this.Remove();
                return;
            }

            this.x += Values.UFOspeed;
            base.Update();
        }

        ~UFO()
        {

        }

        public override void Accept(CollisionVisitor other)
        {
            //other.VisitMissileRoot(this);
            other.VisitUFO(this);
        }

        public override void VisitMissileRoot(MissileRoot m)
        {
            CollisionPair.Collide((GameObject)m.pChild, this);
        }

        public override void VisitMissile(Missile m)
        {
            CollisionPair cp = CollisionPairManager.GetActivePair();
            cp.SetCollision(m, this);
            cp.NotifyListeners();
        }
    }
}
