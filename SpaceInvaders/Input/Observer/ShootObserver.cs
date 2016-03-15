using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShootObserver : InputObserver
    {
        public override void Notify()
        {
            //Debug.WriteLine("Shoot pressed");
            Ship shippy = ShipManager.GetShip();
            shippy.ShootMissile();
        }
    }
}
