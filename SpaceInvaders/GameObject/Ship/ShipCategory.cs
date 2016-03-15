using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract class ShipCategory : GameObject
    {
        protected ShipCategory.Type type;

        public enum Type
        {
            Ship,
            ShipRoot,
            FakeShip,
            Splat,
            Uninitialized
        }

        protected ShipCategory(GameObject.Name gameNameArg, GameSprite.Name spriteNameArg, int indexArg, ShipCategory.Type shipArg)
            : base(gameNameArg, spriteNameArg, indexArg)
        {
            this.type = shipArg;
        }

        ~ShipCategory()
        {

        }
    }
}
