using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract class ShipState
    {
        public abstract void ShootMissile(Ship shipArg);
        public abstract void Handle(Ship shipArg);
        public abstract void MoveRight(Ship shipArg);
        public abstract void MoveLeft(Ship shipArg);
    }
}
