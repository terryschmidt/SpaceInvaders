using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SplatRemover5 : Command
    {
        public SplatRemover5()
        {

        }

        public override void execute(float deltaTime)
        {
            GameObject go = GameObjectManager.Find(GameObject.Name.Splat5);
            SpriteBatchNode sbn = go.proxySprite.GetSpriteBatchNode();
            SpriteBatchManager.Remove(sbn);
        }
    }
}
