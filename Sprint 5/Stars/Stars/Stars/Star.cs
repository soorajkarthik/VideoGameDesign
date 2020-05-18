using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stars
{
    class Star
    {
        public Vector2 velocity;
        public Color color;
        public Rectangle rect;
        public Texture2D tex;

        public bool isOffScreen
        {
            get
            {
                return rect.X < 0 || rect.X > 800 || rect.Y < 0 || rect.Y > 480;
            }
        }

        public Star()
        {

        }

        public Star(Random rand, Texture2D t)
        {
            velocity = new Vector2(rand.Next(-5, 5), rand.Next(-5, 5));
            color = new Color(rand.Next(255), rand.Next(255), rand.Next(255));
            rect = new Rectangle(rand.Next(995), rand.Next(995), 5, 5);
            tex = t;
        }

        public void Update()
        {
            rect.X += (int)velocity.X;
            rect.Y += (int)velocity.Y;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(tex, rect, color);
        }
    }
}
