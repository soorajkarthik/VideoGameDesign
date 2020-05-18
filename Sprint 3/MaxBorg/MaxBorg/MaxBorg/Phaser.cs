using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaxBorg
{
    class Phaser
    {
        public Color color;
        public Rectangle frame;
        public bool isActive;
        int timer;

        public Phaser()
        {
            frame = new Rectangle();
            timer = 0;
        }

        public Phaser(int strength, int side)
        {
            switch (side)
            {
                case 0:
                    frame = new Rectangle(402, 0, 1, 110);
                    break;

                case 1:
                    frame = new Rectangle(530, 240, 270, 1);
                    break;

                case 2:
                    frame = new Rectangle(402, 370, 1, 330);
                    break;

                case 3:
                    frame = new Rectangle(0, 240, 270, 1);
                    break;

            }


            switch (strength)
            {
                case 0:
                    color = Color.Green;
                    if (side % 2 == 0)
                        frame.Inflate(5, 0);
                    else
                        frame.Inflate(0, 5);
                    break;

                case 1:
                    color = Color.Red;
                    if (side % 2 == 0)
                        frame.Inflate(10, 0);
                    else
                        frame.Inflate(0, 10);
                    break;

                case 2:
                    color = Color.CornflowerBlue;
                    if (side % 2 == 0)
                        frame.Inflate(15, 0);
                    else
                        frame.Inflate(0, 15);
                    break;

                case 3:
                    color = Color.Yellow;
                    if (side % 2 == 0)
                        frame.Inflate(20, 0);
                    else
                        frame.Inflate(0, 20);
                    break;
            }

            isActive = true;
            timer = 120;

        }

        public void Update()
        {
            timer--;
            if (timer == 0)
            {
                isActive = false;
                frame.X = 1000;
            }


        }
    }


}
