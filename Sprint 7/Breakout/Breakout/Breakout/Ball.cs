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
    class Ball
    {
        public Rectangle rect;
        public Texture2D tex;

        public double x;
        public double y;
        public Vector2 vel;

        public float spinRate;
        public float angle;

        public Ball(Rectangle r, Texture2D t)
        {
            rect = r;
            tex = t;

            x = r.X;
            y = r.Y;

            Random rand = new Random();
            while (vel.X == 0 || vel.Y == 0)
                vel = new Vector2((float)rand.NextDouble() * 10 - 5, -5);

            spinRate = (float)((rand.NextDouble() * 6 - 3) * 2 * Math.PI);
            angle = 0;
        }

        public void Update(Rectangle paddle, KeyboardState kb)
        {

            Rectangle paddleLeft = new Rectangle(paddle.Left, paddle.Y + 3, 3, paddle.Height - 6);
            Rectangle paddleRight = new Rectangle(paddle.Right - 3, paddle.Y + 3, 3, paddle.Height - 6);
            Rectangle paddleTop = new Rectangle(paddle.Left + 3, paddle.Top, paddle.Width - 6, 3);

            x += vel.X;
            y += vel.Y;


            if (x <= 0)
            {
                x = 0;
                vel.X *= -1;
            }

            else if(x >= 680)
            {
                x = 680;
                vel.X *= -1;
            }

            if(y <= 0)
            {
                y = 0;
                vel.Y *= -1;
            }

            if (paddle.Intersects(rect))
            {

                if(rect.Intersects(paddleTop))
                                {
                    vel.Y *= -1;
                    y = paddle.Y - 25;

                    vel.X -= spinRate / 60;
                    vel.Y += spinRate / 60;
                    spinRate *= 0.95f;

                }
                
                else if (rect.Intersects(paddleLeft))
                {
                    vel.X *= -1;
                    x = paddle.Left - 21;

                    vel.X += spinRate * 0.1f;
                    vel.Y -= spinRate * 0.1f;
                    spinRate *= 0.9f;
                }

                else if (rect.Intersects(paddleRight))
                {
                    vel.X *= -1;
                    x = paddle.Right + 1;

                    vel.X += spinRate * 0.1f;
                    vel.Y -= spinRate * 0.1f;
                    spinRate *= 0.9f;
                }

                if (paddle.Left != 0 && paddle.Right != 700)
                {
                    if (kb.IsKeyDown(Keys.Left))
                        spinRate += 4;
                    else if (kb.IsKeyDown(Keys.Right))
                        spinRate -= 4;
                }
            }

            rect.X = (int)x;
            rect.Y = (int)y;

            angle += spinRate / 30;
        }
        
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(tex, new Vector2(rect.Center.X, rect.Center.Y), null, Color.White, angle, new Vector2(tex.Width / 2, tex.Height / 2), 1, SpriteEffects.None, 0);
        }
    }
}
