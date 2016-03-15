using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class TimerEvent : MLink
    {
        // data:
        public TimerEvent.Name name;
        public Command command;
        public float triggerEventTime;
        public float deltaTime;

        public enum Name
        {
            SpriteAnimation,
            GridMovement,
            BombDrop,
            SplatRemove,
            MissileBombSplatRemove,
            ShipSplatRemove,
            UFOSplatRemove,
            MissileWallSplatRemove,
            MissileBombSplatRemove2,
            SplatRemove2,
            SplatRemove3,
            SplatRemove4,
            SplatRemove5,
            ShipUnpause,
            UFOSpawn,
            NewWave,
            Uninitialized
        }

        public TimerEvent()
            : base()
        {
            this.name = TimerEvent.Name.Uninitialized;
            this.command = null;
            this.triggerEventTime = 0.0f;
            this.deltaTime = 0.0f;
        }

        ~TimerEvent()
        {
            this.name = TimerEvent.Name.Uninitialized;
            this.command = null;
        }

        public void Set(TimerEvent.Name timerEventName, Command comm, float deltaTimeToTrigger)
        {
            this.name = timerEventName;
            this.command = comm;
            this.deltaTime = deltaTimeToTrigger;
            this.triggerEventTime = TimerManager.getCurrentTime() + deltaTimeToTrigger;
        }

        public void Process()
        {
            Debug.Assert(this.command != null);
            this.command.execute(deltaTime);
        }
    }
}
