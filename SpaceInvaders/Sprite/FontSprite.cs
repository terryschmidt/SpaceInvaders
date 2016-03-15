using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class FontSprite : SpriteBase
    {
        // data:
        public Font.Name name;
        private Azul.Sprite azulSprite;
        private Azul.Rect azulRect;
        private Azul.Color azulColor;
        private String message;
        public Character.Name charName;
        public float x;
        public float y;

        public override Enum GetName()
        {
            return this.name;
        }

        public FontSprite()
            : base()
        {
            this.azulSprite = new Azul.Sprite();
            this.azulRect = new Azul.Rect();
            this.azulColor = new Azul.Color(1.0f, 1.0f, 1.0f);
            this.message = null;
            this.charName = Character.Name.Uninitialized;
            this.x = 0.0f;
            this.y = 0.0f;
        }

        ~FontSprite()
        {
            this.azulSprite = null;
            this.azulRect = null;
            this.azulColor = null;
            this.message = null;
        }

        public void Set(Font.Name fontNameArg, String messageArg, Character.Name charNameArg, float xArg, float yArg)
        {
            Debug.Assert(messageArg != null);
            this.message = messageArg;
            this.x = xArg;
            this.y = yArg;
            this.name = fontNameArg;
            this.charName = charNameArg;
            Debug.Assert(this.azulColor != null);
            this.azulColor.Set(1.0f, 1.0f, 1.0f);
        }

        public void SetColor(float red, float green, float blue, float alpha = 1.0f)
        {
            Debug.Assert(this.azulColor != null);
            this.azulColor.Set(red, green, blue, alpha);
        }

        public void UpdateMessage(String messageArg)
        {
            Debug.Assert(messageArg != null);
            this.message = messageArg;
        }

        public override void Update()
        {
            Debug.Assert(this.azulSprite != null);
        }

        public override void Render()
        {
            Debug.Assert(this.azulSprite != null);
            Debug.Assert(this.azulColor != null);
            Debug.Assert(this.azulRect != null);
            Debug.Assert(this.message != null);
            //Debug.Assert(this.message.Length >= 1);

            float tempX = this.x;
            float tempY = this.y;
            float xEnd = this.x;

            int messageLength = message.Length;

            for (int i = 0; i < messageLength; i++)
            {
                int key = Convert.ToByte(message[i]);
                Character someChar = CharacterManager.Find(this.charName, key);
                Debug.Assert(someChar != null);

                tempX = xEnd + someChar.GetAzulSubRect().width / 2;
                this.azulRect.Set(tempX, tempY, someChar.GetAzulSubRect().width, someChar.GetAzulSubRect().height);

                this.azulSprite.Swap(someChar.GetAzulTexture(), someChar.GetAzulSubRect(), this.azulRect, this.azulColor);

                this.azulSprite.Update();
                this.azulSprite.Render();

                xEnd = someChar.GetAzulSubRect().width / 2 + tempX;
            }
        }

        public void Dump()
        {

        }
    }
}
