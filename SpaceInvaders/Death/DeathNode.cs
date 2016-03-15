using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class DeathNode : MLink
    {
        // data:
        public object obj;

        public DeathNode()
            : base()
        {
            this.obj = null;
        }

        ~DeathNode()
        {
            this.obj = null;
        }

        public void Set(object objArg)
        {
            Debug.Assert(objArg != null);
            this.obj = objArg;
        }

        public void Dump()
        {
            
        }
    }
}
