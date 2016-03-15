using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SpriteBatch : Container
    {
        // data:
        public SpriteBatch.Name name;

        public enum Name
        {
            Aliens,
            Boxes,
            AngryBirds,
            Background,
            Texts,
            Shields,
            Uninitialized
        }

        public SpriteBatch(int reserveNum = 0, int reserveGrow = 0)
            : base(reserveNum, reserveGrow)
        {
            Debug.Assert(reserveNum == 0);
            Debug.Assert(reserveGrow == 0);
        }

        public void Draw()
        {
            SpriteBatchNode sbn = (SpriteBatchNode)this.active;

            while (sbn != null)
            {
                sbn.GetSpriteBase().Render();
                sbn = (SpriteBatchNode)sbn.next;
            }
        }

        public void Attach(SpriteBase pNode)
        {
            // Go to Man, get a node from reserve, add to active, return it
            SpriteBatchNode sbn = (SpriteBatchNode)this.baseAdd();
            Debug.Assert(sbn != null);

            // Initialize SpriteBatchNode
            sbn.Set(pNode, this);
        }

        public void Remove(SpriteBatchNode sbnArg)
        {
            Debug.Assert(sbnArg != null);
            this.baseRemove(sbnArg);
        }

        protected override CLink CreateNode()
        {
            SpriteBatchNode node = new SpriteBatchNode();
            Debug.Assert(node != null);
            return node;
        }

        override protected void DumpNode(CLink pLink)
        {

        }
        override protected Boolean Compare(CLink pLinkA, CLink pLinkB)
        {
            return false;
        }

        public void Dump()
        {

        }

        public void Set(SpriteBatch.Name sbName, int reserveNum, int reserveGrow)
        {
            this.name = sbName;
            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);
            this.baseSetReserve(reserveNum, reserveGrow);
        }
    }
}
