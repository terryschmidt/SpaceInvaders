using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract class ShieldCategory : GameObject
    {
        // data:
        protected ShieldCategory.Type type;

        public enum Type
        {
            Root,
            Grid,
            Grid2,
            Grid3,
            Grid4,
            Column,
            Brick,
            LeftTop0,
            LeftTop1,
            LeftBottom,
            RightTop0,
            RightTop1,
            RightBottom,
            Uninitialized
        }

        protected ShieldCategory(GameObject.Name gameNameArg, GameSprite.Name spriteNameArg, int indexArg, ShieldCategory.Type shieldTypeArg)
            : base(gameNameArg, spriteNameArg, indexArg)
        {
            this.type = shieldTypeArg;
        }

        ~ShieldCategory()
        {

        }

        public ShieldCategory.Type GetCategoryType()
        {
            return this.type;
        }
    }
}
