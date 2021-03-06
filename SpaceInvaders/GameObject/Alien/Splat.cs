﻿using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class Splat : AlienCategory
    {
        public Splat(GameObject.Name gameName, GameSprite.Name spriteName, int indexArg, float positionX, float positionY)
            : base(gameName, spriteName, indexArg, AlienCategory.Type.Splat)
        {
            this.x = positionX;
            this.y = positionY;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Accept(CollisionVisitor other)
        {
            
        }
    }
}
