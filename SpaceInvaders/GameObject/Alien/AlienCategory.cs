using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract class AlienCategory : GameObject
    {
        public AlienCategory.Type type;

        public enum Type
        {
            Crab,
            Crab2,
            Squid,
            Squid2,
            Octopus,
            MissileBombSplat,
            Octopus2,
            UFOSplat,
            SpaceInvaders,
            Column,
            Splat,
            MissileWallSplat,
            Grid,
            Uninitialized
        }

        protected AlienCategory(GameObject.Name name, GameSprite.Name spriteName, int indexArg, AlienCategory.Type alienType)
            : base(name, spriteName, indexArg)
        {
            this.type = alienType;
        }

        ~AlienCategory()
        {

        }
    }
}
