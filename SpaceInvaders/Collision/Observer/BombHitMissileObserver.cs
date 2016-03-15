using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class BombHitMissileObserver : CollisionObserver
    {
        MissileBombSplat mws;
        MissileBombSplat mws2;
        PCSTree tree;
        MissileBombSplatRemover sr;
        MissileBombSplatRemover2 sr2;
        int counter = 1;

        public BombHitMissileObserver()
        {
            mws = new MissileBombSplat(GameObject.Name.MissileBombSplat, GameSprite.Name.MissileBombSplat, 0, -500, -500);
            mws2 = new MissileBombSplat(GameObject.Name.MissileBombSplat2, GameSprite.Name.MissileBombSplat2, 0, -500, -500);
            tree = new PCSTree();
            sr = new MissileBombSplatRemover();
            sr2 = new MissileBombSplatRemover2();
        }

        public override void Notify()
        {
            if (counter == 1)
            {
                GameObject a = this.subject.gameObjA;
                GameObject b = this.subject.gameObjB;

                float aX = a.x;
                float aY = a.y;

                if (a is Bomb)
                {
                    a.Remove();
                    //Bomb bomby = (Bomb)a;
                    //bomby.Reset();
                }

                if (b is Bomb)
                {
                    b.Remove();
                    //Bomb bomby = (Bomb)b;
                    //bomby.Reset();
                }

                mws.ActivateGameSprite(SpriteBatchManager.Find(SpriteBatch.Name.Aliens));
                mws.x = aX;
                mws.y = aY;
                mws.Update();

                GameObjectManager.AttachTree(mws, tree);
                TimerManager.Add(TimerEvent.Name.MissileBombSplatRemove, sr, 0.05f);
                counter++;
                return;
            }

            if (counter == 2)
            {
                GameObject a = this.subject.gameObjA;
                GameObject b = this.subject.gameObjB;

                float aX = a.x;
                float aY = a.y;

                if (a is Bomb)
                {
                    a.Remove();
                    //Bomb bomby = (Bomb)a;
                    //bomby.Reset();
                }

                if (b is Bomb)
                {
                    b.Remove();
                    //Bomb bomby = (Bomb)b;
                    //bomby.Reset();
                }

                mws2.ActivateGameSprite(SpriteBatchManager.Find(SpriteBatch.Name.Aliens));
                mws2.x = aX;
                mws2.y = aY;
                mws2.Update();

                GameObjectManager.AttachTree(mws2, tree);
                TimerManager.Add(TimerEvent.Name.MissileBombSplatRemove2, sr2, 0.05f);
                counter = 1;
            }
        }
    }
}
