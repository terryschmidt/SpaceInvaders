using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class Ship : ShipCategory
    {
        // data:
        public float shipSpeed;
        private ShipState state;

        public Ship(GameObject.Name nameArg, GameSprite.Name spriteName, int indexArg, float positionX, float positionY)
            : base(nameArg, spriteName, indexArg, ShipCategory.Type.Ship)
        {
            this.x = positionX;
            this.y = positionY;
            //this.hit = false;
            //this.delta = 1.0f;
            this.shipSpeed = 4.5f;
            this.state = null;
        }

        public override void Update()
        {
            base.Update();
        }

        ~Ship()
        {

        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitShip(this);
        }

        //public override void VisitGrid(Grid a)
        //{
        //    CollisionPair cp = CollisionPairManager.GetActivePair();
        //    Debug.Assert(cp != null);
        //    cp.SetCollision(a, this);
        //    cp.NotifyListeners();
        //}

        public override void VisitBomb(Bomb b)
        {
            CollisionPair cp = CollisionPairManager.GetActivePair();
            cp.SetCollision(b, this);
            cp.NotifyListeners();
        }

        public override void VisitBombRoot(BombRoot b)
        {
            CollisionPair cp = CollisionPairManager.GetActivePair();
            cp.SetCollision(b, this);
            cp.NotifyListeners();
        }

        public void MoveRight()
        {
            this.state.MoveRight(this);
        }

        public void MoveLeft()
        {
            this.state.MoveLeft(this);
        }

        public void ShootMissile()
        {
            this.state.ShootMissile(this);
        }

        public void SetState(ShipManager.State stateArg)
        {
            this.state = ShipManager.GetState(stateArg);
        }

    }
}
