using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShieldFactory
    {
        // data:
        private SpriteBatch spriteBatch;
        private SpriteBatch collisionSpriteBatch;
        private GameObject parent;
        private PCSTree tree;

        public ShieldFactory(SpriteBatch.Name spriteBatchNameArg, SpriteBatch.Name collisionSpriteBatchArg, PCSTree treeArg)
        {
            this.spriteBatch = SpriteBatchManager.Find(spriteBatchNameArg);
            Debug.Assert(this.spriteBatch != null);
            this.collisionSpriteBatch = SpriteBatchManager.Find(collisionSpriteBatchArg);
            Debug.Assert(this.collisionSpriteBatch != null);
            Debug.Assert(treeArg != null);
            this.tree = treeArg;
        }

        public void setParent(GameObject parentNode)
        {
            this.parent = parentNode;
        }

        ~ShieldFactory()
        {
            this.spriteBatch = null;
            this.parent = null;
        }

        public ShieldCategory Create(ShieldCategory.Type type, GameObject.Name gameName, int indexArg = 0, float xArg = 0.0f, float yArg = 0.0f)
        {
            ShieldCategory shield = null;

            switch (type)
            {
                case ShieldCategory.Type.Brick:
                    shield = new ShieldBrick(gameName, GameSprite.Name.Brick, indexArg, xArg, yArg);
                    break;

                case ShieldCategory.Type.LeftTop1:
                    shield = new ShieldBrick(gameName, GameSprite.Name.Brick_LeftTop1, indexArg, xArg, yArg);
                    break;

                case ShieldCategory.Type.LeftTop0:
                    shield = new ShieldBrick(gameName, GameSprite.Name.Brick_LeftTop0, indexArg, xArg, yArg);
                    break;

                case ShieldCategory.Type.LeftBottom:
                    shield = new ShieldBrick(gameName, GameSprite.Name.Brick_LeftBottom, indexArg, xArg, yArg);
                    break;

                case ShieldCategory.Type.RightTop1:
                    shield = new ShieldBrick(gameName, GameSprite.Name.Brick_RightTop1, indexArg, xArg, yArg);
                    break;

                case ShieldCategory.Type.RightTop0:
                    shield = new ShieldBrick(gameName, GameSprite.Name.Brick_RightTop0, indexArg, xArg, yArg);
                    break;

                case ShieldCategory.Type.RightBottom:
                    shield = new ShieldBrick(gameName, GameSprite.Name.Brick_RightBottom, indexArg, xArg, yArg);
                    break;

                case ShieldCategory.Type.Root:
                    shield = new ShieldRoot(gameName, GameSprite.Name.NullObject, indexArg, xArg, yArg);
                    //shield.SetCollisionColor(0.0f, 0.0f, 1.0f);
                    Debug.Assert(false);
                    break;

                case ShieldCategory.Type.Grid:
                    shield = new ShieldGrid(gameName, GameSprite.Name.NullObject, indexArg, xArg, yArg);
                    //shield.SetCollisionColor(0.0f, 0.0f, 1.0f);
                    break;

                case ShieldCategory.Type.Column:
                    shield = new ShieldColumn(gameName, GameSprite.Name.NullObject, indexArg, xArg, yArg);
                    //shield.SetCollisionColor(1.0f, 0.0f, 0.0f);
                    break;

                default:
                    // something is wrong
                    Debug.Assert(false);
                    break;
            }

            this.tree.Insert(shield, this.parent);
            shield.ActivateGameSprite(this.spriteBatch);
            shield.ActivateCollisionSprite(this.collisionSpriteBatch);
            return shield;
        }
    }
}
