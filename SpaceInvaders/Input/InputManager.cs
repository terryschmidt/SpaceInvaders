using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class InputManager
    {
        // data:
        private static InputManager instance = null;
        private bool spaceKeyPrev;
        private InputSubject right;
        private InputSubject left;
        private InputSubject space;
        private Boolean cKeyPrev;

        private InputManager()
        {
            this.left = new InputSubject();
            this.right = new InputSubject();
            this.space = new InputSubject();
            this.spaceKeyPrev = false;
            this.cKeyPrev = false;
        }

        public static InputSubject getRightSubject()
        {
            InputManager inst = InputManager.getInstance();
            Debug.Assert(inst != null);
            return inst.right;
        }

        public static InputSubject getLeftSubject()
        {
            InputManager inst = InputManager.getInstance();
            Debug.Assert(inst != null);
            return inst.left;
        }

        public static InputSubject getSpaceSubject()
        {
            InputManager inst = InputManager.getInstance();
            Debug.Assert(inst != null);
            return inst.space;
        }

        public static void Update()
        {
            InputManager inst = InputManager.getInstance();
            Debug.Assert(inst != null);

            bool spaceKeyCurrent = Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_SPACE);

            //if (spaceKeyCurrent == true && inst.spaceKeyPrev == false)
            //{
            //    inst.space.Notify();
            //}

            if (spaceKeyCurrent == true)
            {
                inst.space.Notify();
            }

            inst.spaceKeyPrev = spaceKeyCurrent;

            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_LEFT) == true)
            {
                inst.left.Notify();
            }

            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_RIGHT) == true)
            {
                inst.right.Notify();
            }

            Boolean cKeyCurr = Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_C);
            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_C))
            {
                if (cKeyCurr == true && inst.cKeyPrev == false)
                {
                    SpriteBatchManager.shouldDrawBoxes = !SpriteBatchManager.shouldDrawBoxes;
                }
                
            }
            inst.cKeyPrev = cKeyCurr;

            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_Q) == true)
            {
                System.Environment.Exit(1);
            }

            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_1) == true && Values.onStartScreen == true)
            {
                Values.onStartScreen = false;
                GameSprite sis = GameSpriteManager.Find(GameSprite.Name.SpaceInvaders);
                sis.SetColor(0, 0, 0);
                Font startMessage = FontManager.Find(Font.Name.SpaceInvaders);
                startMessage.changeMessageTo("");
                Font q = FontManager.Find(Font.Name.Q);
                q.changeMessageTo("");
                Font c = FontManager.Find(Font.Name.C);
                c.changeMessageTo("");
                Font arrows = FontManager.Find(Font.Name.LeftArrow);
                arrows.changeMessageTo("");
                Font spacebar = FontManager.Find(Font.Name.Spacebar);
                spacebar.changeMessageTo("");
                Font credit = FontManager.Find(Font.Name.Credits);
                credit.changeMessageTo("CREDITS 01");

                Font crabpoints = FontManager.Find(Font.Name.CrabPoints);
                crabpoints.changeMessageTo("");

                Font octopoints = FontManager.Find(Font.Name.OctoPoints);
                octopoints.changeMessageTo("");

                Font squidpoints = FontManager.Find(Font.Name.SquidPoints);
                squidpoints.changeMessageTo("");

                Font ufopoints = FontManager.Find(Font.Name.UFOPoints);
                ufopoints.changeMessageTo("");

                // UFO
                UFOSpawner ufospawn = new UFOSpawner();
                double random = Values.getRandom(30.0, 40.0);
                float f = (float)random;
                Debug.WriteLine("Initial UFO Event value: " + f);
                TimerManager.Add(TimerEvent.Name.UFOSpawn, ufospawn, f);

                Ship shippy = ShipManager.GetShip();
                shippy.SetState(ShipManager.State.Ready);
            }

            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_S) == true && Values.onStartScreen == true)
            {
                Values.onStartScreen = false;
                GameSprite sis = GameSpriteManager.Find(GameSprite.Name.SpaceInvaders);
                sis.SetColor(0, 0, 0);
                Font startMessage = FontManager.Find(Font.Name.SpaceInvaders);
                startMessage.changeMessageTo("");
                Font q = FontManager.Find(Font.Name.Q);
                q.changeMessageTo("");
                Font c = FontManager.Find(Font.Name.C);
                c.changeMessageTo("");
                Font arrows = FontManager.Find(Font.Name.LeftArrow);
                arrows.changeMessageTo("");
                Font spacebar = FontManager.Find(Font.Name.Spacebar);
                spacebar.changeMessageTo("");
                Font credit = FontManager.Find(Font.Name.Credits);
                credit.changeMessageTo("CREDITS 01");

                Font crabpoints = FontManager.Find(Font.Name.CrabPoints);
                crabpoints.changeMessageTo("");

                Font octopoints = FontManager.Find(Font.Name.OctoPoints);
                octopoints.changeMessageTo("");

                Font squidpoints = FontManager.Find(Font.Name.SquidPoints);
                squidpoints.changeMessageTo("");

                Font ufopoints = FontManager.Find(Font.Name.UFOPoints);
                ufopoints.changeMessageTo("");

                // UFO
                UFOSpawner ufospawn = new UFOSpawner();
                double random = Values.getRandom(30.0, 40.0);
                float f = (float)random;
                Debug.WriteLine("Initial UFO Event value: " + f);
                TimerManager.Add(TimerEvent.Name.UFOSpawn, ufospawn, f);

                Ship shippy = ShipManager.GetShip();
                shippy.SetState(ShipManager.State.Ready);
            }

            inst.spaceKeyPrev = spaceKeyCurrent;
        }

        private static InputManager getInstance()
        {
            if (instance == null)
            {
                instance = new InputManager();
            }

            return instance;
        }
    }
}
