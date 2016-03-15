using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class Grid : AlienCategory
    {

        // data:
        public float delta;

        public Grid(GameObject.Name gameName, GameSprite.Name spriteName, int indexArg, float positionX, float positionY)
            : base(gameName, spriteName, indexArg, AlienCategory.Type.Grid)
        {
            this.x = positionX;
            this.y = positionY;
            this.delta = 6.5f;
            //this.colObj.colSprite.SetLineColor(0, 0, 1);
        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitGrid(this);
        }

        public override void VisitMissileRoot(MissileRoot m)
        {
            CollisionPair.Collide(m, (GameObject)this.pChild);
        }

        public override void VisitWallRoot(WallRoot w)
        {
            CollisionPair.Collide(this, (GameObject)w.pChild);
        }

        public override void VisitShipRoot(ShipRoot s)
        {
            CollisionPair.Collide(s, (GameObject)this.pChild);
        }

        public override void VisitShieldRoot(ShieldRoot s)
        {
            CollisionPair.Collide(s, (GameObject)this.pChild);

            //GameObject curr = (GameObject)this.pChild;
            //while (curr != null)
            //{
            //    CollisionPair.Collide(s, curr);
            //    curr = curr.pSibling as GameObject;
            //}
        }

        public override void VisitShieldGrid(ShieldGrid s)
        {
            CollisionPair.Collide(s, (GameObject)this.pChild);

            //GameObject curr = this.pChild as GameObject;
            //while (curr != null)
            //{
            //    CollisionPair.Collide(curr, s);
            //    curr = curr.pSibling as GameObject;
            //}

            //GameObject curr = (GameObject)this.pChild;
            //while (curr != null)
            //{
            //    CollisionPair.Collide(curr, s);
            //    curr = curr.pSibling as GameObject;
            //}
        }

        public override void Update()
        {
            //Console.WriteLine("inside Grid.Update()");

            // will eventally comment on this.MoveGrid() when putting the movement on the timer instead...
            //this.MoveGrid();
            base.baseUpdateBoundingBox();
            base.Update();
        }

        public void MoveGrid()
        {
            // Initialize
            PCSTreeForwardIterator iter = new PCSTreeForwardIterator(this);
            Debug.Assert(iter != null);

            PCSNode pNode = iter.First();

            while (!iter.IsDone())
            {
                // delta
                GameObject pGameObj = (GameObject)pNode;
                pGameObj.x += this.delta;
                

                // Advance
                pNode = iter.Next();
            }
        }

        public void PushGridDownward()
        {
            // Initialize
            PCSTreeForwardIterator iter = new PCSTreeForwardIterator(this);
            Debug.Assert(iter != null);

            PCSNode pNode = iter.First();

            while (!iter.IsDone())
            {
                // delta
                GameObject pGameObj = (GameObject)pNode;
                pGameObj.y -= 15.0f;


                // Advance
                pNode = iter.Next();
            }
        }

        private void MoveTreeDepthFirst(GameObject pNode)
        {
            /*PCSNode child = null;

            // dump
            pNode.x += this.delta;

            if (this.total > 80.0f || this.total < -35.0f)
            {
                pNode.y -= 20.0f;
            }

            // iterate through all of the active children 
            if (pNode.child != null)
            {
                child = pNode.child;

                // make sure that allocation is not a child node 
                while (child != null)
                {
                  
                    MoveTreeDepthFirst((GameObject)child);
                    // goto next sibling
                    child = child.sibling;
                }
            }
            else
            {
                // bye bye exit condition
            }*/
        }
    }
}
