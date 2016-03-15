using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class NewWaveMaker : Command
    {
        PCSTree newTree;
        AlienFactory AF;

        public NewWaveMaker()
        {
            newTree = new PCSTree();
            AF = new AlienFactory(SpriteBatch.Name.Aliens, newTree);
        }

        override public void execute(float deltaTime)
        {
            GameSpriteManager.Add(GameSprite.Name.Octopus, Image.Name.Octopus, 450, 400, 49, 33).SetColor(0.0f, 1.02f, 1.02f);
            GameSpriteManager.Add(GameSprite.Name.Octopus2, Image.Name.Octopus2, 450, 400, 49, 33).SetColor(0, 1, 1);
            GameSpriteManager.Add(GameSprite.Name.Crab, Image.Name.Crab, 600, 400, 45, 33).SetColor(0.45f, 0.0f, 1.89f);
            GameSpriteManager.Add(GameSprite.Name.Crab2, Image.Name.Crab2, 600, 400, 45, 33).SetColor(0.45f, 0.0f, 1.89f);
            GameSpriteManager.Add(GameSprite.Name.Squid, Image.Name.Squid, 700, 400, 33, 33).SetColor(2.55f, 0, 2.55f);
            GameSpriteManager.Add(GameSprite.Name.Squid2, Image.Name.Squid2, 700, 400, 33, 33).SetColor(2.55f, 0, 2.55f);
            GameSpriteManager.Add(GameSprite.Name.Missile, Image.Name.Missile, 50, 50, 3, 17);
            GameSpriteManager.Add(GameSprite.Name.Ship, Image.Name.Ship, 50, 50, 75, 54).SetColor(0.0f, 250.0f, 0.0f);
            GameSpriteManager.Add(GameSprite.Name.UFO, Image.Name.UFO, 50, 50, 64, 28).SetColor(1, 0, 0);
            GameSpriteManager.Add(GameSprite.Name.Splat, Image.Name.Splat, 600, 400, 49, 33);
            GameSpriteManager.Add(GameSprite.Name.BombZigZag, Image.Name.BombZigZag, -200, -200, 14, 30);
            GameSpriteManager.Add(GameSprite.Name.BombStraight, Image.Name.BombStraight, -100, -100, 6, 34);
            GameSpriteManager.Add(GameSprite.Name.BombDagger, Image.Name.BombCross, -100, -100, 14, 36);

            GameSpriteManager.Add(GameSprite.Name.Brick, Image.Name.Brick, 50, 25, 16, 8);
            GameSpriteManager.Add(GameSprite.Name.Brick_LeftTop0, Image.Name.BrickLeft_Top0, 50, 25, 16, 8);
            GameSpriteManager.Add(GameSprite.Name.Brick_LeftTop1, Image.Name.BrickLeft_Top1, 50, 25, 16, 8);
            GameSpriteManager.Add(GameSprite.Name.Brick_LeftBottom, Image.Name.BrickLeft_Bottom, 50, 25, 16, 8);
            GameSpriteManager.Add(GameSprite.Name.Brick_RightTop0, Image.Name.BrickRight_Top0, 50, 25, 16, 8);
            GameSpriteManager.Add(GameSprite.Name.Brick_RightTop1, Image.Name.BrickRight_Top1, 50, 25, 16, 8);
            GameSpriteManager.Add(GameSprite.Name.Brick_RightBottom, Image.Name.BrickRight_Bottom, 50, 25, 16, 8);

            //// create the factory 
            //AlienFactory AF = new AlienFactory(SpriteBatch.Name.Aliens, newTree);

            DeathManager.Attach(AF);

            // Set the parent for Grid inside the factory, Since grid is root so parent is null
            AF.setParent(null);

            // attach to Root  (this is the Grid)
            AlienCategory pGrid = AF.Create(AlienCategory.Type.Grid, GameObject.Name.Grid);

            // create 15 quickly attach to Grid
            for (int i = 0; i < 11; i++)
            {
                // set Parent
                AF.setParent(pGrid);
                AlienCategory pColumn = AF.Create(AlienCategory.Type.Column, GameObject.Name.Column, i, 0.0f, 0.0f);

                // set Parent
                AF.setParent(pColumn);

                AF.Create(AlienCategory.Type.Octopus, GameObject.Name.Octopus, i, 100.0f + i * 65.0f, Values.currentHighestYPositionOfAlien - 240);
                AF.Create(AlienCategory.Type.Octopus, GameObject.Name.Octopus, i, 100.0f + i * 65.0f, Values.currentHighestYPositionOfAlien - 180);
                AF.Create(AlienCategory.Type.Crab, GameObject.Name.Crab, i, 100.0f + i * 65.0f, Values.currentHighestYPositionOfAlien - 60);
                AF.Create(AlienCategory.Type.Squid, GameObject.Name.Squid, i, 100.0f + i * 65.0f, Values.currentHighestYPositionOfAlien);
                AF.Create(AlienCategory.Type.Crab, GameObject.Name.Crab, i, 100.0f + i * 65.0f, Values.currentHighestYPositionOfAlien - 120);
            }

             //associate in a collision pair
            CollisionPair cp = CollisionPairManager.Add(CollisionPair.Name.Alien_Wall, pGrid, GameObjectManager.Find(GameObject.Name.WallRoot));
            Debug.Assert(cp != null);
            cp.Attach(new GridObserver());
            cp.Attach(new SoundObserver(SpaceInvaders.eng));

            CollisionPair cp1 = CollisionPairManager.Add(CollisionPair.Name.Alien_Ship, GameObjectManager.Find(GameObject.Name.ShipRoot), pGrid);
            cp1.Attach(new AlienHitPlayerObserver());

            CollisionPair cp2 = CollisionPairManager.Add(CollisionPair.Name.Alien_Missile, GameObjectManager.Find(GameObject.Name.MissileRoot), pGrid);
            Debug.Assert(cp2 != null);
            cp2.Attach(new NewWaveObserver());
            cp2.Attach(new ShipReadyObserver());
            cp2.Attach(new ShipRemoveMissileObserver());
            cp2.Attach(new ScoreUpdateObserver());
            cp2.Attach(new AlienDeathSoundObserver());
            cp2.Attach(new AdjustGridMovementObserver());
            cp2.Attach(new AlienRemovalObserver());
            cp2.Attach(new AlienSplatObserver());

            //aliens colliding with bottom wall:

            CollisionPair cp8 = CollisionPairManager.Add(CollisionPair.Name.Grid_BottomWall, pGrid, GameObjectManager.Find(GameObject.Name.WallBottom));
            cp8.Attach(new GridHitBottomObserver());

            CollisionPair cp12 = CollisionPairManager.Add(CollisionPair.Name.Alien_Shield, pGrid, GameObjectManager.Find(GameObject.Name.ShieldRoot));
            cp12.Attach(new RemoveBrickObserver());

            GridMover gm = new GridMover();
            TimerManager.Add(TimerEvent.Name.GridMovement, gm, Values.startingGridMovementInterval);


            // shields

            //GameObject shieldRoot = GameObjectManager.Find(GameObject.Name.ShieldRoot);
            //shieldRoot.Remove();

            //GameObject shieldGr = GameObjectManager.Find(GameObject.Name.ShieldGrid);
            //shieldGr.Remove();



            //PCSTree pRootTree = GameObjectManager.GetRootTree();



            //ShieldRoot pShieldRoot = new ShieldRoot(GameObject.Name.ShieldRoot, GameSprite.Name.NullObject, 0, 0.0f, 0.0f);

            //pRootTree.Insert(pShieldRoot, null);
            //GameObjectManager.AttachTree(pShieldRoot);


            //// create the factory 
            //ShieldFactory SF = new ShieldFactory(SpriteBatch.Name.Shields, SpriteBatch.Name.Boxes, pRootTree);

            //// set the parent for hierarchy inside the factory,
            //SF.setParent(pShieldRoot);

            //// attach to Root  
            //ShieldCategory pShieldGrid = SF.Create(ShieldCategory.Type.Grid, GameObject.Name.ShieldGrid);

            //// load by column
            //{
            //    int j = 0;

            //    ShieldCategory shieldColumn;

            //    SF.setParent(pShieldGrid);
            //    shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn, j++);
            //    SF.setParent(shieldColumn);

            //    int i = 0;

            //    float start_x = 100.0f;
            //    float start_y = 200.0f;
            //    float off_x = 0;
            //    float brickWidth = 16.0f;
            //    float brickHeight = 8.0f;

            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + 2 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + 3 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + 4 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + 5 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + 6 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + 7 * brickHeight);
            //    SF.Create(ShieldCategory.Type.LeftTop1, GameObject.Name.ShieldBrick, i++, start_x, start_y + 8 * brickHeight);
            //    SF.Create(ShieldCategory.Type.LeftTop0, GameObject.Name.ShieldBrick, i++, start_x, start_y + 9 * brickHeight);

            //    SF.setParent(pShieldGrid);
            //    shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.Column, j++);
            //    SF.setParent(shieldColumn);

            //    off_x += brickWidth;
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 2 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 3 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 4 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 5 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 6 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 7 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 8 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 9 * brickHeight);

            //    SF.setParent(pShieldGrid);
            //    shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.Column, j++);
            //    SF.setParent(shieldColumn);

            //    off_x += brickWidth;
            //    SF.Create(ShieldCategory.Type.LeftBottom, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 2 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 3 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 4 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 5 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 6 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 7 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 8 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 9 * brickHeight);

            //    SF.setParent(pShieldGrid);
            //    shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.Column, j++);
            //    SF.setParent(shieldColumn);

            //    off_x += brickWidth;
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 3 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 4 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 5 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 6 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 7 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 8 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 9 * brickHeight);

            //    SF.setParent(pShieldGrid);
            //    shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.Column, j++);
            //    SF.setParent(shieldColumn);

            //    off_x += brickWidth;
            //    SF.Create(ShieldCategory.Type.RightBottom, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 2 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 3 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 4 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 5 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 6 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 7 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 8 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 9 * brickHeight);

            //    SF.setParent(pShieldGrid);
            //    shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.Column, j++);
            //    SF.setParent(shieldColumn);

            //    off_x += brickWidth;
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 0 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 1 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 2 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 3 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 4 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 5 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 6 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 7 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 8 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 9 * brickHeight);

            //    SF.setParent(pShieldGrid);
            //    shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.Column, j++);
            //    SF.setParent(shieldColumn);

            //    off_x += brickWidth;
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 0 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 1 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 2 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 3 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 4 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 5 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 6 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 7 * brickHeight);
            //    SF.Create(ShieldCategory.Type.RightTop1, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 8 * brickHeight);
            //    SF.Create(ShieldCategory.Type.RightTop0, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 9 * brickHeight);

            //}

            //SF.setParent(pShieldRoot);

            //// attach to Root  
            ////ShieldCategory pShieldGrid2 = SF.Create(ShieldCategory.Type.Grid, GameObject.Name.ShieldGrid);

            //{
            //    int j = 0;
            //    ShieldCategory shieldColumn;

            //    SF.setParent(pShieldGrid);
            //    shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn, j++);
            //    SF.setParent(shieldColumn);
            //    int i = 0;

            //    float start_x = 300.0f;
            //    float start_y = 200.0f;
            //    float off_x = 0;
            //    float brickWidth = 16.0f;
            //    float brickHeight = 8.0f;

            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + 2 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + 3 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + 4 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + 5 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + 6 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + 7 * brickHeight);
            //    SF.Create(ShieldCategory.Type.LeftTop1, GameObject.Name.ShieldBrick, i++, start_x, start_y + 8 * brickHeight);
            //    SF.Create(ShieldCategory.Type.LeftTop0, GameObject.Name.ShieldBrick, i++, start_x, start_y + 9 * brickHeight);

            //    SF.setParent(pShieldGrid);
            //    shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.Column, j++);
            //    SF.setParent(shieldColumn);

            //    off_x += brickWidth;
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 2 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 3 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 4 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 5 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 6 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 7 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 8 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 9 * brickHeight);

            //    SF.setParent(pShieldGrid);
            //    shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.Column, j++);
            //    SF.setParent(shieldColumn);

            //    off_x += brickWidth;
            //    SF.Create(ShieldCategory.Type.LeftBottom, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 2 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 3 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 4 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 5 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 6 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 7 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 8 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 9 * brickHeight);

            //    SF.setParent(pShieldGrid);
            //    shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.Column, j++);
            //    SF.setParent(shieldColumn);

            //    off_x += brickWidth;
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 3 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 4 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 5 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 6 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 7 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 8 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 9 * brickHeight);

            //    SF.setParent(pShieldGrid);
            //    shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.Column, j++);
            //    SF.setParent(shieldColumn);

            //    off_x += brickWidth;
            //    SF.Create(ShieldCategory.Type.RightBottom, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 2 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 3 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 4 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 5 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 6 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 7 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 8 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 9 * brickHeight);

            //    SF.setParent(pShieldGrid);
            //    shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.Column, j++);
            //    SF.setParent(shieldColumn);

            //    off_x += brickWidth;
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 0 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 1 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 2 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 3 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 4 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 5 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 6 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 7 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 8 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 9 * brickHeight);

            //    SF.setParent(pShieldGrid);
            //    shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.Column, j++);
            //    SF.setParent(shieldColumn);

            //    off_x += brickWidth;
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 0 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 1 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 2 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 3 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 4 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 5 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 6 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 7 * brickHeight);
            //    SF.Create(ShieldCategory.Type.RightTop1, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 8 * brickHeight);
            //    SF.Create(ShieldCategory.Type.RightTop0, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 9 * brickHeight);

            //}

            //SF.setParent(pShieldRoot);

            //// attach to Root  
            ////ShieldCategory pShieldGrid3 = SF.Create(ShieldCategory.Type.Grid, GameObject.Name.ShieldGrid);

            //{
            //    int j = 0;
            //    ShieldCategory shieldColumn;

            //    SF.setParent(pShieldGrid);
            //    shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn, j++);
            //    SF.setParent(shieldColumn);
            //    int i = 0;

            //    float start_x = 500.0f;
            //    float start_y = 200.0f;
            //    float off_x = 0;
            //    float brickWidth = 16.0f;
            //    float brickHeight = 8.0f;

            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + 2 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + 3 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + 4 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + 5 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + 6 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + 7 * brickHeight);
            //    SF.Create(ShieldCategory.Type.LeftTop1, GameObject.Name.ShieldBrick, i++, start_x, start_y + 8 * brickHeight);
            //    SF.Create(ShieldCategory.Type.LeftTop0, GameObject.Name.ShieldBrick, i++, start_x, start_y + 9 * brickHeight);

            //    SF.setParent(pShieldGrid);
            //    shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.Column, j++);
            //    SF.setParent(shieldColumn);

            //    off_x += brickWidth;
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 2 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 3 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 4 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 5 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 6 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 7 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 8 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 9 * brickHeight);

            //    SF.setParent(pShieldGrid);
            //    shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.Column, j++);
            //    SF.setParent(shieldColumn);

            //    off_x += brickWidth;
            //    SF.Create(ShieldCategory.Type.LeftBottom, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 2 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 3 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 4 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 5 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 6 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 7 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 8 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 9 * brickHeight);

            //    SF.setParent(pShieldGrid);
            //    shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.Column, j++);
            //    SF.setParent(shieldColumn);

            //    off_x += brickWidth;
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 3 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 4 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 5 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 6 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 7 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 8 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 9 * brickHeight);

            //    SF.setParent(pShieldGrid);
            //    shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.Column, j++);
            //    SF.setParent(shieldColumn);

            //    off_x += brickWidth;
            //    SF.Create(ShieldCategory.Type.RightBottom, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 2 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 3 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 4 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 5 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 6 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 7 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 8 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 9 * brickHeight);

            //    SF.setParent(pShieldGrid);
            //    shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.Column, j++);
            //    SF.setParent(shieldColumn);

            //    off_x += brickWidth;
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 0 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 1 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 2 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 3 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 4 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 5 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 6 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 7 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 8 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 9 * brickHeight);

            //    SF.setParent(pShieldGrid);
            //    shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.Column, j++);
            //    SF.setParent(shieldColumn);

            //    off_x += brickWidth;
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 0 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 1 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 2 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 3 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 4 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 5 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 6 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 7 * brickHeight);
            //    SF.Create(ShieldCategory.Type.RightTop1, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 8 * brickHeight);
            //    SF.Create(ShieldCategory.Type.RightTop0, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 9 * brickHeight);

            //}

            //SF.setParent(pShieldRoot);

            //// attach to Root  
            ////ShieldCategory pShieldGrid4 = SF.Create(ShieldCategory.Type.Grid, GameObject.Name.ShieldGrid);

            //{
            //    int j = 0;
            //    ShieldCategory shieldColumn;

            //    SF.setParent(pShieldGrid);
            //    shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn, j++);
            //    SF.setParent(shieldColumn);
            //    int i = 0;

            //    float start_x = 700.0f;
            //    float start_y = 200.0f;
            //    float off_x = 0;
            //    float brickWidth = 16.0f;
            //    float brickHeight = 8.0f;

            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + 2 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + 3 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + 4 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + 5 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + 6 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + 7 * brickHeight);
            //    SF.Create(ShieldCategory.Type.LeftTop1, GameObject.Name.ShieldBrick, i++, start_x, start_y + 8 * brickHeight);
            //    SF.Create(ShieldCategory.Type.LeftTop0, GameObject.Name.ShieldBrick, i++, start_x, start_y + 9 * brickHeight);

            //    SF.setParent(pShieldGrid);
            //    shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.Column, j++);
            //    SF.setParent(shieldColumn);

            //    off_x += brickWidth;
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 2 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 3 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 4 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 5 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 6 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 7 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 8 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 9 * brickHeight);

            //    SF.setParent(pShieldGrid);
            //    shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.Column, j++);
            //    SF.setParent(shieldColumn);

            //    off_x += brickWidth;
            //    SF.Create(ShieldCategory.Type.LeftBottom, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 2 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 3 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 4 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 5 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 6 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 7 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 8 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 9 * brickHeight);

            //    SF.setParent(pShieldGrid);
            //    shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.Column, j++);
            //    SF.setParent(shieldColumn);

            //    off_x += brickWidth;
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 3 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 4 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 5 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 6 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 7 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 8 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 9 * brickHeight);

            //    SF.setParent(pShieldGrid);
            //    shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.Column, j++);
            //    SF.setParent(shieldColumn);

            //    off_x += brickWidth;
            //    SF.Create(ShieldCategory.Type.RightBottom, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 2 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 3 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 4 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 5 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 6 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 7 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 8 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 9 * brickHeight);

            //    SF.setParent(pShieldGrid);
            //    shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.Column, j++);
            //    SF.setParent(shieldColumn);

            //    off_x += brickWidth;
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 0 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 1 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 2 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 3 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 4 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 5 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 6 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 7 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 8 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 9 * brickHeight);

            //    SF.setParent(pShieldGrid);
            //    shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.Column, j++);
            //    SF.setParent(shieldColumn);

            //    off_x += brickWidth;
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 0 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 1 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 2 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 3 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 4 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 5 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 6 * brickHeight);
            //    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 7 * brickHeight);
            //    SF.Create(ShieldCategory.Type.RightTop1, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 8 * brickHeight);
            //    SF.Create(ShieldCategory.Type.RightTop0, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 9 * brickHeight);

            //}


            //CollisionPair cp4 = CollisionPairManager.Add(CollisionPair.Name.Missile_Shield, GameObjectManager.Find(GameObject.Name.MissileRoot), pShieldRoot);
            //cp4.Attach(new ShipRemoveMissileObserver());
            //cp4.Attach(new RemoveBrickObserver());
            //cp4.Attach(new ShipReadyObserver());
            
        }
    }
}
