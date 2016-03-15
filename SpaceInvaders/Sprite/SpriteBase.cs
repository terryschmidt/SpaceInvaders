using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class SpriteBase : MLink
    {
        // data:

        private SpriteBatchNode spriteBatchNode;

        //public float x;
        //public float y;
        //public float sx;
        //public float sy;
        //public float angle;

        protected SpriteBase()
        {
            //x = 0.0f;
            //y = 0.0f;
            //sx = 1.0f;
            //sy = 1.0f;
            //angle = 0.0f;
            this.spriteBatchNode = null;
        }

        ~SpriteBase()
        {
            this.spriteBatchNode = null;
        }

        public SpriteBatchNode GetSpriteBatchNode()
        {
            Debug.Assert(this.spriteBatchNode != null);
            return this.spriteBatchNode;
        }

        public void SetSpriteBatchNode(SpriteBatchNode spriteBatchArg)
        {
            Debug.Assert(spriteBatchArg != null);
            this.spriteBatchNode = spriteBatchArg;
        }

        // abstract methods

        abstract public Enum GetName();
        abstract public void Update();
        abstract public void Render();
    }
}
