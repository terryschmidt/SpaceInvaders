using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class InputTest
    {
        public static void KeyboardTest()
        {
	        // Quick and dirty test, if these work the rest do.
	        // ---> try a,s,d,<SPACE> keys
            //GameObject m = GameObjectManager.Find(GameObject.Name.Missile);

            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_RIGHT) && Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_LEFT) && Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_SPACE)) {
                Console.WriteLine("Left, Right and Space");
                // don't move ship at all.  left and right cancel each other out.
                // check if missile has already been fired
                // if not, fire missile
                // if so, do nothing
            } else if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_LEFT) && Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_SPACE)) {
                Console.WriteLine("Left and Space");
                // move ship left
                // check if missile has already been fired
                // if not, fire
                // if so, do nothing
            } else if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_RIGHT) && Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_SPACE)) {
                Console.WriteLine("Right and Space");
                // move ship right
                // check if missile has already been fired
                // if not, fire
                // if so, do nothing
            }
            else if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_RIGHT) && Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_LEFT))
	        {
                Console.WriteLine("Left and Right");
                // don't move sihp at all.  left and right cancel each other out.
	        } else if (Azul.Input.GetKeyState( Azul.AZUL_KEY.KEY_ARROW_LEFT))
	        {
                Console.WriteLine("Left");
                // move ship left on x axis
            }
            else if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_RIGHT))
            {
                Console.WriteLine("Right");
                // move ship right on x axis
            }
            else if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_Q))
            {
                System.Environment.Exit(1);
                // quits the application
            } else  if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_SPACE))
            {
                Console.WriteLine("Space");
                // check if missile has already been fired
                // if not, shoot missile
                // if so, do nothing
            }
        }

        /*public static void MouseTest()
        {

            // Quick and dirty test, if these work the rest do.
            // --> try move the mouse inside the window, click right, click left
            String a = "";
            String b = "";

            float xPos = 0.0f;
            float yPos = 0.0f;

            // get mouse position
            Azul.Input.GetCursor( ref xPos, ref yPos);

            // read mouse buttons
            if (Azul.Input.GetKeyState( Azul.AZUL_MOUSE.BUTTON_RIGHT ))
            {
                a = " <right>";
            }

            if (Azul.Input.GetKeyState( Azul.AZUL_MOUSE.BUTTON_LEFT ))
            {
                b = " <left>";
            }

            Console.WriteLine("({0},{1}): {2} {3} ", xPos, yPos, a, b);
        }
        */

    }
}
