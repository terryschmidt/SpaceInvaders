using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class NullGameObject : GameObject
    {
        public NullGameObject()
            : base(GameObject.Name.Root, GameSprite.Name.NullObject, 0)
        {

        }

        ~NullGameObject()
        {

        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitNullGameObject(this);
        }

        public override void Update()
        {
            
        }
    }
}
