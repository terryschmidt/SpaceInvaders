using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class PCSTreeReverseIterator : Iterator
    {
        // data:
        private GameObject root;
        private GameObject current;
        private GameObject prev;

        public PCSTreeReverseIterator(GameObject rootNode)
        {
            Debug.Assert(rootNode != null);
            this.root = rootNode;
            this.current = this.root;

            PCSTreeIterator.CalculateIterators(rootNode);

        }

        public override PCSNode First()
        {
            this.current = (GameObject)this.root.pReverse;
            this.prev = this.current;
            return this.current;
        }

        public override PCSNode Next()
        {
            Debug.Assert(this.current != null);
            this.prev = this.current;
            this.current = (GameObject)this.current.pReverse;
            return this.current;
        }

        public override bool IsDone()
        {
            return ((this.current == this.root.pReverse) && (this.prev == this.root));
        }

        public override PCSNode CurrentItem()
        {
            return this.current;
        }
    }
}
