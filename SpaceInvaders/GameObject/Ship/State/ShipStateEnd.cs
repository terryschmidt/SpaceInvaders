using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShipStateEnd : ShipState
    {
        public override void ShootMissile(Ship shipArg)
        {
            // cant shoot in this state
        }

        public override void Handle(Ship shipArg)
        {
            
        }

        public override void MoveRight(Ship shipArg)
        {
            //shipArg.x += shipArg.shipSpeed;
        }

        public override void MoveLeft(Ship shipArg)
        {
            //shipArg.x -= shipArg.shipSpeed;
        }
    }
}
