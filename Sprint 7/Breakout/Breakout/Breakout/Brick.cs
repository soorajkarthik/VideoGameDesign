using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Breakout
{
    class Brick
    {

        public Rectangle rect;
        public Color color;
        public Texture2D tex;
        public bool exists;
        public int health;

        public Rectangle left;
        public Rectangle right;
        public Rectangle top;
        public Rectangle bottom;
         
        public Brick(Rectangle r, string s, Texture2D t)
        {
            rect = r;
            tex = t;
            exists = true;

            left = new Rectangle(rect.Left, rect.Y + 1, 1, rect.Height - 2);
            right = new Rectangle(rect.Right - 1, rect.Y + 1, 1, rect.Height - 2);

            top = new Rectangle(rect.Left + 1, rect.Top, rect.Width - 2, 1);
            bottom = new Rectangle(rect.Left + 1, rect.Bottom - 1, rect.Width - 2, 1);

            switch (s)
            {
                case "b":
                    color = Color.Blue;
                    health = 1;
                    break;

                case "g":
                    color = Color.Green;
                    health = 2;
                    break;

                case "o":
                    color = Color.Orange;
                    health = 3;
                    break;

                case "r":
                    color = Color.Red;
                    health = 4;
                    break;

                case "y":
                    color = Color.Yellow;
                    health = 5;
                    break;

                default:
                    exists = false;
                    break;
            }
        }

        public void Draw(SpriteBatch sb)
        {
            if (exists)
                sb.Draw(tex, rect, color);
        }
        
    }
}
