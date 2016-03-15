using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShipStateReady : ShipState
    {
        public override void ShootMissile(Ship shipArg)
        {
            Missile missileToShoot = ShipManager.ActivateMissile();

            missileToShoot.SetPos(shipArg.x, shipArg.y + 20);
            missileToShoot.SetActive(true);
            SpaceInvaders.eng.Play2D("shoot.wav");

            this.Handle(shipArg);
        }

        public override void Handle(Ship shipArg)
        {
            shipArg.SetState(ShipManager.State.MissileFlying);
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
    }
}
