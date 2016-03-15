using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShipReadyObserver : CollisionObserver
    {
        public override void Notify()
        {
            Ship shippy = ShipManager.GetShip();
            shippy.SetState(ShipManager.State.Ready);
        }
    }
}
