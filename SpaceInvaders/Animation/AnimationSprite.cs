using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class AnimationSprite : Command
    {
        // data:
        private GameSprite sprite;
        private SLink currentImage;
        private SLink firstImage;

        public AnimationSprite(GameSprite.Name spriteName)
        {
            this.sprite = GameSpriteManager.Find(spriteName);
            Debug.Assert(this.sprite != null);
            this.currentImage = null;
            this.firstImage = null;
        }

        public void Attach(Image.Name iName)
        {
            Image img = ImageManager.Find(iName);
            Debug.Assert(img != null);
            ImageHolder holder = new ImageHolder(img);
            this.addNode(holder, ref this.firstImage);
            this.currentImage = holder;
        }

        public override void execute(float deltaTime)
        {
            ImageHolder holder = (ImageHolder)this.currentImage.next;

            if (holder == null)
            {
                holder = (ImageHolder)firstImage;
            }

            this.currentImage = holder;
            this.sprite.SwapImage(holder.image);

            TimerManager.Add(TimerEvent.Name.SpriteAnimation, this, deltaTime);
        }

        private void addNode(SLink node, ref SLink head)
        {
            Debug.Assert(node != null);

            if (head == null)
            {
                head = node;
                node.next = null;
            }
            else
            {
                node.next = head;
                head = node;
            }
        }
    }
}
