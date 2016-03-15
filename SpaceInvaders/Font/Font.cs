using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Font : MLink
    {
        // data:
        public Name name;
        public FontSprite fontSprite;

        public enum Name
        {
            TopLine,
            Score1,
            Score2,
            HighScore,
            Spacebar,
            LeftArrow,
            RightArrow,
            OctoPoints,
            CrabPoints,
            SquidPoints,
            UFOPoints,
            C,
            Q,
            SpaceInvaders,
            CrabWorth,
            SquidWorth,
            OctoWorth,
            UFOWorth,
            RestartMessage,
            Lives1,
            GameOver,
            Lives2,
            Credits,
            NullObject,
            Uninitialized
        };

        public Font()
            : base()
        {
            this.name = Name.Uninitialized;
            this.fontSprite = new FontSprite();
        }

        ~Font()
        {
            this.name = Name.Uninitialized;
            this.fontSprite = null;
        }

        public void changeMessageTo(String newMessage)
        {
            Debug.Assert(newMessage != null);
            Debug.Assert(this.fontSprite != null);
            this.fontSprite.UpdateMessage(newMessage);
        }

        public void Set(Font.Name fontNameArg, String messageArg, Character.Name charNameArg, float xStart, float yStart)
        {
            Debug.Assert(messageArg != null);
            this.name = fontNameArg;
            this.fontSprite.Set(fontNameArg, messageArg, charNameArg, xStart, yStart);
        }

        public void Dump()
        {

        }
    }
}
