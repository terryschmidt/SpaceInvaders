using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Values
    {
        static public int player1score = 0;
        static public int player1lives = 3;
        static public int highestScore = 0;
        static public int columnCount = 11;
        static public Boolean ufoIsActive = false;
        static public Boolean onStartScreen = true;
        static public Boolean firstRun = true;
        static public Boolean ufoAdded = false;
        static public float startingGridMovementInterval = 0.5f;
        static public float gridMovementInterval = 0.5f;
        static public float originalHighestYPositionOfAlien = 815.0f;
        static public float currentHighestYPositionOfAlien = 815.0f;
        static public int alienCount = 55;
        static public float UFOspeed = 3.0f;

        public static double getRandom(double min, double max)
        {
            return min + SpaceInvaders.randy.NextDouble() * (max - min);
        }

        static public void AdjustGridMovementInterval() {
            alienCount--;
            if (alienCount >= 50 && alienCount <= 55)
            {
                gridMovementInterval = startingGridMovementInterval;
            }
            else if (alienCount >= 45 && alienCount <= 49)
            {
                //gridMovementInterval = 0.45f;
                gridMovementInterval *= 0.95f;
            }
            else if (alienCount >= 40 && alienCount <= 44)
            {
                //gridMovementInterval = 0.40f;
                gridMovementInterval *= 0.95f;
            }
            else if (alienCount >= 35 && alienCount <= 39)
            {
                //gridMovementInterval = 0.35f;
                gridMovementInterval *= 0.95f;
            }
            else if (alienCount >= 30 && alienCount <= 34)
            {
                //gridMovementInterval = 0.29f;
                gridMovementInterval *= 0.95f;
            }
            else if (alienCount >= 25 && alienCount <= 29)
            {
                //gridMovementInterval = 0.26f;
                gridMovementInterval *= 0.95f;
            }
            else if (alienCount >= 20 && alienCount <= 24)
            {
                //gridMovementInterval = 0.21f;
                gridMovementInterval *= 0.95f;
            }
            else if (alienCount >= 15 && alienCount <= 19)
            {
                //gridMovementInterval = 0.16f;
                gridMovementInterval *= 0.95f;
            }
            else if (alienCount >= 10 && alienCount <= 14)
            {
                //gridMovementInterval = 0.12f;
                gridMovementInterval *= 0.95f;
            }
            else if (alienCount >= 5 && alienCount <= 9)
            {
                //gridMovementInterval = 0.08f;
                gridMovementInterval *= 0.95f;
            }
            else if (alienCount >= 3 && alienCount <= 4)
            {
                //gridMovementInterval = 0.05f;
                gridMovementInterval *= 0.95f;
            }
            else if (alienCount == 2)
            {
                //gridMovementInterval = 0.03f;
                gridMovementInterval *= 0.95f;
            }
            else if (alienCount == 1)
            {
                gridMovementInterval = 0.01f;
                //gridMovementInterval *= 0.95f;
            }
        }
    }
}
