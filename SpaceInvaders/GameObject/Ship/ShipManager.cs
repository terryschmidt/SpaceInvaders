using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShipManager
    {
        // data:
        private static ShipManager instance;

        private ShipStateReady ready;
        private ShipStateMissileFlying flying;
        private ShipStateEnd end;

        private Ship ship;
        private Missile missile;

        public enum State
        {
            Ready,
            MissileFlying,
            End
        }

        private ShipManager()
        {
            this.ready = new ShipStateReady();
            this.flying = new ShipStateMissileFlying();
            this.end = new ShipStateEnd();

            this.ship = null;
            this.missile = null;
        }

        public static void Create()
        {
            Debug.Assert(instance == null);

            if (instance == null)
            {
                instance = new ShipManager();
            }

            Debug.Assert(instance != null);

            instance.ship = ActivateShip();
            instance.ship.SetState(ShipManager.State.Ready);
        }

        private static Ship ActivateShip()
        {
            ShipManager inst = ShipManager.getInstance();
            Debug.Assert(inst != null);

            PCSTree tree = GameObjectManager.GetRootTree();
            Debug.Assert(tree != null);

            Ship s = new Ship(GameObject.Name.Ship, GameSprite.Name.Ship, 0, 448, 130);
            inst.ship = s;
            //s.Update();

            // attach the sprite to the correct batch
            SpriteBatch pSB_Aliens = SpriteBatchManager.Find(SpriteBatch.Name.Aliens);
            pSB_Aliens.Attach(s.proxySprite);

            // attach the missile to the missile root
            GameObject sRoot = GameObjectManager.Find(GameObject.Name.ShipRoot);
            Debug.Assert(sRoot != null);

            tree.Insert(inst.ship, sRoot);

            return inst.ship;
        }

        public static Ship GetShip()
        {
            ShipManager inst = ShipManager.getInstance();
            Debug.Assert(inst != null);
            Debug.Assert(inst.ship != null);
            return inst.ship;
        }

        public static Missile GetMissile()
        {
            ShipManager inst = ShipManager.getInstance();
            Debug.Assert(inst != null);
            Debug.Assert(inst.missile != null);
            return inst.missile;
        }

        public static Missile ActivateMissile()
        {
            ShipManager inst = ShipManager.getInstance();
            Debug.Assert(inst != null);

            PCSTree t = GameObjectManager.GetRootTree();
            Debug.Assert(t != null);

            Missile m = new Missile(GameObject.Name.Missile, GameSprite.Name.Missile, 0, 400, 300);
            inst.missile = m;

            // attach to spritebatches
            SpriteBatch pSB_Aliens = SpriteBatchManager.Find(SpriteBatch.Name.Aliens);
            SpriteBatch pSB_Boxes = SpriteBatchManager.Find(SpriteBatch.Name.Boxes);

            m.ActivateCollisionSprite(pSB_Boxes);
            m.ActivateGameSprite(pSB_Aliens);

            // attach the missile to the missile root
            GameObject mRoot = GameObjectManager.Find(GameObject.Name.MissileRoot);
            Debug.Assert(mRoot != null);

            // add to game object tree
            t.Insert(inst.missile, mRoot);

            return inst.missile;
        }

        public static ShipState GetState(State state)
        {
            ShipManager inst = ShipManager.getInstance();
            Debug.Assert(inst != null);

            ShipState ss = null;

            switch (state)
            {
                case ShipManager.State.Ready:
                    ss = inst.ready;
                    break;
                case ShipManager.State.MissileFlying:
                    ss = inst.flying;
                    break;
                case ShipManager.State.End:
                    ss = inst.end;
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }

            return ss;
        }

        // private

        private static ShipManager getInstance()
        {
            Debug.Assert(instance != null);
            return instance;
        }
    }
}
