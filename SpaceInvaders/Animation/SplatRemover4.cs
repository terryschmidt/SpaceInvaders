using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SplatRemover4 : Command
    {
        public SplatRemover4()
        {

        }

        public override void execute(float deltaTime)
        {
            GameObject go = GameObjectManager.Find(GameObject.Name.Splat4);
            SpriteBatchNode sbn = go.proxySprite.GetSpriteBatchNode();
            SpriteBatchManager.Remove(sbn);
        }
    }
}
