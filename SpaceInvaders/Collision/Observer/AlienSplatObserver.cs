using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class AlienSplatObserver : CollisionObserver
    {
        // data:
        PCSTree tree;
        //AlienFactory AF;
        SplatRemover sr;
        SplatRemover2 sr2;
        SplatRemover3 sr3;
        SplatRemover4 sr4;
        SplatRemover5 sr5;

        Splat splat;
        Splat splat2;
        Splat splat3;
        Splat splat4;
        Splat splat5;

        int counter = 1;

        public AlienSplatObserver()
        {
            tree = new PCSTree();
            sr = new SplatRemover();
            sr2 = new SplatRemover2();
            sr3 = new SplatRemover3();
            sr4 = new SplatRemover4();
            sr5 = new SplatRemover5();
            splat = new Splat(GameObject.Name.Splat, GameSprite.Name.Splat, 0, -500, -500);
            splat2 = new Splat(GameObject.Name.Splat2, GameSprite.Name.Splat2, 0, -500, -500);
            splat3 = new Splat(GameObject.Name.Splat3, GameSprite.Name.Splat3, 0, -500, -500);
            splat4 = new Splat(GameObject.Name.Splat4, GameSprite.Name.Splat4, 0, -500, -500);
            splat5 = new Splat(GameObject.Name.Splat5, GameSprite.Name.Splat5, 0, -500, -500);
        }

        public override void Notify()
        {
            GameObject a = this.subject.gameObjA;
            GameObject b = this.subject.gameObjB;

            // 5 simultaneous alien splats are available.  Should be plenty to avoid runtime error.

            if (counter == 1)
            {
                splat.ActivateGameSprite(SpriteBatchManager.Find(SpriteBatch.Name.Aliens));
                splat.x = b.x;
                splat.y = b.y;
                splat.Update();

                GameObjectManager.AttachTree(splat, tree);
                TimerManager.Add(TimerEvent.Name.SplatRemove, sr, 0.09f);
                counter++;
                return;
            }

            if (counter == 2)
            {
                splat2.ActivateGameSprite(SpriteBatchManager.Find(SpriteBatch.Name.Aliens));
                splat2.x = b.x;
                splat2.y = b.y;
                splat2.Update();

                GameObjectManager.AttachTree(splat2, tree);
                TimerManager.Add(TimerEvent.Name.SplatRemove2, sr2, 0.09f);
                counter++;
                return;
            }

            if (counter == 3)
            {
                splat3.ActivateGameSprite(SpriteBatchManager.Find(SpriteBatch.Name.Aliens));
                splat3.x = b.x;
                splat3.y = b.y;
                splat3.Update();

                GameObjectManager.AttachTree(splat3, tree);
                TimerManager.Add(TimerEvent.Name.SplatRemove3, sr3, 0.09f);
                counter++;
                return;
            }

            if (counter == 4)
            {
                splat4.ActivateGameSprite(SpriteBatchManager.Find(SpriteBatch.Name.Aliens));
                splat4.x = b.x;
                splat4.y = b.y;
                splat4.Update();

                GameObjectManager.AttachTree(splat4, tree);
                TimerManager.Add(TimerEvent.Name.SplatRemove4, sr4, 0.09f);
                counter++;
                return;
            }

            if (counter == 5)
            {
                splat5.ActivateGameSprite(SpriteBatchManager.Find(SpriteBatch.Name.Aliens));
                splat5.x = b.x;
                splat5.y = b.y;
                splat5.Update();

                GameObjectManager.AttachTree(splat5, tree);
                TimerManager.Add(TimerEvent.Name.SplatRemove5, sr5, 0.09f);
                counter = 1;
                return;
            }
        }
    }
}
