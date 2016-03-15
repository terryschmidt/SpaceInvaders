using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class CLink
    {
        // data:
        public CLink next;
        public CLink prev;
        public Status status;

        public enum Status
        {
            Active,
            Reserve,
            Uninitialized
        }

        protected CLink()
        {
            Clear();
        }

        public void Clear()
        {
            next = null;
            prev = null;
            status = Status.Uninitialized;
        }
    }
}
