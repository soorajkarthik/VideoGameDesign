using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoreBorg
{
    class Borg
    {
        public Rectangle frame;
        public int timeVisible;
        public int shotTime;
        public int side;

        public Borg(int s)
        {
            Random rand = new Random();
            side = s;
            timeVisible = rand.Next(5, 10);

            switch (side)
            {
                case 0:
                    frame = new Rectangle(382, rand.Next(-66, 44), 40, 66);
                    break;

                case 1:
                    frame = new Rectangle(rand.Next(530, 800), 207, 40, 66);
                    break;

                case 2:
                    frame = new Rectangle(382, rand.Next(370, 400), 40, 66);
                    break;

                case 3:
                    frame = new Rectangle(rand.Next(-40, 230), 207, 40, 66);
                    break;
            }

            timeVisible = rand.Next(5, 10) * 60;
            shotTime = rand.Next(1, 4) * 60;

        }

        public void DecrementTime()
        {
            timeVisible--;
            shotTime--;
        }

        public Rectangle GetShotRectangle()
        {

            Rectangle ret = new Rectangle();

            switch (side)
            {
                case 0:
                    ret = new Rectangle(frame.Center.X - 2, frame.Bottom, 5, 5);
                    break;

                case 1:
                    ret = new Rectangle(frame.Left - 5, frame.Center.Y - 2, 5, 5);
                    break;

                case 2:
                    ret = new Rectangle(frame.Center.X - 2, frame.Top - 5, 5, 5);
                    break;

                case 3:
                    ret = new Rectangle(frame.Right, frame.Center.Y - 2, 5, 5);
                    break;
            }

            return ret;
        }
    }
}
