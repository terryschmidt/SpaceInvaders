using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class Column : AlienCategory
    {
        // data:
        private float delta;
        private float total;

        public Column(GameObject.Name nameArg, GameSprite.Name spriteName, int indexArg, float positionX, float positionY)
            : base(nameArg, spriteName, indexArg, AlienCategory.Type.Column)
        {
            this.x = positionX;
            this.y = positionY;
            this.delta = 2.0f;
            this.total = 0.0f;
            //this.colObj.colSprite.SetLineColor(1, 1, 1);
        }

        ~Column()
        {

        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitColumn(this);
        }

        public override void VisitMissileRoot(MissileRoot m)
        {
            CollisionPair.Collide(m, (GameObject)this.pChild);
        }

        public override void VisitShipRoot(ShipRoot s)
        {
            CollisionPair.Collide(s, (GameObject)this.pChild);
        }

        public override void VisitShieldRoot(ShieldRoot s)
        {
            //CollisionPair.Collide(s, (GameObject)this.pChild);

            GameObject curr = (GameObject)this.pChild;
            while (curr != null)
            {
                CollisionPair.Collide(curr, s);
                curr = curr.pSibling as GameObject;
            }
        }

        public override void VisitShieldGrid(ShieldGrid s)
        {
            //CollisionPair.Collide(s, (GameObject)this.pChild);

            GameObject curr = (GameObject)this.pChild;
            while (curr != null)
            {
                CollisionPair.Collide(curr, s);
                curr = curr.pSibling as GameObject;
            }
        }

        public override void VisitShieldBrick(ShieldBrick s)
        {
            //CollisionPair.Collide(s, (GameObject)this.pChild);

            GameObject curr = (GameObject)this.pChild;
            while (curr != null)
            {
                CollisionPair.Collide(curr, s);
                curr = curr.pSibling as GameObject;
            }
        }

        public override void VisitShieldColumn(ShieldColumn s)
        {
            //GameObject curr = (GameObject)this.pChild;
            //while (curr != null)
            //{
            //    CollisionPair.Collide(curr, s);
            //    curr = curr.pSibling as GameObject;
            //}
        }

        public override void Update()
        {
            base.baseUpdateBoundingBox();
            base.Update();
        }

        public void MoveGrid()
        {
            // Initialize
            PCSTreeForwardIterator i = new PCSTreeForwardIterator(this);
            Debug.Assert(i != null);

            PCSNode pNode = i.First();

            while (pNode != null)
            {
                // delta
                GameObject pGameObj = (GameObject)pNode;
                pGameObj.x += this.delta;

                // Advance
                pNode = i.Next();
            }

            this.total += this.delta;

            if (this.total > 400.0f || this.total < 0.0f)
            {
                this.delta *= -1.0f;
            }
        }
    }
}
