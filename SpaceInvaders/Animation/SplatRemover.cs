using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SplatRemover : Command
    {
        public SplatRemover()
        {

        }

        public override void execute(float deltaTime)
        {
            GameObject go = GameObjectManager.Find(GameObject.Name.Splat);
            SpriteBatchNode sbn = go.proxySprite.GetSpriteBatchNode();
            SpriteBatchManager.Remove(sbn);
            //go.Remove();

            //GameSprite gs = GameSpriteManager.Find(GameSprite.Name.Splat);
            //gs.x = -500;
            //gs.y = -500;
            //gs.Update();
        }
    }
}
