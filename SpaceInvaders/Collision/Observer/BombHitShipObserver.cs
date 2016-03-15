using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class BombHitShipObserver : CollisionObserver
    {
        ShipUnpauser su;

        public BombHitShipObserver()
        {
            su = new ShipUnpauser();
        }

        public override void Notify()
        {
            SpaceInvaders.eng.Play2D("explosion.wav");
            Ship shippy = ShipManager.GetShip();
            shippy.SetState(ShipManager.State.End);
            TimerManager.Add(TimerEvent.Name.ShipUnpause, su, 0.10f);
            TimerManager.Wait(2.5f);
            Values.player1lives--;
            Font lives = FontManager.Find(Font.Name.Lives1);
            lives.changeMessageTo(Values.player1lives.ToString());
            shippy.x = -500;
            shippy.y = -500;

            if (Values.player1lives == 0)
            {
                FontManager.Add(Font.Name.GameOver, SpriteBatch.Name.Texts, "GAME OVER", Character.Name.Consolas36pt, 350, 875);
                Font credit = FontManager.Find(Font.Name.Credits);
                credit.changeMessageTo("CREDITS 00");
                //Font start = FontManager.Find(Font.Name.SpaceInvaders);
                //start.changeMessageTo("GAME OVER");
                shippy.x = -500;
                shippy.y = -500;
                TimerManager.Wait(99999);
                if (Values.player1score > Values.highestScore)
                {
                    Font high = FontManager.Find(Font.Name.HighScore);
                    high.changeMessageTo(Values.player1score.ToString());
                    Values.highestScore = Values.player1score;
                    //Values.player1score = 0;
                }
                shippy.SetState(ShipManager.State.End);
            }
        }
    }
}
