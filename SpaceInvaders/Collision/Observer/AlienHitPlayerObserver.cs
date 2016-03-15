using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class AlienHitPlayerObserver : CollisionObserver
    {
        ShipSplat s;
        PCSTree tree;

        public AlienHitPlayerObserver()
        {
            s = new ShipSplat(GameObject.Name.ShipSplat, GameSprite.Name.ShipSplat, 0, -500, -500);
            tree = new PCSTree();
        }

        public override void Notify()
        {
            GameObject a = this.subject.gameObjA;
            GameObject b = this.subject.gameObjB;
            s.ActivateGameSprite(SpriteBatchManager.Find(SpriteBatch.Name.Aliens));

            if (a is Ship)
            {
                s.x = a.x;
                s.y = a.y;
                s.Update();
                GameObjectManager.AttachTree(s, tree);
                a.x = -500;
                a.y = -500;
            }

            if (b is Ship)
            {
                s.x = b.x;
                s.y = b.y;
                s.Update();
                GameObjectManager.AttachTree(s, tree);
                b.x = -500;
                b.y = -500;
            }

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

            Ship shippy = ShipManager.GetShip();
            shippy.SetState(ShipManager.State.End);
        }
    }
}
