using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract class UFOCategory : GameObject
    {
        // data:
        protected UFOCategory.Type type;

        protected UFOCategory(GameObject.Name gameNameArg, GameSprite.Name spriteNameArg, int indexArg, UFOCategory.Type ufoTypeArg)
            : base(gameNameArg, spriteNameArg, indexArg)
        {
            type = ufoTypeArg;   
        }

        public enum Type
        {
            UFO,
            UFORoot,
            Uninitialized
        }
    }
}
