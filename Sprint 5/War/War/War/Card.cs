using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War
{
    class Card
    {

        public string suit;
        public int value;
        public Texture2D tex;

        public Card()
        {

        }

        public Card (string s, int v)
        {
            suit = s;
            value = v;
        }

        public void Draw(SpriteBatch sb, Rectangle rect)
        {
            sb.Draw(tex, rect, Color.White);

        }

    }
}
