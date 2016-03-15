using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class AlienDeathSoundObserver : CollisionObserver
    {
        public AlienDeathSoundObserver()
        {

        }

        public override void Notify()
        {
            SpaceInvaders.eng.Play2D("invaderkilled.wav");
        }
    }
}
