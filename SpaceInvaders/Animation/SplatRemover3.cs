using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SplatRemover3 : Command
    {
          public SplatRemover3()
        {

        }

        public override void execute(float deltaTime)
        {
            GameObject go = GameObjectManager.Find(GameObject.Name.Splat3);
            SpriteBatchNode sbn = go.proxySprite.GetSpriteBatchNode();
            SpriteBatchManager.Remove(sbn);
        }
    }
}
