using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SplatRemover2 : Command
    {
        public SplatRemover2()
        {

        }

        public override void execute(float deltaTime)
        {
            GameObject go = GameObjectManager.Find(GameObject.Name.Splat2);
            SpriteBatchNode sbn = go.proxySprite.GetSpriteBatchNode();
            SpriteBatchManager.Remove(sbn);
        }
    }
}
