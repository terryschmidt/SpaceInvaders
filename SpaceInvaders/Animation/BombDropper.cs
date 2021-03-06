﻿using System;
using System.Diagnostics;

// Tsrry Schmidt.

namespace SpaceInvaders
{
    class BombDropper : Command
    {
        public BombDropper()
        {

        }

        public override void execute(float deltaTime)
        {
            if (Values.columnCount > 0)
            {
                int randomForColumn = SpaceInvaders.randy.Next(0, 11);

                Column c = (Column)GameObjectManager.Find(GameObject.Name.Column, randomForColumn);

                if (c != null)
                {
                    float width = c.colObj.colRect.width;
                    float height = c.colObj.colRect.height;

                    int random = SpaceInvaders.randy.Next(1, 4);
                    Debug.WriteLine("Random value generated by Bomb Dropper: " + random);

                    Bomb boom = null;
                    Bomb ufoBoom = null;

                    if (random == 1)
                    {
                        boom = new Bomb(GameObject.Name.Bomb, GameSprite.Name.BombStraight, new FallStraight(), 0, c.x, c.y - height / 2);
                        PCSTree pRootTree = GameObjectManager.GetRootTree();
                        pRootTree.Insert(boom, GameObjectManager.Find(GameObject.Name.BombRoot));
                        boom.ActivateCollisionSprite(SpriteBatchManager.Find(SpriteBatch.Name.Boxes));
                        boom.ActivateGameSprite(SpriteBatchManager.Find(SpriteBatch.Name.Aliens));
                    }

                    if (random == 2)
                    {
                        boom = new Bomb(GameObject.Name.Bomb, GameSprite.Name.BombDagger, new FallDagger(), 0, c.x, c.y - height / 2);
                        PCSTree pRootTree = GameObjectManager.GetRootTree();
                        pRootTree.Insert(boom, GameObjectManager.Find(GameObject.Name.BombRoot));
                        boom.ActivateCollisionSprite(SpriteBatchManager.Find(SpriteBatch.Name.Boxes));
                        boom.ActivateGameSprite(SpriteBatchManager.Find(SpriteBatch.Name.Aliens));
                    }

                    if (random == 3)
                    {
                        boom = new Bomb(GameObject.Name.Bomb, GameSprite.Name.BombZigZag, new FallZigZag(), 0, c.x, c.y - height / 2);
                        PCSTree pRootTree = GameObjectManager.GetRootTree();
                        pRootTree.Insert(boom, GameObjectManager.Find(GameObject.Name.BombRoot));
                        boom.ActivateCollisionSprite(SpriteBatchManager.Find(SpriteBatch.Name.Boxes));
                        boom.ActivateGameSprite(SpriteBatchManager.Find(SpriteBatch.Name.Aliens));
                    }

                    // give UFO a chance to drop a bomb too if its active

                    if (Values.ufoIsActive == true)
                    {
                        int randomForUFO = SpaceInvaders.randy.Next(0, 100);
                        if (randomForUFO >= 25 && randomForUFO < 30) { // 5%
                            UFO ufo = (UFO)GameObjectManager.Find(GameObject.Name.UFO);
                            //float ufoHeight = ufo.colObj.colRect.height;

                            ufoBoom = new Bomb(GameObject.Name.UFOBomb, GameSprite.Name.UFOBomb, new FallZigZag(), 0, ufo.x, ufo.y - 25);
                            PCSTree pRootTree = GameObjectManager.GetRootTree();
                            pRootTree.Insert(ufoBoom, GameObjectManager.Find(GameObject.Name.BombRoot));
                            ufoBoom.ActivateCollisionSprite(SpriteBatchManager.Find(SpriteBatch.Name.Boxes));
                            ufoBoom.ActivateGameSprite(SpriteBatchManager.Find(SpriteBatch.Name.Aliens));
                        }
                    }

                    // re-add bomb drop event to timer for next round!
                    double randDouble = Values.getRandom(0.50, 0.90);
                    float fl = (float)randDouble;
                    //Debug.WriteLine("Reseeding Bomb Drop event with value: " + fl);
                    TimerManager.Add(TimerEvent.Name.BombDrop, this, fl);
                    return;
                }

                // re-add bomb drop event to timer for next round!
                double randomDouble = Values.getRandom(0.0, 0.0);
                float f = (float)randomDouble;
                //Debug.WriteLine("Reseeding Bomb Drop event with value: " + f);
                TimerManager.Add(TimerEvent.Name.BombDrop, this, f);
            }
        }
    }
}
