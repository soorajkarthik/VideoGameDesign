using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class Invader
    {
        public int xPos;
        public int yPos;
        public int velocity;
        public Rectangle rect;
        public Texture2D tex;

        public Invader()
        {

        }

        public Invader(int x, int y, Texture2D t)
        {
            xPos = x;
            yPos = y;
            velocity = 6;
            rect = new Rectangle(50 * xPos, 50 * yPos, 50, 38);
            tex = t;
        }

        public void Update(int xCount, int yCount)
        {
           
            rect.X += velocity;

            if (rect.X + (xCount - xPos) * 50 >= 1000 || rect.X - xPos * 50 <= 0)
            {
                velocity *= -1;
                rect.Y += 50;

                if (rect.Y >= 800 - (yCount - yPos) * 50)
                    rect.Y = 800 - (yCount - yPos) * 50;

            }

        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(tex, rect, Color.White);
        }
    }
}
