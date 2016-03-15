using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class GridHitBottomObserver : CollisionObserver
    {
        public GridHitBottomObserver()
        {

        }

        public override void Notify()
        {
            Grid grid = (Grid)this.subject.gameObjA;
            PCSTreeForwardIterator iter = new PCSTreeForwardIterator(grid);
            Debug.Assert(iter != null);

            PCSNode pNode = iter.First();

            while (!iter.IsDone())
            {
                // delta
                GameObject pGameObj = (GameObject)pNode;
                pGameObj.y += 25.0f;


                // Advance
                pNode = iter.Next();
            }

            Ship shippy = ShipManager.GetShip();
            shippy.x = -500;
            shippy.y = -500;
            Font playerLives = FontManager.Find(Font.Name.Lives1);
            playerLives.changeMessageTo("0");
            SpaceInvaders.eng.Play2D("explosion.wav");
            TimerManager.Wait(99999);
            FontManager.Add(Font.Name.GameOver, SpriteBatch.Name.Texts, "GAME OVER", Character.Name.Consolas36pt, 350, 875);
            Font credit = FontManager.Find(Font.Name.Credits);
            credit.changeMessageTo("CREDITS 00");
            //Font start = FontManager.Find(Font.Name.SpaceInvaders);
            //start.changeMessageTo("GAME OVER");
            //FontManager.Add(Font.Name.RestartMessage, SpriteBatch.Name.Texts, "press R to restart", Character.Name.Consolas36pt, 350, 350);

            if (Values.player1score > Values.highestScore)
            {
                Font high = FontManager.Find(Font.Name.HighScore);
                high.changeMessageTo(Values.player1score.ToString());
                Values.highestScore = Values.player1score;
                Values.player1score = 0;
            }

            shippy.SetState(ShipManager.State.End);
        }
    }
}
