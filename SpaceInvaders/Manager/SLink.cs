using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    abstract class SLink
    {
        protected SLink()
        {
            next = null;
        }

        public SLink next;
    }
}
