using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShipSplatRemover : Command
    {
        public ShipSplatRemover()
        {

        }

        public override void execute(float deltaTime)
        {
            GameObject go = GameObjectManager.Find(GameObject.Name.ShipSplat);
            SpriteBatchNode sbn = go.proxySprite.GetSpriteBatchNode();
            SpriteBatchManager.Remove(sbn);
        }
    }
}
