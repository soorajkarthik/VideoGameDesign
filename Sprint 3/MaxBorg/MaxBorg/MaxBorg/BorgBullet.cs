using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaxBorg
{
    class BorgBullet
    {
        public Rectangle frame;
        int direction;

        public BorgBullet(Rectangle f, int d)
        {
            frame = f;
            direction = d;
        }

        public void Move()
        {
            switch (direction)
            {
                case 0:
                    frame.Y += 2;
                    break;

                case 1:
                    frame.X -= 2;
                    break;

                case 2:
                    frame.Y -= 2;
                    break;

                case 3:
                    frame.X += 2;
                    break;
            }
        }
    }
}
