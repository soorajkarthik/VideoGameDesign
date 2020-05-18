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

namespace TheBorg
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D turretTexture;
        Texture2D blankTexture;
        Texture2D borgTexture;

        SpriteFont font;

        Rectangle turretFrame;
        Rectangle[] tubeFrames;
        Point[] torpedoLaunchLocations;
        Rectangle torpedoFrame;
        List<Rectangle> borgFrames;

        Random random;

        int index;
        bool canFire;
        int torpedoDirection;

        int LSUCharge;
        int power;
        int tics;

        KeyboardState oldState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            index = 0;
            canFire = true;
            oldState = Keyboard.GetState();
            LSUCharge = 0;
            power = 0;

            turretFrame = new Rectangle(300, 140, 200, 200);
            tubeFrames = new Rectangle[]
            {
                new Rectangle(392, 110, 20, 40),
                new Rectangle(490, 230, 40, 20),
                new Rectangle(392, 330, 20, 40),
                new Rectangle(270, 230, 40, 20)
            };

            torpedoLaunchLocations = new Point[]
            {
                new Point(397, 100),
                new Point(530, 235),
                new Point(397, 370),
                new Point(260, 235)
            };

            torpedoFrame = new Rectangle(1000, 1000, 20, 20);

            borgFrames = new List<Rectangle>();

            random = new Random();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            turretTexture = Content.Load<Texture2D>("turret");
            blankTexture = Content.Load<Texture2D>("empty");
            borgTexture = Content.Load<Texture2D>("borg");
            font = Content.Load<SpriteFont>("SpriteFont1");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            KeyboardState currState = Keyboard.GetState();
            
            if(currState.IsKeyDown(Keys.Left) && !oldState.IsKeyDown(Keys.Left))
            {
                index += 3;
                index %= 4;
            }

            if (currState.IsKeyDown(Keys.Right) && !oldState.IsKeyDown(Keys.Right))
            {
                index++;
                index %= 4;
            }

            if(currState.IsKeyDown(Keys.Space) && canFire)
            {
                torpedoDirection = index;
                canFire = false;
                torpedoFrame.Width = 10;
                torpedoFrame.Height = 10;
                torpedoFrame.Location = torpedoLaunchLocations[index];
                

                if(power > LSUCharge)
                {
                    torpedoFrame.Inflate(LSUCharge * 2, LSUCharge * 2);
                    LSUCharge = 0;
                }
                
                else
                {
                    torpedoFrame.Inflate(power * 2, power * 2);
                    LSUCharge -= power;
                }    
                                
            }  

            for (int i = 0; i < currState.GetPressedKeys().Length; i++)
            {
                int key = (int) currState.GetPressedKeys()[i];
                if (key >= 96 && key <= 105)
                    power = key - 96;
            }

            switch (torpedoDirection)
            {
                case 0:
                    torpedoFrame.Y -= 2;
                    break;

                case 1:
                    torpedoFrame.X += 2;
                    break;

                case 2:
                    torpedoFrame.Y += 2;
                    break;

                case 3:
                    torpedoFrame.X -= 2;
                    break;
            }

            if (torpedoFrame.X < -torpedoFrame.Width || torpedoFrame.X > 800 || torpedoFrame.Y < -torpedoFrame.Height || torpedoFrame.Y > 480)
                canFire = true;

            if (tics % 60 == 0)
                LSUCharge += LSUCharge + 3 < 100 ? 3 : 100 - LSUCharge;

            if(random.NextDouble() < 0.0167)
            {
                int direction = random.Next(4);

                switch (direction)
                {
                    case 0:
                        borgFrames.Add(new Rectangle(382, random.Next(-66, 44), 40, 66));
                        break;

                    case 1:
                        borgFrames.Add(new Rectangle(random.Next(530, 800), 207, 40, 66));
                        break;

                    case 2:
                        borgFrames.Add(new Rectangle(382, random.Next(370, 400), 40, 66));
                        break;

                    case 3:
                        borgFrames.Add(new Rectangle(random.Next(-40, 230), 207, 40, 66));
                        break;
                }
            }

            for(int i = 0; i < borgFrames.Count; i++)
            {
                if(borgFrames.ElementAt(i).Intersects(torpedoFrame))
                {
                    borgFrames.RemoveAt(i);
                    i--;
                }
            }

            oldState = currState;
            tics++;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            spriteBatch.Draw(turretTexture, turretFrame, Color.White);
            
            for(int i = 0; i < tubeFrames.Length; i++)
            {
                if (i == index)
                    spriteBatch.Draw(blankTexture, tubeFrames[i], canFire ? Color.Green : Color.DarkRed);
                else
                    spriteBatch.Draw(blankTexture, tubeFrames[i], Color.Gray);
            }

            for(int i = 0; i < borgFrames.Count; i++)
                spriteBatch.Draw(borgTexture, borgFrames.ElementAt(i), Color.White);
            

            spriteBatch.Draw(blankTexture, torpedoFrame, Color.DarkBlue);
            spriteBatch.DrawString(font, LSUCharge + "MJ", new Vector2(377, 227), Color.White);
            spriteBatch.DrawString(font, "Power: " + power, new Vector2(10, 10), Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
