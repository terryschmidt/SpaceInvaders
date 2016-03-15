using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class PCSTreeIterator
    {
        // data:
        private static GameObject root;
        private static GameObject current;
        private static GameObject wrongParent;

        public static void CalculateIterators(GameObject pRootNode)
        {
            // FIX Todo have this backed into PCSTree

            Debug.Assert(pRootNode != null);
            PCSTreeIterator.root = pRootNode;
            PCSTreeIterator.wrongParent = (GameObject)pRootNode.pParent;

            PCSTreeIterator.current = PCSTreeIterator.root;

            GameObject pPrevGameObj = (GameObject)pRootNode;
            // Initialize the reserve pointer
            GameObject pGameObj = (GameObject)pRootNode;


            while (pGameObj != null)
            {
                // fill the basis
                pPrevGameObj = pGameObj;

                // Advance
                pGameObj = PCSTreeIterator.secretNext();
                pPrevGameObj.pForward = pGameObj;

                if (pGameObj != null)
                {
                    pGameObj.pReverse = pPrevGameObj;
                }
            }


            //    pRootNode.pForward.pReverse = pRootNode;
            pRootNode.pReverse = pPrevGameObj;

        }

        private static GameObject secretNext()
        {
            PCSTreeIterator.current = getNext(PCSTreeIterator.current);

            return (GameObject)PCSTreeIterator.current;
        }

        private static GameObject getNext(GameObject node, bool UseChild = true)
        {
            GameObject tmp = null;

            Debug.Assert(node != null);

            if ((node.pChild != null) && UseChild)
            {
                tmp = (GameObject)node.pChild;
            }
            else if (node.pSibling != null)
            {
                tmp = (GameObject)node.pSibling;
            }
            else if (node.pParent != PCSTreeIterator.root && node.pParent != PCSTreeIterator.wrongParent)
            {
                // recurse
                tmp = PCSTreeIterator.getNext((GameObject)node.pParent, false);
            }
            else
            {
                tmp = null;
            }
            return tmp;
        }
    }
}
