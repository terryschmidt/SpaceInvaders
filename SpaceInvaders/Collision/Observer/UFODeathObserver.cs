using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class UFODeathObserver : CollisionObserver
    {
        public override void Notify()
        {
            Values.ufoIsActive = false;
            SpaceInvaders.eng.StopAllSounds();
            SpaceInvaders.eng.Play2D("ufo_lowpitch.wav");
            int random = SpaceInvaders.randy.Next(1, 4);
            Debug.WriteLine("Random value generated for UFO kill: " + random);
            int pointsToAdd = 0;

            if (random == 1)
            {
                pointsToAdd = 100;
            }

            if (random == 2)
            {
                pointsToAdd = 150;
            }

            if (random == 3)
            {
                pointsToAdd = 50;
            }

            Font score = FontManager.Find(Font.Name.Score1);
            Debug.WriteLine("Adding points for UFO: " + pointsToAdd);
            int newScore = Values.player1score + pointsToAdd;
            score.changeMessageTo(newScore.ToString());
            Values.player1score = newScore;
            IncrementPlayerLivesObserver.Notify();
        }
    }
}
