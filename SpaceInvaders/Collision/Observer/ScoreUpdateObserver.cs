using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ScoreUpdateObserver : CollisionObserver
    {
        public ScoreUpdateObserver()
        {
            
        }

        public override void Notify()
        {
            GameObject a = this.subject.gameObjA;
            GameObject b = this.subject.gameObjB;

            if (a is AlienCategory)
            {
                if (a is Octopus)
                {
                    Values.player1score += 10;
                    Font score = FontManager.Find(Font.Name.Score1);
                    score.changeMessageTo(Values.player1score.ToString());
                    IncrementPlayerLivesObserver.Notify();
                }

                if (a is Crab)
                {
                    Values.player1score += 20;
                    Font score = FontManager.Find(Font.Name.Score1);
                    score.changeMessageTo(Values.player1score.ToString());
                    IncrementPlayerLivesObserver.Notify();
                }

                if (a is Squid)
                {
                    Values.player1score += 30;
                    Font score = FontManager.Find(Font.Name.Score1);
                    score.changeMessageTo(Values.player1score.ToString());
                    IncrementPlayerLivesObserver.Notify();
                }
            } 
            else if (b is AlienCategory)
            {   
                if (b is Octopus)
                {
                    Values.player1score += 10;
                    Font score = FontManager.Find(Font.Name.Score1);
                    score.changeMessageTo(Values.player1score.ToString());
                    IncrementPlayerLivesObserver.Notify();
                }

                if (b is Crab)
                {
                    Values.player1score += 20;
                    Font score = FontManager.Find(Font.Name.Score1);
                    score.changeMessageTo(Values.player1score.ToString());
                    IncrementPlayerLivesObserver.Notify();
                }

                if (b is Squid)
                {
                    Values.player1score += 30;
                    Font score = FontManager.Find(Font.Name.Score1);
                    score.changeMessageTo(Values.player1score.ToString());
                    IncrementPlayerLivesObserver.Notify();
                }
            }
        }
    }
}
