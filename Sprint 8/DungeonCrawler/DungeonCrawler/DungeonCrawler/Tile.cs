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

namespace DungeonCrawler
{
    class Tile
    {
        public Rectangle loc;
        public Texture2D img;

        public Tile(Rectangle r)
        {
            loc = r;
        }

        public void Draw (SpriteBatch sb, Rectangle camera)
        {
            sb.Draw(img, new Rectangle(loc.X - camera.X, loc.Y - camera.Y, loc.Width, loc.Height), Color.White);
        }
    }
}
