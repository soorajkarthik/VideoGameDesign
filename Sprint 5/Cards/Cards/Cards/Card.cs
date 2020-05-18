using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cards
{
    class Card
    {
        public Texture2D tex;
        public Rectangle rect;

        public Card()
        {

        }

        public Card (Rectangle r)
        {
            rect = r;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Begin();
            sb.Draw(tex, rect, Color.White);
            sb.End();
        }
    }
}
