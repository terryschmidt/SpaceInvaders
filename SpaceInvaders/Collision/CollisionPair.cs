using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class CollisionPair : MLink
    {
        // data:
        public CollisionPair.Name name;
        public int index;
        public GameObject treeA;
        public GameObject treeB;
        public CollisionSubject subject;

        public enum Name
        {
            Alien_Missile,
            Alien_Wall,
            Alien_Ship,
            Missile_Wall,
            Missile_Shield,
            Alien_Shield,
            Octo_Ship,
            Bomb_Missile,
            Crab_Ship,
            Grid_BottomWall,
            Bomb_Ship,
            Squid_Ship,
            Bomb_Wall,
            Bomb_Shield,
            NullObject,
            Missile_UFO,
            Not_Initialized
        }

        public CollisionPair()
            : base()
        {
            this.treeA = null;
            this.treeB = null;
            this.subject = null;
            this.name = CollisionPair.Name.Not_Initialized;
            this.index = 0;
        }

        ~CollisionPair()
        {
            this.subject = null;
        }

        public void Set(CollisionPair.Name pairName, int indexArg, GameObject treeAArg, GameObject treeBArg)
        {
            Debug.Assert(treeAArg != null);
            Debug.Assert(treeBArg != null);

            this.treeA = treeAArg;
            this.treeB = treeBArg;
            this.name = pairName;
            this.index = indexArg;
            this.subject = new CollisionSubject();
        }

        public void Process()
        {
            Collide(this.treeA, this.treeB);
        }

        static public void Collide(GameObject safeTreeA, GameObject safeTreeB)
        {
            // A vs B
            GameObject nodeA = safeTreeA;
            GameObject nodeB = safeTreeB;

            while (nodeA != null)
            {
                nodeB = safeTreeB;

                while (nodeB != null)
                {
                    //Debug.WriteLine("ColPair: collide: {0}, {1}", nodeA.name, nodeB.name);

                    CollisionRectangle rectA = nodeA.colObj.colRect;
                    CollisionRectangle rectB = nodeB.colObj.colRect;

                    if (CollisionRectangle.Intersect(rectA, rectB))
                    {
                        //Debug.WriteLine("ColPair: collide: {0}, {1}", nodeA.name, nodeB.name);
                        nodeA.Accept(nodeB);
                        break;
                    }

                    nodeB = (GameObject)nodeB.pSibling;
                }
                nodeA = (GameObject)nodeA.pSibling;
            }
        }

        public void Attach(CollisionObserver observer)
        {
            this.subject.Attach(observer);
        }

        public void NotifyListeners()
        {
            this.subject.Notify();
        }

        public void SetCollision(GameObject objA, GameObject objB)
        {
            Debug.Assert(objA != null);
            Debug.Assert(objB != null);

            this.subject.gameObjA = objA;
            this.subject.gameObjB = objB;
        }
    }
}
