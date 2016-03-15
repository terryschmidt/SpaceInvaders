using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class FallStraight : FallStrategy
    {
        private float oldPosY;

        public FallStraight()
        {
            this.oldPosY = 0.0f;
        }

        public override void Reset(float yArg)
        {
            this.oldPosY = yArg;
        }

        public override void Fall(Bomb bomb)
        {
            Debug.Assert(bomb != null);
            // do nothing for fall straight strategy
        }
    }
}
