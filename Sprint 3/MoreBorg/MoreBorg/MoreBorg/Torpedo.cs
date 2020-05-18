using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoreBorg
{
    class Torpedo
    {
        public Rectangle frame;
        int direction;
        int power;
        int propulsion;
        int distanceLeft;

        static Point[] torpedoLaunchLocations = new Point[]
        {
            new Point(402, 110),
            new Point(530, 240),
            new Point(402, 370),
            new Point(270, 240),
            new Point(10000, 10000)
        };

        public Torpedo(int dir, int pow, int prop)
        {
            direction = dir;
            power = pow;
            propulsion = prop;
            distanceLeft = prop * 30;

            frame = new Rectangle();
            frame.X = torpedoLaunchLocations[direction].X;
            frame.Y = torpedoLaunchLocations[direction].Y;
            frame.Inflate(10 + power * 2, 10 + power * 2);
        }

        public void Move()
        {
            switch (direction)
            {
                case 0:
                    frame.Y -= 2;
                    break;

                case 1:
                    frame.X += 2;
                    break;

                case 2:
                    frame.Y += 2;
                    break;

                case 3:
                    frame.X -= 2;
                    break;
            }

            distanceLeft -= 2;

            if (distanceLeft == 2)
                frame.X = 10000;

        }

        public bool IsOffScreen()
        {
            return frame.X < -frame.Width || frame.X > 800 || frame.Y < -frame.Height || frame.Y > 480; 
        }

    }
}
