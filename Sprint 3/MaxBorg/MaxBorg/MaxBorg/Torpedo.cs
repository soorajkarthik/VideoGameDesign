using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaxBorg
{
    class Torpedo
    {
        public Rectangle frame;
        public bool hasExploded;
        int direction;
        int power;
        int propulsion;
        int distanceLeft;

        static Point[] torpedoLaunchLocations = new Point[]
        {
            new Point(402, 110),
            new Point(530, 240),
            new Point(402, 370),
            new Point(270, 240)
        };

        public Torpedo()
        {
            direction = 0;
            power = 0;
            propulsion = 0;
            hasExploded = false;
            frame = new Rectangle();
        }

        public Torpedo(int dir, int pow, int prop)
        {
            direction = dir;
            power = pow;
            propulsion = prop;
            distanceLeft = prop * 30;
            hasExploded = false;

            frame = new Rectangle();
            frame.X = torpedoLaunchLocations[direction].X;
            frame.Y = torpedoLaunchLocations[direction].Y;
            frame.Inflate(10 + power * 2, 10 + power * 2);
        }

        public void Move()
        {
            if (distanceLeft == 0)
            {
                frame.Width = 0;
                hasExploded = true;
            }

            else
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

            }
        }

    }
}
