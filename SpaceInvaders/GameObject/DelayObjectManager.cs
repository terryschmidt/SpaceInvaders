using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class DelayObjectManager
    {
        // data:
        private CollisionObserver head;
        private static DelayObjectManager instance = null;

        static public void Attach(CollisionObserver observer)
        {
            Debug.Assert(observer != null);

            DelayObjectManager inst = DelayObjectManager.getInstance();

            if (inst.head == null)
            {
                inst.head = observer;
                observer.next = null;
                observer.prev = null;
            }
            else
            {
                observer.next = inst.head;
                observer.prev = null;
                inst.head.prev = observer;
                inst.head = observer;
            }
        }

        private void Detach(CollisionObserver node, ref CollisionObserver head)
        {
            Debug.Assert(node != null);

            if (node.prev != null)
            {
                node.prev.next = node.next;
            }
            else
            {
                head = (CollisionObserver)node.next;
            }

            if (node.next != null)
            {
                node.next.prev = node.prev;
            }
        }

        static public void Process()
        {
            DelayObjectManager inst = DelayObjectManager.getInstance();

            CollisionObserver node = inst.head;

            while (node != null)
            {
                node.Execute();
                node = (CollisionObserver)node.next;
            }

            //remove
            node = inst.head;
            CollisionObserver temp = null;

            while (node != null)
            {
                temp = node;
                node = (CollisionObserver)node.next;
                inst.Detach(temp, ref inst.head);
            }
        }

        private DelayObjectManager()
        {
            this.head = null;
        }

        private static DelayObjectManager getInstance()
        {
            if (instance == null)
            {
                instance = new DelayObjectManager();
            }

            Debug.Assert(instance != null);

            return instance;
        }
    }
}
