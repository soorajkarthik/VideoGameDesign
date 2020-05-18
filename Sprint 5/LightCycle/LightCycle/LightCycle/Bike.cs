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

    public class Bike
    {

        
        public Texture2D bike;
        public Rectangle rect;             
        public Rectangle rectDraw;         
        public Rectangle rectCollision;    
        public Vector2 origin;         
        public int velocity;
        public int direction;

       

        public Texture2D trail;
        public Trail currentTrail;

      
        public Bike(Texture2D bike, Texture2D trail, Rectangle position, int direction)
        {
            
            velocity = 4;
            this.direction = direction % 360;

            this.bike = bike;
            this.trail = trail;
            origin = new Vector2(bike.Width / 2, bike.Height / 2);
            rectDraw = position;

            Turn(this.direction);
        }

    
        public void Update()
        {
            rect = MoveRect(rect);
            rectDraw = MoveRect(rectDraw);
            rectCollision = MoveRect(rectCollision);

            currentTrail.ExtendRect(direction, velocity);
        }
        
        public Rectangle MoveRect(Rectangle r)
        {
            Rectangle o = r;
            double xtest = velocity * Math.Cos(MathHelper.ToRadians(direction));
            double ytest = velocity * Math.Sin(MathHelper.ToRadians(direction));
            o.X += (int)Math.Round(xtest);
            o.Y += (int)Math.Round(ytest);
            return o;
        }

       
        public void Turn(int d)
        {
            
            
            switch (d)
            {
                case 0:
                    rect = new Rectangle(rectDraw.X - rectDraw.Width / 2, rectDraw.Y - rectDraw.Height / 3, rectDraw.Width, (int)(rectDraw.Height / 1.5));
                    rectCollision = new Rectangle(rect.X + rect.Width - 10, rect.Y, 10, rect.Height);
                    break;
                case 90:
                    rect = new Rectangle(rectDraw.X - rectDraw.Height / 3, rectDraw.Y - rectDraw.Width / 2, (int)(rectDraw.Height / 1.5), rectDraw.Width);
                    rectCollision = new Rectangle(rect.X, rect.Y + rect.Height - 10, rect.Width, 10);
                    break;
                case 180:
                    rect = new Rectangle(rectDraw.X - rectDraw.Width / 2, rectDraw.Y - rectDraw.Height / 3, rectDraw.Width, (int)(rectDraw.Height / 1.5));
                    rectCollision = new Rectangle(rect.X, rect.Y, 10, rect.Height);
                    break;
                case 270:
                    rect = new Rectangle(rectDraw.X - rectDraw.Height / 3, rectDraw.Y - rectDraw.Width / 2, (int)(rectDraw.Height / 1.5), rectDraw.Width);
                    rectCollision = new Rectangle(rect.X, rect.Y, rect.Width, 10);
                    break;
                default:    
                   
                    break;
            }
            
            direction = d;
            currentTrail = new Trail(trail, new Rectangle(rectDraw.X - 10 / 2, rectDraw.Y - 10 / 2, 10, 10));
            
        }

        public void Draw(SpriteBatch sb, GameTime gameTime)
        {
            currentTrail.Draw(sb, gameTime);
            sb.Draw(bike, rectDraw, new Rectangle(0, 0, bike.Width, bike.Height), Color.White, MathHelper.ToRadians(direction), origin, SpriteEffects.None, 1f);
        }
    }
}