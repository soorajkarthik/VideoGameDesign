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

namespace DeathBlossom
{
    class Gunstar
    {
        Texture2D gunstarTex;
        Rectangle gunstarRect;
        Vector2 gunstarCenter;
        Boolean isFiring;
        double heading;
        int fireCounter;
        Random rand;

        public double Heading
        {
            get
            {
                return heading;
            }
        }
        public Boolean IsFiring
        {
            get
            {
                return isFiring;
            }
        }
        public Vector2 Location
        {
            get
            {
                return gunstarCenter;
            }
        }

        public Gunstar(Texture2D texture, Rectangle location)
        {
            gunstarTex = texture;
            gunstarRect = location;
            gunstarCenter = new Vector2(gunstarRect.X + gunstarRect.Width / 2,
                                        gunstarRect.Y + gunstarRect.Height / 2);
            isFiring = false;
            heading = 0;
            fireCounter = 0;
            rand = new Random();
        }

        public void fire()
        {
            isFiring = true;
        }

        public void Update(GameTime gameTime)
        {
            if(isFiring)
            {
                fireCounter++;
                if (fireCounter < 600)
                {
                    double multiplier = (fireCounter / 600.0) * 100;
                    heading = MathHelper.ToRadians((MathHelper.ToDegrees((float)heading) + (float)(rand.NextDouble() * multiplier)) % 360);
                }
                else
                {
                    isFiring = false;
                    fireCounter = 0;
                    heading = 0;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(gunstarTex, gunstarCenter, null, Color.White, 
                (float)heading, 
                new Vector2(gunstarTex.Width/2, gunstarTex.Height/2), 0.2f, 
                SpriteEffects.None, 0);
        }
    }
}
