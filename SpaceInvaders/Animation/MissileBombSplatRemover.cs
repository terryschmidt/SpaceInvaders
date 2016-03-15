using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class MissileBombSplatRemover : Command
    {
        public MissileBombSplatRemover()
        {

        }

        public override void execute(float deltaTime)
        {
            GameObject go = GameObjectManager.Find(GameObject.Name.MissileBombSplat);
            SpriteBatchNode sbn = go.proxySprite.GetSpriteBatchNode();
            SpriteBatchManager.Remove(sbn);
        }
    }
}
