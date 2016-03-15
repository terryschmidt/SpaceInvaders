using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract class PCSNode
    {
        // Constructors: --------------------------------
        public PCSNode()
        {
            this.pParent = null;
            this.pChild = null;
            this.pSibling = null;
            this.pForward = null;
            this.pReverse = null;
        }

        ~PCSNode()
        {
#if(TRACK_DESTRUCTOR)
            Debug.WriteLine("     ~PCSNode():{0}", this.GetHashCode());
#endif
            this.pParent = null;
            this.pChild = null;
            this.pSibling = null;
            this.pReverse = null;
            this.pForward = null;
        }

        public PCSNode(PCSNode pNode)
        {
            this.pParent = pNode.pParent;
            this.pChild = pNode.pChild;
            this.pSibling = pNode.pSibling;
            this.pReverse = pNode.pReverse;
            this.pForward = pNode.pForward;
        }

        public PCSNode(PCSNode pParent, PCSNode pChild, PCSNode pSibling, PCSNode pForward, PCSNode pReverse)
        {
            this.pParent = pParent;
            this.pChild = pChild;
            this.pSibling = pSibling;
            this.pForward = pForward;
            this.pReverse = pReverse;
        }

        // Methods: set/get -------------------------------

        abstract public Enum getName();
        abstract public int getIndex();

        // Methods: Dump ------------------

        public void dumpNode()
        {
            Debug.WriteLine("");
            Debug.WriteLine("    name: {0} {1}", this.getName(), this.GetHashCode());
            if (this.pParent != null)
            {
                Debug.WriteLine("  parent: {0} {1}", this.pParent.getName(), this.pParent.GetHashCode());
            }
            else
            {
                Debug.WriteLine("  parent: ------");
            }
            if (this.pChild != null)
            {
                Debug.WriteLine("   child: {0} {1}", this.pChild.getName(), this.pChild.GetHashCode());
            }
            else
            {
                Debug.WriteLine("   child: ------");
            }
            if (this.pSibling != null)
            {
                Debug.WriteLine(" sibling: {0} {1}", this.pSibling.getName(), this.pSibling.GetHashCode());
            }
            else
            {
                Debug.WriteLine(" sibling: ------");
            }

        }

        // data ----------------------
        public PCSNode pParent;
        public PCSNode pChild;
        public PCSNode pSibling;
        public PCSNode pForward;
        public PCSNode pReverse;
    }
}
