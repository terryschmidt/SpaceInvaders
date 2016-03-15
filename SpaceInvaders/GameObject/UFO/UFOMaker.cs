using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class UFOMaker
    {
        private UFOMaker()
        {

        }

        public static void makeUFORoot()
        {
            UFORoot ufoRoot;
            if (Values.UFOspeed == 3.0f)
            {
                ufoRoot = new UFORoot(GameObject.Name.UFORoot, GameSprite.Name.NullObject, 0, -40, 865);
            }
            else
            {
                ufoRoot = new UFORoot(GameObject.Name.UFORoot, GameSprite.Name.NullObject, 0, 934, 865);
            }

            PCSTree rootTree = GameObjectManager.GetRootTree();
            rootTree.Insert(ufoRoot, null);
            ufoRoot.ActivateGameSprite(SpriteBatchManager.Find(SpriteBatch.Name.Boxes));
            ufoRoot.ActivateCollisionSprite(SpriteBatchManager.Find(SpriteBatch.Name.Boxes));
            GameObjectManager.AttachTree(ufoRoot, rootTree);
        }

        public static void makeUFO()
        {
            UFO ufo;
            if (Values.UFOspeed == 3.0f)
            {
                ufo = new UFO(GameObject.Name.UFO, GameSprite.Name.UFO, 0, -40, 865);
            }
            else
            {
                ufo = new UFO(GameObject.Name.UFO, GameSprite.Name.UFO, 0, 934, 865);
            }

            SpriteBatch pSB_Aliens = SpriteBatchManager.Find(SpriteBatch.Name.Aliens);
            SpriteBatch pSB_Boxes = SpriteBatchManager.Find(SpriteBatch.Name.Boxes);

            ufo.ActivateCollisionSprite(pSB_Boxes);
            ufo.ActivateGameSprite(pSB_Aliens);

            GameObject ufoRoot = GameObjectManager.Find(GameObject.Name.UFORoot);

            PCSTree rootTree = GameObjectManager.GetRootTree();
            rootTree.Insert(ufo, ufoRoot);
        }

        public static void makeCollisionPair()
        {
            CollisionPair cp = CollisionPairManager.Add(CollisionPair.Name.Missile_UFO, GameObjectManager.Find(GameObject.Name.MissileRoot), GameObjectManager.Find(GameObject.Name.UFORoot));
            cp.Attach(new ShipRemoveMissileObserver());
            cp.Attach(new ShipReadyObserver());
            //cp.Attach(new AlienSplatObserver());
            cp.Attach(new UFOSplatObserver());
            cp.Attach(new RemoveUFOObserver());
            cp.Attach(new UFODeathObserver());
        }

        public static void makeUFORootAndUFO()
        {
            makeUFORoot();
            makeUFO();
            makeCollisionPair();
            Values.ufoIsActive = true;
        }
    }
}
