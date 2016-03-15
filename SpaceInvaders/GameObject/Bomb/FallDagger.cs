using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class FallDagger : FallStrategy
    {
        // data:
        private float oldPosY;

        public FallDagger()
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
            float targetY = oldPosY - 1.0f * bomb.GetBoundingBoxHeight();

            if (bomb.y < targetY)
            {
                bomb.MultiplyScale(1.0f, -1.0f);
                oldPosY = targetY;
            }
        }
    }
}
