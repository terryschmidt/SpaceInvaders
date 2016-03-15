using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class GameObjectManager : Manager
    {
        // data:
        private static GameObjectManager instance = null;
        private GameObjectNode referenceNode;
        protected PCSTree root;
        
        private GameObjectManager(int reserveNum = 3, int reserveGrow = 1)
            : base(reserveNum, reserveGrow)
        {
            this.referenceNode = (GameObjectNode)this.CreateNode();
            Debug.Assert(this.referenceNode != null);

            this.referenceNode.gameObject = new NullGameObject();
            Debug.Assert(this.referenceNode.gameObject != null);

            this.root = new PCSTree();
            Debug.Assert(this.root != null);
        }

        ~GameObjectManager()
        {
            this.referenceNode = null;
            GameObjectManager.instance = null;
            this.root = null;
        }

        public static void Destroy()
        {
            GameObjectManager inst = GameObjectManager.getInstance();
            inst.baseDestroy();
        }

        public static void Create(int reserveNum = 3, int reserveGrow = 1)
        {
            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);
            Debug.Assert(instance == null);

            if (instance == null)
            {
                instance = new GameObjectManager(reserveNum, reserveGrow);
            }
        }

        public static PCSTree GetRootTree()
        {
            GameObjectManager inst = GameObjectManager.getInstance();
            return inst.root;
        }

        public static GameObjectNode AttachTree(GameObject pGameObject)
        {
            Debug.Assert(pGameObject != null);

            GameObjectManager inst = GameObjectManager.getInstance();

            GameObjectNode pNode = (GameObjectNode)inst.baseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(pGameObject);
            return pNode;
        }

        public static GameObjectNode AttachTree(GameObject gameObj, PCSTree treeArg)
        {
            Debug.Assert(gameObj != null);
            GameObjectManager inst = GameObjectManager.getInstance();
            GameObjectNode node = (GameObjectNode)inst.baseAdd();
            Debug.Assert(node != null);
            Debug.Assert(treeArg != null);
            node.Set(gameObj, treeArg);
            return node;
        }

        public static void Remove(GameObjectNode nodeArg)
        {
            Debug.Assert(nodeArg != null);
            GameObjectManager inst = GameObjectManager.getInstance();
            inst.baseRemove(nodeArg);
        }

        public static void Remove(GameObject pNode)
        {
            Debug.Assert(pNode != null);
            GameObjectManager inst = GameObjectManager.getInstance();

            GameObject pSafetyNode = pNode;

            // OK so we have a linked list of trees (Remember that)

            // 1) find the tree (we already know its the most parent)

            GameObject pTmp = pNode;
            GameObject pRoot = null;
            while (pTmp != null)
            {
                pRoot = pTmp;
                pTmp = (GameObject)pTmp.pParent;
            }

            // 2) pRoot is the tree we are looking for
            // now walk the active list looking for pRoot

            GameObjectNode pTree = (GameObjectNode)inst.active;

            while (pTree != null)
            {
                if (pTree.gameObject == pRoot)
                {
                    // found it
                    break;
                }
                // Goto Next tree
                pTree = (GameObjectNode)pTree.next;
            }

            // 3) pTree is the tree that holds pNode
            //  Now remove it

            Debug.Assert(pTree != null);
            Debug.Assert(pTree.gameObject != null);
            inst.root.SetRoot(pTree.gameObject);
            inst.root.Remove(pNode);

        }

        protected override Boolean Compare(MLink first, MLink second)
        {
            /*Debug.Assert(first != null);
            Debug.Assert(second != null);

            GameObjectNode firstGameObj = (GameObjectNode)first;
            Debug.Assert(firstGameObj != null);

            GameObjectNode secondGameObj = (GameObjectNode)second;
            Debug.Assert(secondGameObj != null);

            if (firstGameObj.gameObject.name == secondGameObj.gameObject.name)
            {
                return true;
            }
            else
            {
                return false;
            }*/

            Debug.Assert(false);
            return false;
        }

        protected override void DumpNode(MLink link)
        {
            Debug.Assert(link != null);
            GameObjectNode node = (GameObjectNode)link;
            Debug.Assert(node != null);
            node.Dump();
        }

        public static void Insert(GameObject gameObjArg, GameObject parent)
        {
            GameObjectManager inst = GameObjectManager.getInstance();
            Debug.Assert(gameObjArg != null);

            if (parent == null)
            {
                GameObjectManager.AttachTree(gameObjArg, null);
            }
            else
            {
                Debug.Assert(parent != null);
                inst.root.SetRoot(parent);
                inst.root.Insert(gameObjArg, parent);
            }
        }

        public static void Update()
        {
            GameObjectManager inst = GameObjectManager.getInstance();
            GameObjectNode pRoot = (GameObjectNode)inst.active;

            while (pRoot != null)
            {
                // OK at this point, I have a Root tree,
                // need to walk the tree completely before moving to next tree

                //PCSTreeForwardIterator pIterator = new PCSTreeForwardIterator( pRoot.pGameObj );
                PCSTreeReverseIterator pIterator = new PCSTreeReverseIterator(pRoot.gameObject);

                // Initialize
                GameObject pGameObj = (GameObject)pIterator.First();

                //  Debug.WriteLine("-------");
                while (!pIterator.IsDone())
                {
                    //  Debug.WriteLine("  {0}", pGameObj.name);
                    pGameObj.Update();

                    // Advance
                    pGameObj = (GameObject)pIterator.Next();
                }

                // Goto Next tree
                pRoot = (GameObjectNode)pRoot.next;
            }
        }

        public static GameObject Find(GameObject.Name nameArg, int indexArg = 0)
        {
            GameObjectManager inst = GameObjectManager.getInstance();

            // Compare functions only compares two Nodes
            inst.referenceNode.gameObject.name = nameArg;
            inst.referenceNode.gameObject.index = indexArg;

            GameObjectNode pRoot = (GameObjectNode)inst.active;
            GameObject pGameObj = null;

            bool found = false;
            while (pRoot != null && found == false)
            {
                // OK at this point, I have a Root tree,
                // need to walk the tree completely before moving to next tree
                PCSTreeForwardIterator pIterator = new PCSTreeForwardIterator(pRoot.gameObject);

                // Initialize
                pGameObj = (GameObject)pIterator.First();

                while (!pIterator.IsDone())
                {
                    if ((pGameObj.name == inst.referenceNode.gameObject.name) && (pGameObj.index == inst.referenceNode.gameObject.index))
                    {
                        found = true;
                        break;
                    }

                    // Advance
                    pGameObj = (GameObject)pIterator.Next();
                }

                // Goto Next tree
                pRoot = (GameObjectNode)pRoot.next;
            }

            return pGameObj;
        }

        public static void Dump()
        {
            GameObjectManager inst = GameObjectManager.getInstance();
            inst.baseDump("GameObjectManager");
        }

        public static void DumpStats()
        {
            GameObjectManager inst = GameObjectManager.getInstance();
            inst.baseDumpStats("GameObjectManager");
        }

        protected override MLink CreateNode()
        {
            MLink node = new GameObjectNode();
            Debug.Assert(node != null);
            return node;
        }

        // private

        private static GameObjectManager getInstance()
        {
            Debug.Assert(instance != null);
            return instance;
        }
    }
}
