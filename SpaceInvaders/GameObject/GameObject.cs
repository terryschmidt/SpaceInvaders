using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract class GameObject : CollisionVisitor
    {
        // data:
        public GameObject.Name name;
        public int index;
        public bool markForDeath;

        public float x;
        public float y;
        public ProxySprite proxySprite;
        public CollisionObject colObj;

        public enum Name
        {
            RedBird,
            ShipSplat,
            YellowBird,
            GreenBird,
            UFOSplat,
            UFOBomb,
            MissileBombSplat,
            SpaceInvaders,
            WhiteBird,
            StaticShip,
            Bomb,
            FakeShip,
            BombRoot,
            Crab,
            MissileBombSplat2,
            Crab2,
            Squid,
            Squid2,
            Octopus,
            BombSplat,
            Octopus2,
            Shield,
            ShieldRoot,
            ShieldGrid,
            ShieldGrid2,
            ShieldGrid3,
            ShieldGrid4,
            ShieldColumn,
            ShieldBrick,
            Ship,
            Splat,
            Splat2,
            Splat3,
            Splat4,
            Splat5,
            ShipRoot,
            Column,
            UFO,
            UFORoot,
            MissileRoot,
            Missile,
            MissileWallSplat,
            WallRoot,
            WallRight,
            WallLeft,
            WallTop,
            WallBottom,
            Root,
            Grid,
            NullObject,
            Uninitialized
        }

        public override Enum getName()
        {
            return this.name;
        }

        public override int getIndex()
        {
            return this.index;
        }

        protected void baseUpdateBoundingBox()
        {
            // Go to first child
            PCSNode pNode = (PCSNode)this;

            pNode = pNode.pChild;

            // Set ColTotal to first child
            GameObject pGameObj = (GameObject)pNode;

            //if (pGameObj is Crab || pGameObj is Octopus || pGameObj is Squid) 
            //{
            //    return;
            //}

            CollisionRectangle ColTotal = this.colObj.colRect;
            Debug.Assert(ColTotal != null);

            if (pGameObj != null)
            {
            ColTotal.Set(pGameObj.colObj.colRect);
            }
            else
            {

                BoxSprite bs = this.colObj.colSprite;
                SpriteBatchNode sbn = bs.GetSpriteBatchNode();
                SpriteBatchManager.Remove(sbn);

                //pGameObj.Remove();

                //ColTotal.width = 0;
                //ColTotal.height = 0;
            }

            // loop through sliblings
            while (pNode != null)
            {
                if (pNode is Column)
                {
                    if (pNode.pChild == null)
                    {
                        Debug.WriteLine("Removing Column");
                        GameObject node = (GameObject)pNode;
                        node.x = -5000;
                        node.y = -5000;
                        Values.columnCount--;
                        GameObjectManager.Remove(node);

                        if (Values.columnCount == 0)
                        {
                            Debug.WriteLine("Removing Grid");
                            GameObject grid = GameObjectManager.Find(GameObject.Name.Grid);
                            grid.Remove();
                            Values.columnCount = 11;
                        }
                    }
                }

                

                //if (pNode is Grid)
                //{
                //    if (pNode.pChild == null)
                //    {
                //        BoxSprite bs = this.colObj.colSprite;
                //        SpriteBatchNode sbn = bs.GetSpriteBatchNode();
                //        SpriteBatchManager.Remove(sbn);
                //        GameObjectManager.Remove((GameObject)pNode);
                //    }
                //}
                pGameObj = (GameObject)pNode;
                ColTotal.Union(pGameObj.colObj.colRect);

                // go to next sibling
                pNode = pNode.pSibling;
            }

            this.x = this.colObj.colRect.x;
            this.y = this.colObj.colRect.y;
        }

        /*protected GameObject(GameObject.Name nameArg)
        {
            this.name = nameArg;
            this.x = 0.0f;
            this.y = 0.0f;
            this.proxySprite = null;
        }*/

        protected GameObject(GameObject.Name nameArg, GameSprite.Name spriteNameArg, int indexArg)
        {
            this.name = nameArg;
            this.index = indexArg;
            this.x = 0.0f;
            this.y = 0.0f;
            this.markForDeath = false;

            this.proxySprite = ProxySpriteManager.Add(spriteNameArg);
            Debug.Assert(this.proxySprite != null);

            this.colObj = new CollisionObject(this.proxySprite);
            Debug.Assert(this.colObj != null);
        }

        ~GameObject()
        {
            this.name = GameObject.Name.Uninitialized;
            this.proxySprite = null;
        }

        public void SetCollisionColor(float red, float green, float blue)
        {
            Debug.Assert(this.colObj != null);
            Debug.Assert(this.colObj.colSprite != null);
            this.colObj.colSprite.SetLineColor(red, green, blue);
        }

        public void ActivateGameSprite(SpriteBatch pSpriteBatch)
        {
            Debug.Assert(pSpriteBatch != null);
            pSpriteBatch.Attach(this.proxySprite);
        }
        public void ActivateCollisionSprite(SpriteBatch pSpriteBatch)
        {
            Debug.Assert(pSpriteBatch != null);
            Debug.Assert(this.colObj != null);
            pSpriteBatch.Attach(this.colObj.colSprite);
        }

        public virtual void Update()
        {
            Debug.Assert(this.proxySprite != null);
            this.proxySprite.x = this.x;
            this.proxySprite.y = this.y;

            Debug.Assert(this.colObj != null);
            this.colObj.PushPos(this.x, this.y);
        }

        public virtual void Remove()
        {
            //if (this is ShieldRoot)
            //{
            //    SpriteBatchNode bn = this.colObj.col
            //}




            // Remove from SpriteBatch
            Debug.Assert(this.proxySprite != null);

            SpriteBatchNode pSpriteBatchNode = this.proxySprite.GetSpriteBatchNode();
            Debug.Assert(pSpriteBatchNode != null);
            SpriteBatchManager.Remove(pSpriteBatchNode);

            // remove collision sprite from sprite batch
            Debug.Assert(this.colObj != null);
            Debug.Assert(this.colObj.colSprite != null);

            pSpriteBatchNode = this.colObj.colSprite.GetSpriteBatchNode();
            Debug.Assert(pSpriteBatchNode != null);
            SpriteBatchManager.Remove(pSpriteBatchNode);

            
            //Remove from GameObjectManager
            GameObjectManager.Remove(this);
            GhostManager.Add(this);

            //GhostMan.DumpStats();
        }

        public void Dump()
        {
            // Data:
            Debug.WriteLine("\t\t\t       name: {0} ({1})", this.name, this.GetHashCode());

            if (this.proxySprite != null)
            {
                Debug.WriteLine("\t\t   pProxySprite: {0}", this.proxySprite.name);
                Debug.WriteLine("\t\t    pRealSprite: {0}", this.proxySprite.realSprite.name);
            }
            else
            {
                Debug.WriteLine("\t\t   pProxySprite: null");
                Debug.WriteLine("\t\t    pRealSprite: null");
            }
            Debug.WriteLine("\t\t\t      (x,y): {0}, {1}", this.x, this.y);
        }
    }
}
