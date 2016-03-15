using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class Missile : MissileCategory
    {
        // data:
        //public bool hit;
        public float delta;
        private bool enable;

        public Missile(GameObject.Name nameArg, GameSprite.Name spriteName, int indexArg, float positionX, float positionY)
            : base(nameArg, spriteName, indexArg, MissileCategory.Type.Missile)
        {
            this.x = positionX;
            this.y = positionY;
            this.enable = false;
            this.delta = 16.0f;
        }

        public override void Remove()
        {
            this.colObj.colRect.Set(0, 0, 0, 0);
            base.Update();
            GameObject par = (GameObject)this.pParent;
            par.Update();

            base.Remove();
        }

        public override void Update()
        {
            base.Update();

            //if (!hit)
            //{
            //    this.y += 1.0f;
            //}

            this.y += delta;
        }

        ~Missile()
        {

        }

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

        public override void Accept(CollisionVisitor other)
        {
            other.VisitMissile(this);
        }

        public void SetPos(float xArg, float yArg) {
            this.x = xArg;
            this.y = yArg;
        }

        public void SetActive(bool state)
        {
            this.enable = state;
        }
    }
}
