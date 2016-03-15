using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class GhostNode : MLink
    {
        // data:
        public GameObject gameObject;

        public GhostNode()
            : base()
        {
            this.gameObject = null;
        }

        ~GhostNode()
        {
            this.gameObject = null;
        }

        public void Set(GameObject gameObjArg)
        {
            Debug.Assert(gameObjArg != null);
            this.gameObject = gameObjArg;
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
