using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class UFOSplatRemover : Command
    {
        public UFOSplatRemover()
        {

        }

        public override void execute(float deltaTime)
        {
            GameObject go = GameObjectManager.Find(GameObject.Name.UFOSplat);
            SpriteBatchNode sbn = go.proxySprite.GetSpriteBatchNode();
            SpriteBatchManager.Remove(sbn);
            //go.Remove();
        }
    }
}
