using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class GridMover : Command
    {
        // data:
        public static PCSTreeForwardIterator it;
        Grid g;
        int numSoundToPlay;

        public GridMover()
        {
            g = (Grid)GameObjectManager.Find(GameObject.Name.Grid);
            it = new PCSTreeForwardIterator(g);
            numSoundToPlay = 4;
        }
        
        override public void execute(float deltaTime)
        {
            g.MoveGrid();
            playSound();
            animateAliens(g);
            TimerManager.Add(TimerEvent.Name.GridMovement, this, Values.gridMovementInterval);
        }

        public void animateAliens(GameObject nodeArg)
        {
            Boolean crabChanged = false;
            Boolean squidChanged = false;
            Boolean octopusChanged = false;
            PCSNode pNode = it.First();

            while (!it.IsDone())
            {
                // delta
                GameObject pGameObj = (GameObject)pNode;

                if (pGameObj.proxySprite.realSprite.name == GameSprite.Name.Crab && crabChanged == false)
                {
                    pGameObj.proxySprite.realSprite.SwapImage(ImageManager.Find(Image.Name.Crab2));
                    pGameObj.proxySprite.realSprite.name = GameSprite.Name.Crab2;
                    crabChanged = true;
                }
                else if (pGameObj.proxySprite.realSprite.name == GameSprite.Name.Crab2 && crabChanged == false)
                {
                    pGameObj.proxySprite.realSprite.SwapImage(ImageManager.Find(Image.Name.Crab));
                    pGameObj.proxySprite.realSprite.name = GameSprite.Name.Crab;
                    crabChanged = true;
                }

                else if (pGameObj.proxySprite.realSprite.name == GameSprite.Name.Octopus && octopusChanged == false)
                {
                    pGameObj.proxySprite.realSprite.SwapImage(ImageManager.Find(Image.Name.Octopus2));
                    pGameObj.proxySprite.realSprite.name = GameSprite.Name.Octopus2;
                    octopusChanged = true;
                }

                else if (pGameObj.proxySprite.realSprite.name == GameSprite.Name.Octopus2 && octopusChanged == false)
                {
                    pGameObj.proxySprite.realSprite.SwapImage(ImageManager.Find(Image.Name.Octopus));
                    pGameObj.proxySprite.realSprite.name = GameSprite.Name.Octopus;
                    octopusChanged = true;
                }

                else if (pGameObj.proxySprite.realSprite.name == GameSprite.Name.Squid && squidChanged == false)
                {
                    pGameObj.proxySprite.realSprite.SwapImage(ImageManager.Find(Image.Name.Squid2));
                    pGameObj.proxySprite.realSprite.name = GameSprite.Name.Squid2;
                    squidChanged = true;
                }

                else if (pGameObj.proxySprite.realSprite.name == GameSprite.Name.Squid2 && squidChanged == false)
                {
                    pGameObj.proxySprite.realSprite.SwapImage(ImageManager.Find(Image.Name.Squid));
                    pGameObj.proxySprite.realSprite.name = GameSprite.Name.Squid;
                    squidChanged = true;
                }
                else if (crabChanged == true && octopusChanged == true && squidChanged == true)
                {
                    break;
                }

                // Advance to next alien
                pNode = it.Next();
            }
        }

        public void playSound()
        {
            if (numSoundToPlay == 1)
            {
                SpaceInvaders.eng.Play2D("fastinvader1.wav");
            }

            if (numSoundToPlay == 2)
            {
                SpaceInvaders.eng.Play2D("fastinvader2.wav");
            }

            if (numSoundToPlay == 3)
            {
                SpaceInvaders.eng.Play2D("fastinvader3.wav");
            }

            if (numSoundToPlay == 4)
            {
                SpaceInvaders.eng.Play2D("fastinvader4.wav");
                numSoundToPlay = 0;
            }

            numSoundToPlay++;
        }
    }
}