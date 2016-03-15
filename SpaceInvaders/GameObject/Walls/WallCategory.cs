using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract class WallCategory : GameObject
    {
        // data:
        protected WallCategory.Type type;

        public enum Type
        {
            WallRoot,
            Right,
            Left,
            Bottom,
            Top,
            Uninitialized
        }

        protected WallCategory(GameObject.Name gameNameArg, GameSprite.Name spriteNameArg, int indexArg, WallCategory.Type wallTypeArg)
            : base(gameNameArg, spriteNameArg, indexArg)
        {
            this.type = wallTypeArg;
        }

        ~WallCategory()
        {

        }

        public WallCategory.Type GetCategory()
        {
            return this.type;
        }
    }
}
