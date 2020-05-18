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
namespace LifeCycle
{

    public class Trail
    {

        Texture2D text;
        public Rectangle rect;

        public Trail(Texture2D text, Rectangle rect)
        {
            this.text = text;
            this.rect = rect;
        }

        public void ExtendRect(int direction, int velocity)
        {
            if (direction % 180 == 0)   
            {
                rect.Width += velocity;
                if (direction == 180)
                    rect.X -= velocity;
            }
            else                        
            {
                rect.Height += velocity;
                if (direction == 270)
                    rect.Y -= velocity;
            }
        }
        
        public void Draw(SpriteBatch sb, GameTime gameTime)
        {
            sb.Draw(text, rect, Color.White);
        }

        
    }
}