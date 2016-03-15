using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShipUnpauser : Command
    {
        public ShipUnpauser()
        {

        }

        public override void execute(float deltaTime)
        {
            Ship shippy = ShipManager.GetShip();
            shippy.SetState(ShipManager.State.Ready);
            shippy.x = 448;
            shippy.y = 130;
        }
    }
}
