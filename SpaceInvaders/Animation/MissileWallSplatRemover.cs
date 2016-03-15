using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class MissileWallSplatRemover : Command
    {
        public MissileWallSplatRemover()
        {

        }

        public override void execute(float deltaTime)
        {
            GameObject go = GameObjectManager.Find(GameObject.Name.MissileWallSplat);
            SpriteBatchNode sbn = go.proxySprite.GetSpriteBatchNode();
            SpriteBatchManager.Remove(sbn);
        }
    }
}
