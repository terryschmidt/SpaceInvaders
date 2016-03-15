using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class Bomb : BombCategory
    {
        // data:
        public float delta;
        private FallStrategy strategy;

        public Bomb(GameObject.Name nameArg, GameSprite.Name spriteName, FallStrategy strategyArg, int indexArg, float positionX, float positionY)
            : base(nameArg, spriteName, indexArg, BombCategory.Type.Bomb)
        {
            this.x = positionX;
            this.y = positionY;
            this.delta = 7.3f;
            Debug.Assert(strategyArg != null);
            this.strategy = strategyArg;
            this.strategy.Reset(this.y);
            this.colObj.colSprite.SetLineColor(1, 1, 0);
        }

        public void Reset()
        {
            this.y = 800.0f;
            this.strategy.Reset(this.y);
        }

        public override void Remove()
        {
            this.colObj.colRect.Set(0, 0, 0, 0);
            base.Update();
            GameObject parent = (GameObject)this.pParent;
            parent.Update();
            base.Remove();
        }

        public override void Update()
        {
            base.Update();
            this.y -= delta;
            this.strategy.Fall(this);
        }

        public float GetBoundingBoxHeight()
        {
            return this.colObj.colRect.height;
        }

        private void StrategyStraightFall()
        {

        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitBomb(this);
        }

        public void SetPos(float xArg, float yArg)
        {
            this.x = xArg;
            this.y = yArg;
        }

        public void MultiplyScale(float sxArg, float syArg)
        {
            Debug.Assert(this.proxySprite != null);
            this.proxySprite.sx *= sxArg;
            this.proxySprite.sy *= syArg;
        }
    }
}
