using System;
using System.Diagnostics;

// Terry Schmidt, Space Invaders, SE456, Winter 2016.

namespace SpaceInvaders
{
    public class SpaceInvaders : Azul.Game
    {
        public static IrrKlang.ISoundEngine eng = null;
        GridMover gm;
        //UFOSpawner ufospawn;
        public static Random randy;
        
        //-----------------------------------------------------------------------------
        // Game::Initialize()
        //		Allows the engine to perform any initialization it needs to before 
        //      starting to run.  This is where it can query for any required services 
        //      and load any non-graphic related content. 
        //-----------------------------------------------------------------------------
        public override void Initialize()
        {
            // Game Window Device setup
            this.SetWindowName("Space Invaders");
            this.SetWidthHeight(896, 1024);
            this.SetClearColor(0.0f, 0.0f, 0.0f, 1.0f);
        }

        //-----------------------------------------------------------------------------
        // Game::LoadContent()
        //		Allows you to load all content needed for your engine,
        //	    such as objects, graphics, etc.
        //-----------------------------------------------------------------------------
        public override void LoadContent()
        {
            //---------------------------------------------------------------------------------------------------------
            // Initialize the Managers
            //---------------------------------------------------------------------------------------------------------

             TextureManager.Create(3, 1);
             ImageManager.Create(5, 1);
             GameSpriteManager.Create(1, 1);
             BoxSpriteManager.Create(5, 2);
             SpriteBatchManager.Create(3, 1);
             TimerManager.Create(1, 1);
             DeathManager.Create(3, 1);
             ProxySpriteManager.Create(10, 1);
             GameObjectManager.Create(1, 1);
             GhostManager.Create(1, 1);
             CollisionPairManager.Create(1, 1);
             CharacterManager.Create(3, 1);
             FontManager.Create(1, 1);

            //---------------------------------------------------------------------------------------------------------
            // Sound
            //---------------------------------------------------------------------------------------------------------

            // start up the engine
            eng = new IrrKlang.ISoundEngine();
            eng.SoundVolume = 0.3f;

            //---------------------------------------------------------------------------------------------------------
            // RNG
            //---------------------------------------------------------------------------------------------------------

            randy = new Random();
            
            //---------------------------------------------------------------------------------------------------------
            // Load Textures
            //---------------------------------------------------------------------------------------------------------

            TextureManager.Add(Texture.Name.Aliens, "SpaceInvadersSprites.tga");
            TextureManager.Add(Texture.Name.Shield, "shield.tga");
            TextureManager.Add(Texture.Name.SpaceInvaders, "Aliens.tga");
            Debug.Assert(TextureManager.Find(Texture.Name.Shield) != null);

            //---------------------------------------------------------------------------------------------------------
            // Load Font
            //---------------------------------------------------------------------------------------------------------

            TextureManager.Add(Texture.Name.Consolas36pt, "Consolas36pt.tga");
            FontManager.AddXML(Character.Name.Consolas36pt, "Consolas36pt.xml", Texture.Name.Consolas36pt);

            //---------------------------------------------------------------------------------------------------------
            // Images
            //---------------------------------------------------------------------------------------------------------

            ImageManager.Add(Image.Name.Crab, Texture.Name.Aliens, 0, 0, 121, 84);
            ImageManager.Add(Image.Name.Crab2, Texture.Name.Aliens, 134, 0, 116, 87);
            ImageManager.Add(Image.Name.Squid, Texture.Name.Aliens, 276, 0, 86, 86);
            ImageManager.Add(Image.Name.Squid2, Texture.Name.Aliens, 405, 0, 86, 85);
            ImageManager.Add(Image.Name.Octopus, Texture.Name.Aliens, 0, 85, 125, 84);
            ImageManager.Add(Image.Name.Octopus2, Texture.Name.Aliens, 130, 84, 124, 87);
            ImageManager.Add(Image.Name.Missile, Texture.Name.Aliens, 236, 535, 6, 34);
            ImageManager.Add(Image.Name.Ship, Texture.Name.Aliens, 154, 441, 75, 54);
            ImageManager.Add(Image.Name.StaticShip, Texture.Name.Aliens, 154, 441, 75, 54);
            ImageManager.Add(Image.Name.UFO, Texture.Name.Aliens, 1, 439, 125, 60);
            ImageManager.Add(Image.Name.Splat, Texture.Name.Aliens, 399, 437, 97, 62);
            ImageManager.Add(Image.Name.Splat2, Texture.Name.Aliens, 399, 437, 97, 62);
            ImageManager.Add(Image.Name.Splat3, Texture.Name.Aliens, 399, 437, 97, 62);
            ImageManager.Add(Image.Name.Splat4, Texture.Name.Aliens, 399, 437, 97, 62);
            ImageManager.Add(Image.Name.Splat5, Texture.Name.Aliens, 399, 437, 97, 62);
            ImageManager.Add(Image.Name.UFOSplat, Texture.Name.Aliens, 399, 437, 97, 62);
            ImageManager.Add(Image.Name.MissileBombSplat, Texture.Name.Aliens, 313, 543, 27, 35);
            ImageManager.Add(Image.Name.MissileBombSplat2, Texture.Name.Aliens, 313, 543, 27, 35);
            ImageManager.Add(Image.Name.ShipSplat, Texture.Name.Aliens, 264, 435, 110, 67);
            ImageManager.Add(Image.Name.MissileWallSplat, Texture.Name.Aliens, 313, 543, 27, 35);
            ImageManager.Add(Image.Name.BombStraight, Texture.Name.Aliens, 236, 535, 6, 34);
            ImageManager.Add(Image.Name.BombZigZag, Texture.Name.Aliens, 172, 539, 14, 30);
            ImageManager.Add(Image.Name.BombCross, Texture.Name.Aliens, 140, 534, 14, 36);
            ImageManager.Add(Image.Name.UFOBomb, Texture.Name.Aliens, 203, 535, 16, 35);
            ImageManager.Add(Image.Name.SpaceInvaders, Texture.Name.SpaceInvaders, 24, 77, 86, 43);

            ImageManager.Add(Image.Name.Brick, Texture.Name.Shield, 20, 210, 10, 5);
            ImageManager.Add(Image.Name.BrickLeft_Top0, Texture.Name.Shield, 15, 180, 10, 5);
            ImageManager.Add(Image.Name.BrickLeft_Top1, Texture.Name.Shield, 15, 185, 10, 5);
            ImageManager.Add(Image.Name.BrickLeft_Bottom, Texture.Name.Shield, 35, 215, 10, 5);
            ImageManager.Add(Image.Name.BrickRight_Top0, Texture.Name.Shield, 75, 180, 10, 5);
            ImageManager.Add(Image.Name.BrickRight_Top1, Texture.Name.Shield, 75, 185, 10, 5);
            ImageManager.Add(Image.Name.BrickRight_Bottom, Texture.Name.Shield, 55, 215, 10, 5);

            //---------------------------------------------------------------------------------------------------------
            // SpriteBatch 
            //---------------------------------------------------------------------------------------------------------

            SpriteBatch textSpriteBatch = SpriteBatchManager.Add(SpriteBatch.Name.Texts);
            SpriteBatch alienSpriteBatch = SpriteBatchManager.Add(SpriteBatch.Name.Aliens);
            SpriteBatch boxSpriteBatch = SpriteBatchManager.Add(SpriteBatch.Name.Boxes);
            SpriteBatch shieldSpriteBatch = SpriteBatchManager.Add(SpriteBatch.Name.Shields);

            //---------------------------------------------------------------------------------------------------------
            // Sprites
            //---------------------------------------------------------------------------------------------------------

            GameSpriteManager.Add(GameSprite.Name.Octopus, Image.Name.Octopus, 450, 400, 49, 33).SetColor(0.0f, 1.02f, 1.02f);
            GameSpriteManager.Add(GameSprite.Name.Octopus2, Image.Name.Octopus2, 450, 400, 49, 33).SetColor(0, 1, 1);
            GameSpriteManager.Add(GameSprite.Name.Crab, Image.Name.Crab, 600, 400, 45, 33).SetColor(0.45f, 0.0f, 1.89f);
            GameSpriteManager.Add(GameSprite.Name.Crab2, Image.Name.Crab2, 600, 400, 45, 33).SetColor(0.45f, 0.0f, 1.89f);
            GameSpriteManager.Add(GameSprite.Name.Squid, Image.Name.Squid, 700, 400, 33, 33).SetColor(2.55f, 0, 2.55f);
            GameSpriteManager.Add(GameSprite.Name.Squid2, Image.Name.Squid2, 700, 400, 33, 33).SetColor(2.55f, 0, 2.55f);
            GameSpriteManager.Add(GameSprite.Name.Missile, Image.Name.Missile, 50, 50, 3, 17);
            GameSpriteManager.Add(GameSprite.Name.Ship, Image.Name.Ship, 50, 50, 75, 54).SetColor(0.0f, 250.0f, 0.0f);
            GameSpriteManager.Add(GameSprite.Name.StaticShip, Image.Name.Ship, 50, 50, 50, 36).SetColor(0.0f, 250.0f, 0.0f);
            GameSpriteManager.Add(GameSprite.Name.UFO, Image.Name.UFO, 50, 50, 64, 28).SetColor(1, 0, 0);
            GameSpriteManager.Add(GameSprite.Name.Splat, Image.Name.Splat, 600, 400, 49, 33);
            GameSpriteManager.Add(GameSprite.Name.Splat2, Image.Name.Splat2, 600, 400, 49, 33);
            GameSpriteManager.Add(GameSprite.Name.Splat3, Image.Name.Splat3, 600, 400, 49, 33);
            GameSpriteManager.Add(GameSprite.Name.Splat4, Image.Name.Splat4, 600, 400, 49, 33);
            GameSpriteManager.Add(GameSprite.Name.Splat5, Image.Name.Splat5, 600, 400, 49, 33);
            GameSpriteManager.Add(GameSprite.Name.UFOSplat, Image.Name.UFOSplat, 600, 400, 49, 33).SetColor(1, 0, 0);
            GameSpriteManager.Add(GameSprite.Name.MissileBombSplat, Image.Name.MissileBombSplat, 600, 400, 37, 45).SetColor(1, 1, 1);
            GameSpriteManager.Add(GameSprite.Name.MissileBombSplat2, Image.Name.MissileBombSplat2, 600, 400, 37, 45).SetColor(1, 1, 1);
            GameSpriteManager.Add(GameSprite.Name.ShipSplat, Image.Name.ShipSplat, 600, 400, 100, 60).SetColor(0, 240, 0);
            GameSpriteManager.Add(GameSprite.Name.MissileWallSplat, Image.Name.MissileWallSplat, 600, 400, 27, 35).SetColor(1, 0, 0);
            GameSpriteManager.Add(GameSprite.Name.BombSplat, Image.Name.MissileWallSplat, 600, 400, 27, 35);
            GameSpriteManager.Add(GameSprite.Name.BombZigZag, Image.Name.BombZigZag, -100, -100, 14, 30);
            GameSpriteManager.Add(GameSprite.Name.BombStraight, Image.Name.BombStraight, -100, -100, 6, 34);
            GameSpriteManager.Add(GameSprite.Name.BombDagger, Image.Name.BombCross, -100, -100, 14, 36);
            GameSpriteManager.Add(GameSprite.Name.UFOBomb, Image.Name.UFOBomb, -100, -100, 16, 35);
            GameSpriteManager.Add(GameSprite.Name.SpaceInvaders, Image.Name.SpaceInvaders, 350, 450, 172, 86).SetColor(1, 1, 1);
            
            GameSpriteManager.Add(GameSprite.Name.Brick, Image.Name.Brick, 50, 25, 16, 8);
            GameSpriteManager.Add(GameSprite.Name.Brick_LeftTop0, Image.Name.BrickLeft_Top0, 50, 25, 16, 8);
            GameSpriteManager.Add(GameSprite.Name.Brick_LeftTop1, Image.Name.BrickLeft_Top1, 50, 25, 16, 8);
            GameSpriteManager.Add(GameSprite.Name.Brick_LeftBottom, Image.Name.BrickLeft_Bottom, 50, 25, 16, 8);
            GameSpriteManager.Add(GameSprite.Name.Brick_RightTop0, Image.Name.BrickRight_Top0, 50, 25, 16, 8);
            GameSpriteManager.Add(GameSprite.Name.Brick_RightTop1, Image.Name.BrickRight_Top1, 50, 25, 16, 8);
            GameSpriteManager.Add(GameSprite.Name.Brick_RightBottom, Image.Name.BrickRight_Bottom, 50, 25, 16, 8);

            //---------------------------------------------------------------------------------------------------------
            // Input
            //---------------------------------------------------------------------------------------------------------

            InputSubject inputSubject;
            inputSubject = InputManager.getRightSubject();
            inputSubject.Attach(new MoveRightObserver());

            inputSubject = InputManager.getLeftSubject();
            inputSubject.Attach(new MoveLeftObserver());

            inputSubject = InputManager.getSpaceSubject();
            inputSubject.Attach(new ShootObserver());

            //---------------------------------------------------------------------------------------------------------
            // Root Tree
            //---------------------------------------------------------------------------------------------------------

            PCSTree root = GameObjectManager.GetRootTree();

            //---------------------------------------------------------------------------------------------------------
            // Bomb
            //---------------------------------------------------------------------------------------------------------

            BombRoot bombRoot = new BombRoot(GameObject.Name.BombRoot, GameSprite.Name.NullObject, 0, -444.0f, -444.0f);

            //pRootTree.Insert(pBombRoot, null);
            //pBombRoot.ActivateCollisionSprite(pSB_Boxes);

            //Bomb pBomb = null;
            //pBomb = new Bomb(GameObject.Name.Bomb, GameSprite.Name.BombStraight, new FallStraight(), 0, 400, 500);
            //pRootTree.Insert(pBomb, pBombRoot);
            //pBomb.ActivateCollisionSprite(pSB_Boxes);
            //pBomb.ActivateGameSprite(pSB_Aliens);

            //pBomb = new Bomb(GameObject.Name.Bomb, GameSprite.Name.BombDagger, new FallDagger(), 0, 500, 500);
            //pRootTree.Insert(pBomb, pBombRoot);
            //pBomb.ActivateCollisionSprite(pSB_Boxes);
            //pBomb.ActivateGameSprite(pSB_Aliens);

            //pBomb = new Bomb(GameObject.Name.Bomb, GameSprite.Name.BombZigZag, new FallZigZag(), 0, 600, 500);

            //pRootTree.Insert(pBomb, pBombRoot);
            //pBomb.ActivateCollisionSprite(pSB_Boxes);
            //pBomb.ActivateGameSprite(pSB_Aliens);

            GameObjectManager.AttachTree(bombRoot, root);

            //---------------------------------------------------------------------------------------------------------
            // Create Walls
            //---------------------------------------------------------------------------------------------------------

            // Wall Root
            WallRoot wallRoot = new WallRoot(GameObject.Name.WallRoot, GameSprite.Name.NullObject, 0, 0.0f, 0.0f);

            root.Insert(wallRoot, null);
            wallRoot.ActivateCollisionSprite(boxSpriteBatch);

            WallTop wt = new WallTop(GameObject.Name.WallTop, GameSprite.Name.NullObject, 0, 450, 965, 870, 131);

            root.Insert(wt, wallRoot);
            wt.ActivateCollisionSprite(boxSpriteBatch);
            wt.ActivateGameSprite(alienSpriteBatch);

            WallBottom wb = new WallBottom(GameObject.Name.WallBottom, GameSprite.Name.NullObject, 0, 450, 10, 899, 80);

            root.Insert(wb, wallRoot);
            wb.ActivateCollisionSprite(alienSpriteBatch);
            wb.ActivateGameSprite(alienSpriteBatch);

            WallRight pWallRight = new WallRight(GameObject.Name.WallRight, GameSprite.Name.NullObject, 0, 900, 475, 30, 849);

            root.Insert(pWallRight, wallRoot);
            pWallRight.ActivateCollisionSprite(boxSpriteBatch);

            WallLeft pWallLeft = new WallLeft(GameObject.Name.WallLeft, GameSprite.Name.NullObject, 0, 000, 475, 30, 849);

            root.Insert(pWallLeft, wallRoot);
            pWallLeft.ActivateCollisionSprite(boxSpriteBatch);

            GameObjectManager.AttachTree(wallRoot, root);

            //---------------------------------------------------------------------------------------------------------
            // Create Missiles
            //---------------------------------------------------------------------------------------------------------

            MissileRoot mr = new MissileRoot(GameObject.Name.MissileRoot, GameSprite.Name.NullObject, 0, 400.0f, 300.0f);

            root.Insert(mr, null);
            mr.ActivateGameSprite(boxSpriteBatch);
            mr.ActivateCollisionSprite(boxSpriteBatch);

            GameObjectManager.AttachTree(mr, root);

            //---------------------------------------------------------------------------------------------------------
            // Create Ship
            //---------------------------------------------------------------------------------------------------------

            ShipRoot sr = new ShipRoot(GameObject.Name.ShipRoot, GameSprite.Name.NullObject, 0, 200.0f, 85.0f);
            root.Insert(sr, null);
            sr.ActivateGameSprite(alienSpriteBatch);
            sr.ActivateCollisionSprite(boxSpriteBatch);

            GameObjectManager.AttachTree(sr, root);
            ShipManager.Create();

            FakeShip fs = new FakeShip(GameObject.Name.StaticShip, GameSprite.Name.StaticShip, 0, 110, 28);
            root.Insert(fs, null);
            fs.ActivateGameSprite(alienSpriteBatch);
            fs.Update();

            SpaceInvadersSprite sis = new SpaceInvadersSprite(GameObject.Name.SpaceInvaders, GameSprite.Name.SpaceInvaders, 0, 450, 750);
            root.Insert(sis, null);
            sis.ActivateGameSprite(shieldSpriteBatch);
            //sis.ActivateCollisionSprite(pSB_Boxes);
            sis.Update();

            //---------------------------------------------------------------------------------------------------------
            // Shield 
            //---------------------------------------------------------------------------------------------------------
            ShieldRoot shieldRoot = new ShieldRoot(GameObject.Name.ShieldRoot, GameSprite.Name.NullObject, 0, 0.0f, 0.0f);

            root.Insert(shieldRoot, null);
            GameObjectManager.AttachTree(shieldRoot);

            // create the factory 
            ShieldFactory SF = new ShieldFactory(SpriteBatch.Name.Shields, SpriteBatch.Name.Boxes, root);

            // set the parent for hierarchy inside the factory,
            SF.setParent(shieldRoot);

            // attach to Root  
            ShieldCategory shieldGrid = SF.Create(ShieldCategory.Type.Grid, GameObject.Name.ShieldGrid);

            // load by column
            {
                int j = 0;

                ShieldCategory shieldColumn;

                SF.setParent(shieldGrid);
                shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn, j++);
                SF.setParent(shieldColumn);

                int i = 0;

                float start_x = 100.0f;
                float start_y = 200.0f;
                float off_x = 0;
                float brickWidth = 16.0f;
                float brickHeight = 8.0f;

                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + 2 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + 3 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + 4 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + 5 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + 6 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + 7 * brickHeight);
                SF.Create(ShieldCategory.Type.LeftTop1, GameObject.Name.ShieldBrick, i++, start_x, start_y + 8 * brickHeight);
                SF.Create(ShieldCategory.Type.LeftTop0, GameObject.Name.ShieldBrick, i++, start_x, start_y + 9 * brickHeight);

                SF.setParent(shieldGrid);
                shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn, j++);
                SF.setParent(shieldColumn);

                off_x += brickWidth;
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 2 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 3 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 4 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 5 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 6 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 7 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 8 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 9 * brickHeight);

                SF.setParent(shieldGrid);
                shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn, j++);
                SF.setParent(shieldColumn);

                off_x += brickWidth;
                SF.Create(ShieldCategory.Type.LeftBottom, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 2 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 3 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 4 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 5 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 6 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 7 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 8 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 9 * brickHeight);

                SF.setParent(shieldGrid);
                shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn, j++);
                SF.setParent(shieldColumn);

                off_x += brickWidth;
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 3 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 4 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 5 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 6 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 7 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 8 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 9 * brickHeight);

                SF.setParent(shieldGrid);
                shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn, j++);
                SF.setParent(shieldColumn);

                off_x += brickWidth;
                SF.Create(ShieldCategory.Type.RightBottom, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 2 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 3 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 4 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 5 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 6 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 7 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 8 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 9 * brickHeight);

                SF.setParent(shieldGrid);
                shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn, j++);
                SF.setParent(shieldColumn);

                off_x += brickWidth;
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 0 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 1 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 2 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 3 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 4 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 5 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 6 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 7 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 8 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 9 * brickHeight);

                SF.setParent(shieldGrid);
                shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn, j++);
                SF.setParent(shieldColumn);

                off_x += brickWidth;
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 0 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 1 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 2 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 3 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 4 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 5 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 6 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 7 * brickHeight);
                SF.Create(ShieldCategory.Type.RightTop1, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 8 * brickHeight);
                SF.Create(ShieldCategory.Type.RightTop0, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 9 * brickHeight);

            }

            SF.setParent(shieldRoot);

            // attach to Root  
            //ShieldCategory pShieldGrid2 = SF.Create(ShieldCategory.Type.Grid, GameObject.Name.ShieldGrid);

            {
                int j = 0;
                ShieldCategory shieldColumn;

                SF.setParent(shieldGrid);
                shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn, j++);
                SF.setParent(shieldColumn);
                int i = 0;

                float start_x = 300.0f;
                float start_y = 200.0f;
                float off_x = 0;
                float brickWidth = 16.0f;
                float brickHeight = 8.0f;

                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + 2 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + 3 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + 4 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + 5 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + 6 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + 7 * brickHeight);
                SF.Create(ShieldCategory.Type.LeftTop1, GameObject.Name.ShieldBrick, i++, start_x, start_y + 8 * brickHeight);
                SF.Create(ShieldCategory.Type.LeftTop0, GameObject.Name.ShieldBrick, i++, start_x, start_y + 9 * brickHeight);

                SF.setParent(shieldGrid);
                shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn, j++);
                SF.setParent(shieldColumn);

                off_x += brickWidth;
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 2 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 3 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 4 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 5 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 6 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 7 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 8 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 9 * brickHeight);

                SF.setParent(shieldGrid);
                shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn, j++);
                SF.setParent(shieldColumn);

                off_x += brickWidth;
                SF.Create(ShieldCategory.Type.LeftBottom, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 2 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 3 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 4 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 5 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 6 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 7 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 8 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 9 * brickHeight);

                SF.setParent(shieldGrid);
                shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn, j++);
                SF.setParent(shieldColumn);

                off_x += brickWidth;
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 3 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 4 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 5 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 6 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 7 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 8 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 9 * brickHeight);

                SF.setParent(shieldGrid);
                shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn, j++);
                SF.setParent(shieldColumn);

                off_x += brickWidth;
                SF.Create(ShieldCategory.Type.RightBottom, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 2 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 3 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 4 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 5 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 6 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 7 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 8 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 9 * brickHeight);

                SF.setParent(shieldGrid);
                shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn, j++);
                SF.setParent(shieldColumn);

                off_x += brickWidth;
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 0 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 1 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 2 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 3 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 4 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 5 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 6 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 7 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 8 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 9 * brickHeight);

                SF.setParent(shieldGrid);
                shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn, j++);
                SF.setParent(shieldColumn);

                off_x += brickWidth;
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 0 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 1 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 2 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 3 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 4 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 5 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 6 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 7 * brickHeight);
                SF.Create(ShieldCategory.Type.RightTop1, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 8 * brickHeight);
                SF.Create(ShieldCategory.Type.RightTop0, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 9 * brickHeight);

            }

            SF.setParent(shieldRoot);

            // attach to Root  
            //ShieldCategory pShieldGrid3 = SF.Create(ShieldCategory.Type.Grid, GameObject.Name.ShieldGrid);

            {
                int j = 0;
                ShieldCategory shieldColumn;

                SF.setParent(shieldGrid);
                shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn, j++);
                SF.setParent(shieldColumn);
                int i = 0;

                float start_x = 500.0f;
                float start_y = 200.0f;
                float off_x = 0;
                float brickWidth = 16.0f;
                float brickHeight = 8.0f;

                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + 2 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + 3 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + 4 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + 5 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + 6 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + 7 * brickHeight);
                SF.Create(ShieldCategory.Type.LeftTop1, GameObject.Name.ShieldBrick, i++, start_x, start_y + 8 * brickHeight);
                SF.Create(ShieldCategory.Type.LeftTop0, GameObject.Name.ShieldBrick, i++, start_x, start_y + 9 * brickHeight);

                SF.setParent(shieldGrid);
                shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn, j++);
                SF.setParent(shieldColumn);

                off_x += brickWidth;
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 2 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 3 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 4 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 5 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 6 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 7 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 8 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 9 * brickHeight);

                SF.setParent(shieldGrid);
                shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn, j++);
                SF.setParent(shieldColumn);

                off_x += brickWidth;
                SF.Create(ShieldCategory.Type.LeftBottom, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 2 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 3 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 4 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 5 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 6 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 7 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 8 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 9 * brickHeight);

                SF.setParent(shieldGrid);
                shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn, j++);
                SF.setParent(shieldColumn);

                off_x += brickWidth;
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 3 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 4 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 5 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 6 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 7 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 8 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 9 * brickHeight);

                SF.setParent(shieldGrid);
                shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn, j++);
                SF.setParent(shieldColumn);

                off_x += brickWidth;
                SF.Create(ShieldCategory.Type.RightBottom, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 2 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 3 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 4 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 5 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 6 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 7 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 8 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 9 * brickHeight);

                SF.setParent(shieldGrid);
                shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn, j++);
                SF.setParent(shieldColumn);

                off_x += brickWidth;
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 0 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 1 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 2 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 3 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 4 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 5 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 6 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 7 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 8 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 9 * brickHeight);

                SF.setParent(shieldGrid);
                shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn, j++);
                SF.setParent(shieldColumn);

                off_x += brickWidth;
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 0 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 1 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 2 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 3 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 4 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 5 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 6 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 7 * brickHeight);
                SF.Create(ShieldCategory.Type.RightTop1, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 8 * brickHeight);
                SF.Create(ShieldCategory.Type.RightTop0, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 9 * brickHeight);
            }

            SF.setParent(shieldRoot);

            // attach to Root  
            //ShieldCategory pShieldGrid4 = SF.Create(ShieldCategory.Type.Grid, GameObject.Name.ShieldGrid);

            {
                int j = 0;
                ShieldCategory shieldColumn;

                SF.setParent(shieldGrid);
                shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn, j++);
                SF.setParent(shieldColumn);
                int i = 0;

                float start_x = 700.0f;
                float start_y = 200.0f;
                float off_x = 0;
                float brickWidth = 16.0f;
                float brickHeight = 8.0f;

                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + 2 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + 3 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + 4 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + 5 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + 6 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x, start_y + 7 * brickHeight);
                SF.Create(ShieldCategory.Type.LeftTop1, GameObject.Name.ShieldBrick, i++, start_x, start_y + 8 * brickHeight);
                SF.Create(ShieldCategory.Type.LeftTop0, GameObject.Name.ShieldBrick, i++, start_x, start_y + 9 * brickHeight);

                SF.setParent(shieldGrid);
                shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn, j++);
                SF.setParent(shieldColumn);

                off_x += brickWidth;
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 2 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 3 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 4 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 5 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 6 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 7 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 8 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 9 * brickHeight);

                SF.setParent(shieldGrid);
                shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn, j++);
                SF.setParent(shieldColumn);

                off_x += brickWidth;
                SF.Create(ShieldCategory.Type.LeftBottom, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 2 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 3 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 4 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 5 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 6 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 7 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 8 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 9 * brickHeight);

                SF.setParent(shieldGrid);
                shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn, j++);
                SF.setParent(shieldColumn);

                off_x += brickWidth;
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 3 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 4 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 5 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 6 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 7 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 8 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 9 * brickHeight);

                SF.setParent(shieldGrid);
                shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn, j++);
                SF.setParent(shieldColumn);

                off_x += brickWidth;
                SF.Create(ShieldCategory.Type.RightBottom, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 2 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 3 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 4 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 5 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 6 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 7 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 8 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 9 * brickHeight);

                SF.setParent(shieldGrid);
                shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn, j++);
                SF.setParent(shieldColumn);

                off_x += brickWidth;
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 0 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 1 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 2 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 3 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 4 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 5 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 6 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 7 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 8 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 9 * brickHeight);

                SF.setParent(shieldGrid);
                shieldColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn, j++);
                SF.setParent(shieldColumn);

                off_x += brickWidth;
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 0 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 1 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 2 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 3 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 4 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 5 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 6 * brickHeight);
                SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 7 * brickHeight);
                SF.Create(ShieldCategory.Type.RightTop1, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 8 * brickHeight);
                SF.Create(ShieldCategory.Type.RightTop0, GameObject.Name.ShieldBrick, i++, start_x + off_x, start_y + 9 * brickHeight);
            }

            //---------------------------------------------------------------------------------------------------------
            // Create Aliens
            //---------------------------------------------------------------------------------------------------------

            // Create a AlienTree
            PCSTree alienTree = new PCSTree();

            // create the factory 
            AlienFactory AF = new AlienFactory(SpriteBatch.Name.Aliens, alienTree);

            DeathManager.Attach(AF);

            // Set the parent for Grid inside the factory, Since grid is root so parent is null
            AF.setParent(null);

            // attach to Root  (this is the Grid)
            AlienCategory grid = AF.Create(AlienCategory.Type.Grid, GameObject.Name.Grid);

            // create 15 quickly attach to Grid
            for (int i = 0; i < 11; i++)
            {
                // set Parent
                AF.setParent(grid);
                AlienCategory column = AF.Create(AlienCategory.Type.Column, GameObject.Name.Column, i, 0.0f, 0.0f);

                // set Parent
                AF.setParent(column);

                AF.Create(AlienCategory.Type.Octopus, GameObject.Name.Octopus, i, 100.0f + i * 65.0f, 575.0f);
                AF.Create(AlienCategory.Type.Octopus, GameObject.Name.Octopus, i, 100.0f + i * 65.0f, 635.0f);
                AF.Create(AlienCategory.Type.Crab, GameObject.Name.Crab, i, 100.0f + i * 65.0f, 755.0f);
                AF.Create(AlienCategory.Type.Squid, GameObject.Name.Squid, i, 100.0f + i * 65.0f, 815.0f);
                AF.Create(AlienCategory.Type.Crab, GameObject.Name.Crab, i, 100.0f + i * 65.0f, 695.0f);
            }

            ////---------------------------------------------------------------------------------------------------------
            //// ColPair 
            ////---------------------------------------------------------------------------------------------------------
        
            // associate in a collision pair
            CollisionPair cp = CollisionPairManager.Add(CollisionPair.Name.Alien_Wall, grid, wallRoot);
            Debug.Assert(cp != null);
            cp.Attach(new GridObserver());
            cp.Attach(new SoundObserver(eng));

            CollisionPair cp1 = CollisionPairManager.Add(CollisionPair.Name.Alien_Ship, sr, grid);
            cp1.Attach(new AlienHitPlayerObserver());

            CollisionPair cp2 = CollisionPairManager.Add(CollisionPair.Name.Alien_Missile, mr, grid);
            Debug.Assert(cp2 != null);
            cp2.Attach(new NewWaveObserver());
            cp2.Attach(new ShipReadyObserver());
            cp2.Attach(new ShipRemoveMissileObserver());
            cp2.Attach(new ScoreUpdateObserver());
            cp2.Attach(new AlienDeathSoundObserver());
            cp2.Attach(new AdjustGridMovementObserver());
            cp2.Attach(new AlienRemovalObserver());
            cp2.Attach(new AlienSplatObserver());
            
            CollisionPair colPair = CollisionPairManager.Add(CollisionPair.Name.Missile_Wall, mr, wallRoot);
            Debug.Assert(colPair != null);
            colPair.Attach(new ShipReadyObserver());
            colPair.Attach(new ShipRemoveMissileObserver());
            colPair.Attach(new MissileHitWallObserver());

            CollisionPair cp3 = CollisionPairManager.Add(CollisionPair.Name.Bomb_Wall, bombRoot, wallRoot);
            cp3.Attach(new BombObserver());

            CollisionPair cp4 = CollisionPairManager.Add(CollisionPair.Name.Missile_Shield, mr, shieldRoot);
            cp4.Attach(new ShipRemoveMissileObserver());
            cp4.Attach(new RemoveBrickObserver());
            cp4.Attach(new ShipReadyObserver());

            CollisionPair cp8 = CollisionPairManager.Add(CollisionPair.Name.Grid_BottomWall, grid, wb);
            cp8.Attach(new GridHitBottomObserver());

            CollisionPair cp9 = CollisionPairManager.Add(CollisionPair.Name.Bomb_Shield, bombRoot, shieldRoot);
            cp9.Attach(new BombObserver());
            cp9.Attach(new RemoveBrickObserver());

            CollisionPair cp10 = CollisionPairManager.Add(CollisionPair.Name.Bomb_Ship, bombRoot, sr);
            cp10.Attach(new BombHitShipObserver());
            cp10.Attach(new BombObserver());
            cp10.Attach(new ShipSplatObserver());

            CollisionPair cp11 = CollisionPairManager.Add(CollisionPair.Name.Bomb_Missile, bombRoot, mr);
            cp11.Attach(new BombHitMissileObserver());
            cp11.Attach(new ShipReadyObserver());
            cp11.Attach(new ShipRemoveMissileObserver());

            CollisionPair cp12 = CollisionPairManager.Add(CollisionPair.Name.Alien_Shield, grid, shieldRoot);
            cp12.Attach(new RemoveBrickObserver());

            //---------------------------------------------------------------------------------------------------------
            // Create Scores
            //---------------------------------------------------------------------------------------------------------

            FontManager.Add(Font.Name.TopLine, SpriteBatch.Name.Texts, "SCORE<1>          HI-SCORE          SCORE<2>", Character.Name.Consolas36pt, 50, 985);
            FontManager.Add(Font.Name.Lives1, SpriteBatch.Name.Texts, Values.player1lives.ToString(), Character.Name.Consolas36pt, 42, 28);
            FontManager.Add(Font.Name.Credits, SpriteBatch.Name.Texts, "CREDITS 00", Character.Name.Consolas36pt, 650, 28);
            FontManager.Add(Font.Name.Score1, SpriteBatch.Name.Texts, Values.player1score.ToString(), Character.Name.Consolas36pt, 75, 935);
            FontManager.Add(Font.Name.HighScore, SpriteBatch.Name.Texts, "0000", Character.Name.Consolas36pt, 400, 935);
            FontManager.Add(Font.Name.Score2, SpriteBatch.Name.Texts, "0000", Character.Name.Consolas36pt, 725, 935);

            FontManager.Add(Font.Name.SpaceInvaders, SpriteBatch.Name.Texts, "PRESS S TO START", Character.Name.Consolas36pt, 290, 128);

            FontManager.Add(Font.Name.Spacebar, SpriteBatch.Name.Texts, "space bar: shoots missile", Character.Name.Consolas36pt, 220, 650);
            FontManager.Add(Font.Name.LeftArrow, SpriteBatch.Name.Texts, "arrow keys: move ship", Character.Name.Consolas36pt, 250, 610);
            FontManager.Add(Font.Name.C, SpriteBatch.Name.Texts, "c: toggles collision boxes", Character.Name.Consolas36pt, 200, 570);
            FontManager.Add(Font.Name.Q, SpriteBatch.Name.Texts, "q: quit", Character.Name.Consolas36pt, 380, 530);
            FontManager.Add(Font.Name.OctoPoints, SpriteBatch.Name.Texts, "octopus: 10 points", Character.Name.Consolas36pt, 280, 410);
            FontManager.Add(Font.Name.CrabPoints, SpriteBatch.Name.Texts, "crab: 20 points", Character.Name.Consolas36pt, 310, 370);
            FontManager.Add(Font.Name.SquidPoints, SpriteBatch.Name.Texts, "squid: 30 points", Character.Name.Consolas36pt, 305, 330);
            FontManager.Add(Font.Name.UFOPoints, SpriteBatch.Name.Texts, "UFO: 50, 100 or 150", Character.Name.Consolas36pt, 275, 290);

            // grid movement
            gm = new GridMover();
            TimerManager.Add(TimerEvent.Name.GridMovement, gm, Values.startingGridMovementInterval);

            //// UFO
            //ufospawn = new UFOSpawner();
            //double random = Values.getRandom(20.0, 40.0);
            //float f = (float)random;
            //Debug.WriteLine("Initial UFO Event value: " + f);
            //TimerManager.Add(TimerEvent.Name.UFOSpawn, ufospawn, f);

            // BOMBS

            BombDropper bd = new BombDropper();
            double random2 = Values.getRandom(1.0, 3.0);
            float f2 = (float)random2;
            Debug.WriteLine("Initial Bomb Drop value: " + f2);
            TimerManager.Add(TimerEvent.Name.BombDrop, bd, f2);

            // not letting user move ship around during the start screen, lol
            Ship shippy = ShipManager.GetShip();
            shippy.SetState(ShipManager.State.End);
        }

        //-----------------------------------------------------------------------------
        // Game::Update()
        //      Called once per frame, update data, tranformations, etc
        //      Use this function to control process order
        //      Input, AI, Physics, Animation, and Graphics
        //-----------------------------------------------------------------------------

        public override void Update()
        {
            if (Values.onStartScreen == true)
            {
                InputManager.Update();
            }
            else
            {
                eng.Update();
                InputManager.Update();
                TimerManager.Update(this.GetTime());
                CollisionPairManager.Process();
                GameObjectManager.Update();
                DelayObjectManager.Process();                
            }
        }

        //-----------------------------------------------------------------------------
        // Game::Draw()
        //		This function is called once per frame
        //	    Use this for draw graphics to the screen.
        //      Only do rendering here
        //-----------------------------------------------------------------------------
        public override void Draw()
        {
            SpriteBatchManager.Draw();
        }

        //-----------------------------------------------------------------------------
        // Game::UnLoadContent()
        //       unload content (resources loaded above)
        //       unload all content that was loaded before the Engine Loop started
        //-----------------------------------------------------------------------------
        public override void UnLoadContent()
        {
            TimerManager.Destroy();
            SpriteBatchManager.Destroy();
            GameSpriteManager.Destroy();
            BoxSpriteManager.Destroy();
            ImageManager.Destroy();
            TextureManager.Destroy();
            DeathManager.Destroy();
            ProxySpriteManager.Destroy();
            GameObjectManager.Destroy();
            CharacterManager.Destroy();
            FontManager.Destroy();
        }
    }
}