using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class AlienFactory
    {
        // data:
        private PCSTree tree;
        private SpriteBatch batch;
        private PCSNode parent;

        public AlienFactory(SpriteBatch.Name sbName, PCSTree treeArg)
        {
            this.batch = SpriteBatchManager.Find(sbName);
            Debug.Assert(this.batch != null);

            Debug.Assert(treeArg != null);
            this.tree = treeArg;
        }

        public void setParent(PCSNode parentNode)
        {
            this.parent = parentNode;
        }

        ~AlienFactory()
        {
            this.batch = null;
            this.parent = null;
        }

        public AlienCategory Create(AlienCategory.Type type, GameObject.Name gameName, int indexArg = 0, float positionX = 0.0f, float positionY = 0.0f)
        {
            AlienCategory alien = null;

            if (type == AlienCategory.Type.Crab)
            {
                alien = new Crab(gameName, GameSprite.Name.Crab, indexArg, positionX, positionY);
                this.tree.Insert(alien, this.parent);
                //alien.ActivateGameSprite(this.batch);
                //alien.ActivateCollisionSprite(this.batch);

                alien.ActivateCollisionSprite(SpriteBatchManager.Find(SpriteBatch.Name.Boxes));
                alien.ActivateGameSprite(SpriteBatchManager.Find(SpriteBatch.Name.Aliens));
            }

            if (type == AlienCategory.Type.Squid)
            {
                alien = new Squid(gameName, GameSprite.Name.Squid, indexArg, positionX, positionY);
                this.tree.Insert(alien, this.parent);
                //alien.ActivateGameSprite(this.batch);
                //alien.ActivateCollisionSprite(this.batch);

                alien.ActivateCollisionSprite(SpriteBatchManager.Find(SpriteBatch.Name.Boxes));
                alien.ActivateGameSprite(SpriteBatchManager.Find(SpriteBatch.Name.Aliens));
            }

            if (type == AlienCategory.Type.Octopus)
            {
                alien = new Octopus(gameName, GameSprite.Name.Octopus, indexArg, positionX, positionY);
                this.tree.Insert(alien, this.parent);
                //alien.ActivateGameSprite(this.batch);
                //alien.ActivateCollisionSprite(this.batch);

                alien.ActivateCollisionSprite(SpriteBatchManager.Find(SpriteBatch.Name.Boxes));
                alien.ActivateGameSprite(SpriteBatchManager.Find(SpriteBatch.Name.Aliens));
            }

            if (type == AlienCategory.Type.Grid)
            {
                alien = new Grid(gameName, GameSprite.Name.NullObject, indexArg, 1500, 1500);
                this.tree.Insert(alien, this.parent);
                //alien.ActivateGameSprite(this.batch);
                //alien.ActivateCollisionSprite(this.batch);

                alien.ActivateCollisionSprite(SpriteBatchManager.Find(SpriteBatch.Name.Boxes));
                alien.ActivateGameSprite(SpriteBatchManager.Find(SpriteBatch.Name.Aliens));


                GameObjectManager.AttachTree(alien, this.tree);
            }

            if (type == AlienCategory.Type.Column)
            {
                alien = new Column(gameName, GameSprite.Name.NullObject, indexArg, positionX, positionY);
                this.tree.Insert(alien, this.parent);
                //alien.ActivateGameSprite(this.batch);
                //alien.ActivateCollisionSprite(this.batch);

                alien.ActivateCollisionSprite(SpriteBatchManager.Find(SpriteBatch.Name.Boxes));
                alien.ActivateGameSprite(SpriteBatchManager.Find(SpriteBatch.Name.Aliens));
            }

            if (type == AlienCategory.Type.Splat)
            {
                alien = new Splat(gameName, GameSprite.Name.Splat, indexArg, positionX, positionY);
                //this.tree.Insert(alien, this.parent);
                alien.ActivateGameSprite(this.batch);
                //alien.ActivateCollisionSprite(this.batch);
            }

            return alien;
        }
    }
}
