using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract class Iterator
    {
        public abstract PCSNode First();
        public abstract PCSNode Next();
        public abstract bool IsDone();
        public abstract PCSNode CurrentItem();
    }
}
