using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class InputSubject
    {
        // data:
        private InputObserver head;

        public void Attach(InputObserver o)
        {
            Debug.Assert(o != null);

            o.subject = this;

            if (head == null)
            {
                head = o;
                o.next = null;
                o.prev = null;
            }
            else
            {
                o.next = head;
                o.prev = null;
                head.prev = o;
                head = o;
            }
        }

        public void Notify()
        {
            InputObserver node = this.head;

            while (node != null)
            {
                node.Notify();
                node = (InputObserver)node.next;
            }
        }

        public void Detach()
        {

        }
    }
}
