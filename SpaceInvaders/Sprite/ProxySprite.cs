using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ProxySprite : SpriteBase
    {
        // data:
        public ProxySprite.Name name;
        public float x;
        public float y;
        public float sx;
        public float sy;
        public GameSprite realSprite;

        public enum Name
        {
            Proxy,
            NullObject,
            Uninitialized
        }

        public ProxySprite()
        {
            this.name = ProxySprite.Name.Proxy;
            this.x = 0.0f;
            this.y = 0.0f;
            this.sx = 1.0f;
            this.sy = 1.0f;
            this.realSprite = null;
        }

        ~ProxySprite()
        {
            this.realSprite = null;
            this.name = ProxySprite.Name.Uninitialized;
        }

        public ProxySprite(GameSprite.Name nameArg)
        {
            this.name = ProxySprite.Name.Proxy;
            this.x = 0.0f;
            this.y = 0.0f;
            this.sx = 1.0f;
            this.sy = 1.0f;
            this.realSprite = GameSpriteManager.Find(nameArg);
            Debug.Assert(this.realSprite != null);
        }

        public void Set(GameSprite.Name nameArg)
        {
            this.name = ProxySprite.Name.Proxy;
            this.x = 0.0f;
            this.y = 0.0f;
            this.sx = 1.0f;
            this.sy = 1.0f;
            this.realSprite = GameSpriteManager.Find(nameArg);
            Debug.Assert(this.realSprite != null);
        }

        public override void Update()
        {
            //this.pushToReal();
            //this.realSprite.Update();

            // Do nothing Render will be responsible for this
        }

        private void pushToReal()
        {
            this.realSprite.x = this.x;
            this.realSprite.y = this.y;
            this.realSprite.sx = this.sx;
            this.realSprite.sy = this.sy;
        }

        public override void Render()
        {
            this.pushToReal();
            this.realSprite.Update();
            this.realSprite.Render();
        }

        public override Enum GetName()
        {
            return this.name;
        }

        public void Dump()
        {

        }
    }
}
