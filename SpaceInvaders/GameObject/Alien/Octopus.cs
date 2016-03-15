using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class Octopus : AlienCategory
    {

        // data:
        //IrrKlang.ISoundEngine engine;

        public Octopus(GameObject.Name gameName, GameSprite.Name spriteName, int indexArg, float positionX, float positionY)
            : base(gameName, spriteName, indexArg, AlienCategory.Type.Octopus)
        {
            this.x = positionX;
            this.y = positionY;
            //this.thisOctoIndex = indexArg;
            //engine = new IrrKlang.ISoundEngine();
        }

        public override void Update()
        {
            base.Update();
        }

        ~Octopus()
        {

        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitOctopus(this);
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

        public override void VisitShipRoot(ShipRoot s)
        {
            CollisionPair.Collide((GameObject)s.pChild, this);
        }

        public override void VisitShip(Ship s)
        {
            CollisionPair cp = CollisionPairManager.GetActivePair();
            cp.SetCollision(s, this);
            cp.NotifyListeners();
        }

        public override void VisitShieldRoot(ShieldRoot s)
        {
            CollisionPair.Collide((GameObject)s.pChild, this);
        }

        public override void VisitShieldBrick(ShieldBrick s)
        {
            CollisionPair cp = CollisionPairManager.GetActivePair();
            cp.SetCollision(s, this);
            cp.NotifyListeners();
        }

        public override void VisitShieldGrid(ShieldGrid s)
        {
            CollisionPair.Collide(s, (GameObject)this.pChild);
        }
    }
}
