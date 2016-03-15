using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract class BombCategory : GameObject
    {
        // data:
        protected BombCategory.Type type;

        public enum Type
        {
            Bomb,
            BombRoot,
            Uninitialized
        }

        protected BombCategory(GameObject.Name gameNameArg, GameSprite.Name spriteNameArg, int indexArg, BombCategory.Type bombTypeArg)
            : base(gameNameArg, spriteNameArg, indexArg)
        {
            this.type = bombTypeArg;
        }

        ~BombCategory()
        {

        }

        public static GameObject GetBomb(GameObject objA, GameObject objB)
        {
            GameObject bomb;
            if (objA is BombCategory)
            {
                bomb = (GameObject)objA;
            }
            else
            {
                bomb = (GameObject)objB;
            }

            Debug.Assert(bomb is MissileCategory);

            return bomb;
        }
    }
}
