using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract class CollisionObserver : CollisionLink
    {
        public abstract void Notify();
        public virtual void Execute()
        {

        }
        public CollisionSubject subject;
    }
}
