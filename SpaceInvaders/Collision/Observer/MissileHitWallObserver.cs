using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class MissileHitWallObserver : CollisionObserver
    {
        // data:
        PCSTree tree;
        //AlienFactory AF;
        MissileWallSplatRemover sr;
        MissileWallSplat splat;

        public MissileHitWallObserver()
        {
            tree = new PCSTree();
            sr = new MissileWallSplatRemover();
            splat = new MissileWallSplat(GameObject.Name.MissileWallSplat, GameSprite.Name.MissileWallSplat, 0, -500, -500);
        }

        public override void Notify()
        {
            GameObject missile = GameObjectManager.Find(GameObject.Name.Missile);
            float mY = missile.y;
            float mX = missile.x;

            splat.ActivateGameSprite(SpriteBatchManager.Find(SpriteBatch.Name.Aliens));
            splat.x = mX;
            splat.y = mY;
            splat.Update();

            GameObjectManager.AttachTree(splat, tree);
            TimerManager.Add(TimerEvent.Name.MissileWallSplatRemove, sr, 0.08f);
        }
    }
}
