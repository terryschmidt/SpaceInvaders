using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ImageHolder : SLink
    {
        // data:
        public Image image;

        public ImageHolder(Image img)
            : base()
        {
            image = img;
        }
    }
}
