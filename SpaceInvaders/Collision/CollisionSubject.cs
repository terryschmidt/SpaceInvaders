using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class CollisionSubject
    {
        // data:
        private CollisionObserver head;
        public GameObject gameObjA;
        public GameObject gameObjB;

        public CollisionSubject()
        {
            this.gameObjA = null;
            this.gameObjB = null;
            this.head = null;
        }

        ~CollisionSubject()
        {
            this.gameObjA = null;
            this.gameObjB = null;
            this.head = null;
        }

        public void Attach(CollisionObserver observer)
        {
            Debug.Assert(observer != null);

            observer.subject = this;

            if (head == null)
            {
                head = observer;
                observer.next = null;
                observer.prev = null;
            }
            else
            {
                observer.next = head;
                head.prev = observer;
                head = observer;
            }
        }

        public void Notify()
        {
            CollisionObserver node = this.head;

            while (node != null)
            {
                node.Notify();
                node = (CollisionObserver)node.next;
            }
        }

        public void Detach()
        {

        }
    }
}
