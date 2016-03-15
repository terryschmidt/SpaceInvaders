using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SpriteBatchNode : CLink
    {
        // data:
        public SpriteBase spriteBase;
        private SpriteBatch spriteBatch;

        public SpriteBatchNode()
        {
            this.spriteBase = null;
            this.spriteBatch = null;
        }

        /*public void Set(GameSprite.Name gsName)
        {
            spriteBase = GameSpriteManager.Find(gsName);
            Debug.Assert(spriteBase != null);
        }

        public void Set(BoxSprite.Name bName)
        {
            spriteBase = BoxSpriteManager.Find(bName);
            Debug.Assert(spriteBase != null);
        }

        public void Set(SpriteBase nodeArg)
        {
            Debug.Assert(nodeArg != null);
            this.spriteBase = nodeArg;
            Debug.Assert(this.spriteBase != null);
        }*/

        public void Set(SpriteBase baseArg, SpriteBatch batchArg)
        {
            Debug.Assert(baseArg != null);
            Debug.Assert(batchArg != null);

            this.spriteBase = baseArg;
            this.spriteBatch = batchArg;

            Debug.Assert(spriteBase != null);
            this.spriteBase.SetSpriteBatchNode(this);
        }

        public SpriteBatch GetSpriteBatch()
        {
            Debug.Assert(this.spriteBatch != null);
            return this.spriteBatch;
        }

        public SpriteBase GetSpriteBase()
        {
            Debug.Assert(this.spriteBase != null);
            return this.spriteBase;
        }
    }
}
