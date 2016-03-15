using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class UFOSplatObserver : CollisionObserver
    {
         // data:
        PCSTree tree;
        //AlienFactory AF;
        UFOSplatRemover sr;
        UFOSplat splat;

        public UFOSplatObserver()
        {
            tree = new PCSTree();
            //AF = new AlienFactory(SpriteBatch.Name.Aliens, tree);
            sr = new UFOSplatRemover();
            splat = new UFOSplat(GameObject.Name.UFOSplat, GameSprite.Name.UFOSplat, 0, -500, -500);
        }

        public override void Notify()
        {
            GameObject a = this.subject.gameObjA;
            GameObject b = this.subject.gameObjB;

            splat.ActivateGameSprite(SpriteBatchManager.Find(SpriteBatch.Name.Aliens));
            splat.x = b.x;
            splat.y = b.y;
            splat.Update();

            GameObjectManager.AttachTree(splat, tree);
            TimerManager.Add(TimerEvent.Name.UFOSplatRemove, sr, 1.18f);
        }
    }
}
