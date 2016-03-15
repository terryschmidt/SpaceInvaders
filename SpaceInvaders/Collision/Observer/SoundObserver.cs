using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SoundObserver : CollisionObserver
    {
        // data:
        IrrKlang.ISoundEngine engine;

        public SoundObserver(IrrKlang.ISoundEngine engineArg)
        {
            Debug.Assert(engineArg != null);
            this.engine = engineArg;
        }

        public override void Notify()
        {
            //Debug.WriteLine("SndObserver: {0} {1}", this.subject.gameObjA, this.subject.gameObjB);

            engine.SoundVolume = 0.3f;
            //engine.Play2D("fastinvader4.wav");
        }
    }
}
