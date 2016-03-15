using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public static class IncrementPlayerLivesObserver
    {
        static Boolean crossed1000 = false;
        static Boolean crossed2000 = false;
        static Boolean crossed3000 = false;
        static Boolean crossed4000 = false;
        static Boolean crossed5000 = false;
        static Boolean crossed6000 = false;
        static Boolean crossed7000 = false;
        static Boolean crossed8000 = false;
        static Boolean crossed9000 = false;
        static Boolean crossed10000 = false;
        static Boolean crossed11000 = false;
        static Boolean crossed12000 = false;
        static Boolean crossed13000 = false;
        static Boolean crossed14000 = false;
        static Boolean crossed15000 = false;
        static Boolean crossed16000 = false;
        static Boolean crossed17000 = false;
        static Boolean crossed18000 = false;
        static Boolean crossed19000 = false;
        static Boolean crossed20000 = false;
        static Boolean crossed21000 = false;
        static Boolean crossed22000 = false;
        static Boolean crossed23000 = false;
        static Boolean crossed24000 = false;
        static Boolean crossed25000 = false;
        static Boolean crossed26000 = false;
        static Boolean crossed27000 = false;
        static Boolean crossed28000 = false;
        static Boolean crossed29000 = false;
        static Boolean crossed30000 = false;

        // ugly.  figure out a better way
        public static void Notify()
        {
            if (Values.player1score >= 1000 && crossed1000 == false)
            {
                Values.player1lives++;
                Font lives = FontManager.Find(Font.Name.Lives1);
                lives.changeMessageTo(Values.player1lives.ToString());
                crossed1000 = true;
                return;
            } 

            if (Values.player1score >= 2000 && crossed2000 == false)
            {
                Values.player1lives++;
                Font lives = FontManager.Find(Font.Name.Lives1);
                lives.changeMessageTo(Values.player1lives.ToString());
                crossed2000 = true;
                return;
            } 

            if (Values.player1score >= 3000 && crossed3000 == false)
            {
                Values.player1lives++;
                Font lives = FontManager.Find(Font.Name.Lives1);
                lives.changeMessageTo(Values.player1lives.ToString());
                crossed3000 = true;
                return;
            }

            if (Values.player1score >= 4000 && crossed4000 == false)
            {
                Values.player1lives++;
                Font lives = FontManager.Find(Font.Name.Lives1);
                lives.changeMessageTo(Values.player1lives.ToString());
                crossed4000 = true;
                return;
            }

            if (Values.player1score >= 5000 && crossed5000 == false)
            {
                Values.player1lives++;
                Font lives = FontManager.Find(Font.Name.Lives1);
                lives.changeMessageTo(Values.player1lives.ToString());
                crossed5000 = true;
                return;
            }

            if (Values.player1score >= 6000 && crossed6000 == false)
            {
                Values.player1lives++;
                Font lives = FontManager.Find(Font.Name.Lives1);
                lives.changeMessageTo(Values.player1lives.ToString());
                crossed6000 = true;
                return;
            }

            if (Values.player1score >= 7000 && crossed7000 == false)
            {
                Values.player1lives++;
                Font lives = FontManager.Find(Font.Name.Lives1);
                lives.changeMessageTo(Values.player1lives.ToString());
                crossed7000 = true;
                return;
            }

            if (Values.player1score >= 8000 && crossed8000 == false)
            {
                Values.player1lives++;
                Font lives = FontManager.Find(Font.Name.Lives1);
                lives.changeMessageTo(Values.player1lives.ToString());
                crossed8000 = true;
                return;
            }

            if (Values.player1score >= 9000 && crossed9000 == false)
            {
                Values.player1lives++;
                Font lives = FontManager.Find(Font.Name.Lives1);
                lives.changeMessageTo(Values.player1lives.ToString());
                crossed9000 = true;
                return;
            }

            if (Values.player1score >= 10000 && crossed10000 == false)
            {
                Values.player1lives++;
                Font lives = FontManager.Find(Font.Name.Lives1);
                lives.changeMessageTo(Values.player1lives.ToString());
                crossed10000 = true;
                return;
            }

            if (Values.player1score >= 11000 && crossed11000 == false)
            {
                Values.player1lives++;
                Font lives = FontManager.Find(Font.Name.Lives1);
                lives.changeMessageTo(Values.player1lives.ToString());
                crossed11000 = true;
                return;
            }

            if (Values.player1score >= 12000 && crossed12000 == false)
            {
                Values.player1lives++;
                Font lives = FontManager.Find(Font.Name.Lives1);
                lives.changeMessageTo(Values.player1lives.ToString());
                crossed12000 = true;
                return;
            }

            if (Values.player1score >= 13000 && crossed13000 == false)
            {
                Values.player1lives++;
                Font lives = FontManager.Find(Font.Name.Lives1);
                lives.changeMessageTo(Values.player1lives.ToString());
                crossed13000 = true;
                return;
            }

            if (Values.player1score >= 14000 && crossed14000 == false)
            {
                Values.player1lives++;
                Font lives = FontManager.Find(Font.Name.Lives1);
                lives.changeMessageTo(Values.player1lives.ToString());
                crossed14000 = true;
                return;
            }

            if (Values.player1score >= 15000 && crossed15000 == false)
            {
                Values.player1lives++;
                Font lives = FontManager.Find(Font.Name.Lives1);
                lives.changeMessageTo(Values.player1lives.ToString());
                crossed15000 = true;
                return;
            }

            if (Values.player1score >= 16000 && crossed16000 == false)
            {
                Values.player1lives++;
                Font lives = FontManager.Find(Font.Name.Lives1);
                lives.changeMessageTo(Values.player1lives.ToString());
                crossed16000 = true;
                return;
            }

            if (Values.player1score >= 17000 && crossed17000 == false)
            {
                Values.player1lives++;
                Font lives = FontManager.Find(Font.Name.Lives1);
                lives.changeMessageTo(Values.player1lives.ToString());
                crossed17000 = true;
                return;
            }

            if (Values.player1score >= 18000 && crossed18000 == false)
            {
                Values.player1lives++;
                Font lives = FontManager.Find(Font.Name.Lives1);
                lives.changeMessageTo(Values.player1lives.ToString());
                crossed18000 = true;
                return;
            }

            if (Values.player1score >= 19000 && crossed19000 == false)
            {
                Values.player1lives++;
                Font lives = FontManager.Find(Font.Name.Lives1);
                lives.changeMessageTo(Values.player1lives.ToString());
                crossed19000 = true;
                return;
            }

            if (Values.player1score >= 20000 && crossed20000 == false)
            {
                Values.player1lives++;
                Font lives = FontManager.Find(Font.Name.Lives1);
                lives.changeMessageTo(Values.player1lives.ToString());
                crossed20000 = true;
                return;
            }

            if (Values.player1score >= 21000 && crossed21000 == false)
            {
                Values.player1lives++;
                Font lives = FontManager.Find(Font.Name.Lives1);
                lives.changeMessageTo(Values.player1lives.ToString());
                crossed21000 = true;
                return;
            }

            if (Values.player1score >= 22000 && crossed22000 == false)
            {
                Values.player1lives++;
                Font lives = FontManager.Find(Font.Name.Lives1);
                lives.changeMessageTo(Values.player1lives.ToString());
                crossed22000 = true;
                return;
            }

            if (Values.player1score >= 23000 && crossed23000 == false)
            {
                Values.player1lives++;
                Font lives = FontManager.Find(Font.Name.Lives1);
                lives.changeMessageTo(Values.player1lives.ToString());
                crossed23000 = true;
                return;
            }

            if (Values.player1score >= 24000 && crossed24000 == false)
            {
                Values.player1lives++;
                Font lives = FontManager.Find(Font.Name.Lives1);
                lives.changeMessageTo(Values.player1lives.ToString());
                crossed24000 = true;
                return;
            }

            if (Values.player1score >= 25000 && crossed25000 == false)
            {
                Values.player1lives++;
                Font lives = FontManager.Find(Font.Name.Lives1);
                lives.changeMessageTo(Values.player1lives.ToString());
                crossed25000 = true;
                return;
            }

            if (Values.player1score >= 26000 && crossed26000 == false)
            {
                Values.player1lives++;
                Font lives = FontManager.Find(Font.Name.Lives1);
                lives.changeMessageTo(Values.player1lives.ToString());
                crossed26000 = true;
                return;
            }

            if (Values.player1score >= 27000 && crossed27000 == false)
            {
                Values.player1lives++;
                Font lives = FontManager.Find(Font.Name.Lives1);
                lives.changeMessageTo(Values.player1lives.ToString());
                crossed27000 = true;
                return;
            }

            if (Values.player1score >= 28000 && crossed28000 == false)
            {
                Values.player1lives++;
                Font lives = FontManager.Find(Font.Name.Lives1);
                lives.changeMessageTo(Values.player1lives.ToString());
                crossed28000 = true;
                return;
            }

            if (Values.player1score >= 29000 && crossed29000 == false)
            {
                Values.player1lives++;
                Font lives = FontManager.Find(Font.Name.Lives1);
                lives.changeMessageTo(Values.player1lives.ToString());
                crossed29000 = true;
                return;
            }

            if (Values.player1score >= 30000 && crossed30000 == false)
            {
                Values.player1lives++;
                Font lives = FontManager.Find(Font.Name.Lives1);
                lives.changeMessageTo(Values.player1lives.ToString());
                crossed30000 = true;
                return;
            }
        }
    }
}
