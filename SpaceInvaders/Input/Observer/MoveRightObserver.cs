using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class MoveRightObserver : InputObserver
    {
        public override void Notify()
        {
            //Debug.WriteLine("Right arrow pressed");
            Ship shippy = ShipManager.GetShip();
            shippy.MoveRight();
        }
    }
}
