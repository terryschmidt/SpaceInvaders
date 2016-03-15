using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShipSplatObserver : CollisionObserver
    {
        // data:
        PCSTree tree;
        ShipSplatRemover sr;
        ShipSplat splat;
        

        public ShipSplatObserver()
        {
            tree = new PCSTree();
            //AF = new AlienFactory(SpriteBatch.Name.Aliens, tree);
            sr = new ShipSplatRemover();
            splat = new ShipSplat(GameObject.Name.ShipSplat, GameSprite.Name.ShipSplat, 0, -500, -500);
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
            TimerManager.Add(TimerEvent.Name.ShipSplatRemove, sr, 0.10f);
        }
    }
}
