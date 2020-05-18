using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IIWGTI
{
    class SubjectImage
    {
        public string title;
        public Texture2D texture;
        public Rectangle rect;

        //Default constructor
        public SubjectImage()
        {
            
        }

        public SubjectImage(string ti, Rectangle r, Texture2D tex)
        {
            title = ti;
            rect = r;
            texture = tex;
        }

        public void Draw(SpriteBatch sb, Rectangle destination, SpriteFont font)
        {
            
            sb.Draw(texture, destination, rect, Color.White);

            int x = destination.Center.X - (title.Length)/2 * 10;
            sb.DrawString(font, title, new Vector2(x, destination.Bottom + 20), Color.Black);
           
        }
    }
}
