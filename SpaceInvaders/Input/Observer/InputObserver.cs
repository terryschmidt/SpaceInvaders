using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract class InputObserver : CollisionLink
    {
        abstract public void Notify();
        public InputSubject subject;
    }
}
