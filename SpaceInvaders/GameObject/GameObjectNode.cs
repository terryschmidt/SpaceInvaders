using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class GameObjectNode : MLink
    {
        // data:
        public GameObject gameObject;
        public PCSTree tree;

        public GameObjectNode() 
            : base()
        {
            this.gameObject = null;
        }

        ~GameObjectNode()
        {
            this.gameObject = null;
        }

        public void Set(GameObject gameObjArg, PCSTree treeArg)
        {
            Debug.Assert(gameObjArg != null);
            this.gameObject = gameObjArg;
            Debug.Assert(treeArg != null);
            this.tree = treeArg;
        }

        public void Set(GameObject pGameObject)
        {
            Debug.Assert(pGameObject != null);
            this.gameObject = pGameObject;
        }

        public void Reset()
        {
            this.gameObject = null;
        }

        public Enum getName()
        {
            return this.gameObject.name;
        }

        public void Dump()
        {
            Debug.Assert(this.gameObject != null);
            Debug.WriteLine("\t\t     GameObject: {0}", this.GetHashCode());
            this.gameObject.Dump();
        }
    }
}
