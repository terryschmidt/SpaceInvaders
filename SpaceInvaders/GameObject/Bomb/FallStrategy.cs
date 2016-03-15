using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract class FallStrategy
    {
        abstract public void Fall(Bomb bomb);
        abstract public void Reset(float yArg);
    }
}
