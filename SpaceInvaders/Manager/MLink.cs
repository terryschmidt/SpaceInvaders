using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class MLink
    {
        // data:
        public MLink next;
        public MLink prev;
        public Status status;

        public enum Status
        {
            Active,
            Reserve,
            Uninitialized
        }

        protected MLink()
        {
            this.Clear();
        }

        public void Clear()
        {
            next = null;
            prev = null;
            status = Status.Uninitialized;
        }
    }
}
