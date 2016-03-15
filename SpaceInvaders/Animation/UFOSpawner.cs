using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class UFOSpawner : Command
    {
        // data: 
        Random randy;

        public UFOSpawner()
        {
            randy = new Random();
        }

        public override void execute(float deltaTime)
        {
            this.playSound();
            int random = randy.Next(1, 100);

            if (random % 2 == 0)
            {
                Debug.WriteLine("Random UFO direction int: " + random);
                // spawn UFO on left side
                Values.UFOspeed = 3.0f;
                UFOMaker.makeUFORootAndUFO();
            }
            else
            {
                Debug.WriteLine("Random UFO direction int: " + random);
                // spawn ufo on right side
                Values.UFOspeed = -3.0f;
                UFOMaker.makeUFORootAndUFO();
            }


            // re-add ufo spawn event to timer for next round!
            double randomDouble = Values.getRandom(20.0, 40.0);
            float f = (float)randomDouble;
            Debug.WriteLine("Reseeding UFO event with value: " + f);
            TimerManager.Add(TimerEvent.Name.UFOSpawn, this, f);
        }

        public void playSound()
        {
            SpaceInvaders.eng.Play2D("ufosound.wav");
        }
    }
}
