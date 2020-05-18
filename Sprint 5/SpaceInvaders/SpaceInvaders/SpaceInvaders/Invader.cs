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
        public int position;
        public int velocity;
        public Rectangle rect;
        public Rectangle sourceRect;
        public Texture2D tex;

        public Invader()
        {

        }

        public Invader(int pos, Texture2D t)
        {
            position = pos;
            velocity = 3;
            rect = new Rectangle(50 * position, 0, 50, 38);
            sourceRect = new Rectangle(0, 0, 50, 38);
            tex = t;
        }

        public void Update(int tics)
        {
            if (tics % 10 == 0)
            {
                sourceRect.X += 50;
                sourceRect.X %= 100;
            }

            rect.X += velocity;      

            if(rect.X + (10 - position) * 50 >= 1000 || rect.X - position * 50 <= 0)
            {
                velocity *= -1;
                rect.Y += 50;

                if (rect.Y >= 950)
                    rect.Y = 950;

            }
                
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(tex, rect, sourceRect, Color.White);
        }
    }
}
