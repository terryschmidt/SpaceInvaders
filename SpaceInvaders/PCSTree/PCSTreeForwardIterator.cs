using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class PCSTreeForwardIterator : Iterator
    {
        // data:
        private GameObject root;
        private GameObject current;

        public PCSTreeForwardIterator(GameObject rootNode)
        {
            Debug.Assert(rootNode != null);
            this.root = rootNode;
            this.current = this.root;

            PCSTreeIterator.CalculateIterators(rootNode);
        }

        public override PCSNode First()
        {
            this.current = this.root;
            return this.current;
        }

        public override PCSNode Next()
        {
            Debug.Assert(this.current != null);
            this.current = (GameObject)this.current.pForward;
            return this.current;
        }

        public override bool IsDone()
        {
            return (this.current == null);
        }

        public override PCSNode CurrentItem()
        {
            return this.current;
        }
    }
}
