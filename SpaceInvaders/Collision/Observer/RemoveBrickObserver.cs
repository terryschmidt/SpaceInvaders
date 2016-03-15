using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class RemoveBrickObserver : CollisionObserver
    {
        // data:
        private GameObject brick;

        public RemoveBrickObserver()
        {
            this.brick = null;
        }

        public RemoveBrickObserver(RemoveBrickObserver b)
        {
            Debug.Assert(b != null);
            this.brick = b.brick;
        }

        public override void Notify()
        {
            if (this.subject.gameObjB is ShieldBrick)
            {
                this.brick = (ShieldBrick)this.subject.gameObjB;
                Debug.Assert(this.brick != null);

                if (brick.markForDeath == false)
                {
                    brick.markForDeath = true;
                    RemoveBrickObserver observer = new RemoveBrickObserver(this);
                    DelayObjectManager.Attach(observer);
                }
            }

            if (this.subject.gameObjA is ShieldBrick)
            {
                this.brick = (ShieldBrick)this.subject.gameObjA;
                Debug.Assert(this.brick != null);

                if (brick.markForDeath == false)
                {
                    brick.markForDeath = true;
                    RemoveBrickObserver observer = new RemoveBrickObserver(this);
                    DelayObjectManager.Attach(observer);
                }
            }
        }
            

        public override void Execute()
        {
            GameObject pA = (GameObject)this.brick;
            GameObject pB = (GameObject)pA.pParent;

            if (pA is ShieldBrick)
            {
                pA.Remove();

                if (checkParent(pB) == true)
                {
                    GameObject pC = (GameObject)pB.pParent;
                    pB.Remove();

                    if (checkParent(pC) == true)
                    {
                        pC.Remove();
                    }
                }
            }


            if (pB is ShieldBrick)
            {
                pB.Remove();

                if (checkParent(pA) == true)
                {
                    GameObject pC = (GameObject)pA.pParent;
                    pA.Remove();

                    if (checkParent(pC) == true)
                    {
                        pC.Remove();
                    }
                }
            }
        }

        private bool checkParent(GameObject obj)
        {
            if (obj.pChild == null)
            {
                return true;
            }

            return false;
        }
    }
}
