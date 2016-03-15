using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class NewWaveObserver : CollisionObserver
    {
        NewWaveMaker nwm;

        public NewWaveObserver()
        {
            nwm = new NewWaveMaker();
        }

        public override void Notify()
        {
            if (Values.alienCount == 0)
            {
                TimerManager.Remove(TimerManager.Find(TimerEvent.Name.GridMovement));
                // reset grid movement to slightly faster (slightly harder) for next wave.
                Values.startingGridMovementInterval *= 0.96f;
                Values.gridMovementInterval = Values.startingGridMovementInterval;
                Values.alienCount = 55;
                Values.currentHighestYPositionOfAlien *= 0.96f; // aliens getting slightly closer each wave.
                TimerManager.Add(TimerEvent.Name.NewWave, this.nwm, 0.1f);
                // pause all timers for X seconds
                TimerManager.Wait(2.5f);
            }
        }
    }
}
