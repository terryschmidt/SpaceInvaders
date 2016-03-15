using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class CollisionRectangle : Azul.Rect
    {
        public CollisionRectangle(float x, float y, float width, float height)
            : base(x, y, width, height)
        {

        }

        public CollisionRectangle(Azul.Rect rectArg)
            : base(rectArg)
        {

        }

        public CollisionRectangle(CollisionRectangle rectArg)
            : base(rectArg)
        {

        }

        public CollisionRectangle()
            : base()
        {

        }

        public static bool Intersect(CollisionRectangle rectAArg, CollisionRectangle rectBArg)
        {
            bool status = false;

            float A_minx = rectAArg.x - rectAArg.width / 2;
            float A_maxx = rectAArg.x + rectAArg.width / 2;
            float A_miny = rectAArg.y - rectAArg.height / 2;
            float A_maxy = rectAArg.y + rectAArg.height / 2;

            float B_minx = rectBArg.x - rectBArg.width / 2;
            float B_maxx = rectBArg.x + rectBArg.width / 2;
            float B_miny = rectBArg.y - rectBArg.height / 2;
            float B_maxy = rectBArg.y + rectBArg.height / 2;

            if ((B_maxx < A_minx) || (B_minx > A_maxx) || (B_maxy < A_miny) || (B_miny > A_maxy))
            {
                status = false;
            }
            else
            {
                status = true;
            }

            return status;
        }

        public void Union(CollisionRectangle rectArg)
        {
            float minX;
            float minY;
            float maxX;
            float maxY;

            if ((this.x - this.width / 2) < (rectArg.x - rectArg.width / 2))
            {
                minX = (this.x - this.width / 2);
            }
            else
            {
                minX = (rectArg.x - rectArg.width / 2);
            }

            if ((this.x + this.width / 2) > (rectArg.x + rectArg.width / 2))
            {
                maxX = (this.x + this.width / 2);
            }
            else
            {
                maxX = (rectArg.x + rectArg.width / 2);
            }

            if ((this.y + this.height / 2) > (rectArg.y + rectArg.height / 2))
            {
                maxY = (this.y + this.height / 2);
            }
            else
            {
                maxY = (rectArg.y + rectArg.height / 2);
            }

            if ((this.y - this.height / 2) < (rectArg.y - rectArg.height / 2))
            {
                minY = (this.y - this.height / 2);
            }
            else
            {
                minY = (rectArg.y - rectArg.height / 2);
            }

            this.width = (maxX - minX);
            this.height = (maxY - minY);
            this.x = minX + this.width / 2;
            this.y = minY + this.height / 2;
        }
    }
}
