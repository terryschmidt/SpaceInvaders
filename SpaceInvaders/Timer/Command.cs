using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class Command
    {
        abstract public void execute(float deltaTime);
    }
}
