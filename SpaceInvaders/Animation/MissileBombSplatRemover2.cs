using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class MissileBombSplatRemover2 : Command
    {
        public MissileBombSplatRemover2()
        {

        }

        public override void execute(float deltaTime)
        {
            GameObject go = GameObjectManager.Find(GameObject.Name.MissileBombSplat2);
            SpriteBatchNode sbn = go.proxySprite.GetSpriteBatchNode();
            SpriteBatchManager.Remove(sbn);
        }
    }
}
