using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class AdjustGridMovementObserver : CollisionObserver
    {
        public AdjustGridMovementObserver()
        {

        }

        public override void Notify()
        {
            Values.AdjustGridMovementInterval();
        }
    }
}
