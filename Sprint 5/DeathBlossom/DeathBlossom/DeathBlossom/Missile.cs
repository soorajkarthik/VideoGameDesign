using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeathBlossom
{
    class Missile
    {
        public Vector2 pos;
        public Vector2 vel;
        public Rectangle frame;
        public float angle;
        
        public bool isOffScreen
        {
            get
            {
                return frame.Left < 0 || frame.Right > 1600 || frame.Top < 0 || frame.Bottom > 950;
            }
        }

        public Missile(float a)
        {
            angle = a;
            pos = new Vector2(535, 325);
            vel = new Vector2(10 * (float)Math.Cos(angle), 10 * (float)Math.Sin(angle));
            frame = new Rectangle(535, 325, 31, 64);
        }

        public void Update()
        {
            pos.X += vel.X;
            pos.Y += vel.Y;

            frame.X = (int)pos.X;
            frame.Y = (int)pos.Y;
        }

        public void Draw(SpriteBatch sb, Texture2D tex)
        {
            sb.Draw(tex, frame, null, Color.White, angle + MathHelper.PiOver2, new Vector2(frame.Width / 2, frame.Height / 2), SpriteEffects.None, 0);
        }
    }
}
