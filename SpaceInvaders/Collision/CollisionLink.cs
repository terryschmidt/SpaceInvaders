using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract class CollisionLink
    {
        // data:
        public CollisionLink next;
        public CollisionLink prev;

        protected CollisionLink()
        {
            this.baseInitialize();
        }

        protected void baseInitialize()
        {
            this.next = null;
            this.prev = null;
        }
    }
}
