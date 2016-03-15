using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract class MissileCategory : GameObject
    {
        // data:

        protected MissileCategory.Type type;

        public enum Type
        {
            Missile,
            MissileRoot,
            Uninitialized
        }

        protected MissileCategory(GameObject.Name gameNameArg, GameSprite.Name spriteNameArg, int indexArg, MissileCategory.Type missileArg)
            : base(gameNameArg, spriteNameArg, indexArg)
        {
            this.type = missileArg;
        }

        static public GameObject GetMissile(GameObject gameObjA, GameObject gameObjB)
        {
            GameObject missile;
            if (gameObjA is MissileCategory)
            {
                missile = (GameObject)gameObjA;
            }
            else
            {
                missile = (GameObject)gameObjB;
            }

            Debug.Assert(missile is MissileCategory);

            return missile;
        }

        ~MissileCategory()
        {

        }
    }
}
