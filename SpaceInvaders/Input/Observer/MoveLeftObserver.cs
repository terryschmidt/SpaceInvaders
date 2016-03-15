using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class MoveLeftObserver : InputObserver
    {
        public override void Notify()
        {
            //Debug.WriteLine("Left arrow pressed");
            Ship shippy = ShipManager.GetShip();
            shippy.MoveLeft();
        }
    }
}
