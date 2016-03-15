using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShipStateMissileFlying : ShipState
    {
        public override void ShootMissile(Ship shipArg)
        {
            // do nothing.  a missile is already flying!
        }

        public override void MoveRight(Ship shipArg)
        {
            if (shipArg.x < 857)
            {
                shipArg.x += shipArg.shipSpeed;
            }
        }

        public override void MoveLeft(Ship shipArg)
        {
            if (shipArg.x > 40)
            {
                shipArg.x -= shipArg.shipSpeed;
            }
        }

        public override void Handle(Ship shipArg)
        {
            
        }
    }
}
