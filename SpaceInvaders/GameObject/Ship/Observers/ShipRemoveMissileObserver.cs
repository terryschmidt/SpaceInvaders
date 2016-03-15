using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShipRemoveMissileObserver : CollisionObserver
    {
        private GameObject missile;

        public ShipRemoveMissileObserver()
        {
            this.missile = null;
        }

        public ShipRemoveMissileObserver(ShipRemoveMissileObserver m)
        {
            this.missile = m.missile;
        }

        public override void Notify()
        {
            // Delete missile
            //Debug.WriteLine("ShipRemoveMissileObserver: {0} {1}", this.subject.gameObjA, this.subject.gameObjB);

            this.missile = MissileCategory.GetMissile(this.subject.gameObjA, this.subject.gameObjB);
            //Debug.WriteLine("MissileRemoveObserver: --> delete missile {0}", missile);

            if (missile.markForDeath == false)
            {
                missile.markForDeath = true;
                //   Delay
                ShipRemoveMissileObserver pObserver = new ShipRemoveMissileObserver(this);
                DelayObjectManager.Attach(pObserver);
            }
        }

        public override void Execute()
        {
            this.missile.Remove();
        }
    }
}
