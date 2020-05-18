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

namespace Raceway
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SoundEffect blownEngineEffect;

        Texture2D car1Texture;
        Texture2D car2Texture;
        Texture2D trackTexture;


        Rectangle car1Frame;
        Rectangle car2Frame;
        Rectangle trackFrame;

        bool isAtStartScreen;
        bool isAtCountDown;
        bool isPlaying;

        int car1Power;
        int car2Power;

        int tics;
        int whoWon;

        Random random;

        KeyboardState oldState;

        SpriteFont font;

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

            isAtStartScreen = true;
            isAtCountDown = false;
            isPlaying = false;
            whoWon = 0;
            car1Frame = new Rectangle(337, 40, 125, 96);
            car2Frame = new Rectangle(337, 200, 125, 125);
            trackFrame = new Rectangle(0, 100, 800, 250);
            tics = 0;
            random = new Random();
            oldState = Keyboard.GetState();

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
            car1Texture = Content.Load<Texture2D>("Race Car #1");
            car2Texture = Content.Load<Texture2D>("Race Car #2");
            trackTexture = Content.Load<Texture2D>("Race Track");
            blownEngineEffect = Content.Load<SoundEffect>("BlownEngineSound");
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

            if (currState.IsKeyDown(Keys.Space))
            {
                isAtStartScreen = false;
                isAtCountDown = true;
                tics = 0;
            }

            if (tics == 300 && isAtCountDown)
            {
                isAtCountDown = false;
                isPlaying = true;
                car1Frame.X = 0;
                car2Frame.X = 0;

                car1Frame.Y = 100;
                car2Frame.Y = 225;
            }
                

            if(tics > 300 && isPlaying)
            {
                if (currState.IsKeyDown(Keys.W) && !oldState.IsKeyDown(Keys.W) && car1Power < 10)
                    car1Power++;

                if (currState.IsKeyDown(Keys.S) && !oldState.IsKeyDown(Keys.S) && car1Power > 0)
                    car1Power--;

                if (currState.IsKeyDown(Keys.Up) && !oldState.IsKeyDown(Keys.Up) && car2Power < 10)
                    car2Power++;

                if (currState.IsKeyDown(Keys.Down) && !oldState.IsKeyDown(Keys.Down) && car2Power > 0)
                    car2Power--;

                if (car1Frame.X > 800)
                {
                    isPlaying = false;
                    car1Frame.X = 337;
                    car1Frame.X = 152;
                    whoWon = 1;
                }

                if (car2Frame.X > 800)
                {
                    isPlaying = false;
                    car2Frame.X = 337;
                    car2Frame.Y = 137;
                    whoWon = 2;
                }

                car1Frame.X += random.Next(car1Power + 1);
                car2Frame.X += random.Next(car2Power + 1);

                switch (car1Power)
                {
                    case 5:
                        if (random.NextDouble() <= 0.05)
                        {
                            car1Power = 0;
                            blownEngineEffect.Play();
                        }
                        break;
                    case 6:
                        if (random.NextDouble() <= 0.1)
                        {
                            car1Power = 0;
                            blownEngineEffect.Play();
                        }
                        break;
                    case 7:
                        if (random.NextDouble() <= 0.15)
                        {
                            car1Power = 0;
                            blownEngineEffect.Play();
                        }
                        break;
                    case 8:
                        if (random.NextDouble() <= 0.20)
                        {
                            car1Power = 0;
                            blownEngineEffect.Play();
                        }
                        break;
                    case 9:
                        if (random.NextDouble() <= 0.25)
                        {
                            car1Power = 0;
                            blownEngineEffect.Play();
                        }
                        break;
                    case 10:
                        if (random.NextDouble() <= 0.30)
                        {
                            car1Power = 0;
                            blownEngineEffect.Play();
                        }
                        break;
                }

                switch (car2Power)
                {
                    case 5:
                        if (random.NextDouble() <= 0.05)
                        {
                            car2Power = 0;
                            blownEngineEffect.Play();
                        }
                        break;
                    case 6:
                        if (random.NextDouble() <= 0.1)
                        {
                            car2Power = 0;
                            blownEngineEffect.Play();
                        }
                        break;
                    case 7:
                        if (random.NextDouble() <= 0.15)
                        {
                            car2Power = 0;
                            blownEngineEffect.Play();
                        }
                        break;
                    case 8:
                        if (random.NextDouble() <= 0.20)
                        {
                            car2Power = 0;
                            blownEngineEffect.Play();
                        }
                        break;
                    case 9:
                        if (random.NextDouble() <= 0.25)
                        {
                            car2Power = 0;
                            blownEngineEffect.Play();
                        }
                        break;
                    case 10:
                        if (random.NextDouble() <= 0.30)
                        {
                            car2Power = 0;
                            blownEngineEffect.Play();
                        }
                        break;
                }
            }

            if(!isAtStartScreen && !isAtCountDown&& !isPlaying && currState.IsKeyDown(Keys.Enter))
                this.Exit();



            tics++;
            oldState = currState;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            if (isAtStartScreen)
            {
                spriteBatch.DrawString(font, "Welcome to the CTE Center Raceway", new Vector2(100, 10), Color.Black);
                spriteBatch.Draw(car1Texture, car1Frame, Color.White);
                spriteBatch.DrawString(font, "Car 1: W -> increase. S -> decrease", new Vector2(120, 140), Color.Black);
                spriteBatch.DrawString(font, "Car 2: Up Arrow -> increase. Down Arrow -> decrease", new Vector2(50, 330), Color.Black);
                spriteBatch.Draw(car2Texture, car2Frame, Color.White);
                spriteBatch.DrawString(font, "Press Space to Continue", new Vector2(100, 370), Color.Black);
            }

            else if (isAtCountDown)
            {
                GraphicsDevice.Clear(Color.Black);
                spriteBatch.DrawString(font, "" + ((360 - tics) / 60), new Vector2(390, 190), Color.White);
            }

            else if (isPlaying)
            {
                spriteBatch.Draw(trackTexture, trackFrame, Color.White);
                spriteBatch.Draw(car1Texture, car1Frame, Color.White);
                spriteBatch.Draw(car2Texture, car2Frame, Color.White);
                spriteBatch.DrawString(font, "Car 1 Power: " + car1Power, new Vector2(10, 10), Color.Black);
                spriteBatch.DrawString(font, "Car 2 Power: " + car2Power, new Vector2(625, 10), Color.Black);


            }

            else
            {
                if(whoWon == 1)
                    spriteBatch.Draw(car1Texture, car1Frame, Color.White);

                if(whoWon == 2)
                    spriteBatch.Draw(car2Texture, car2Frame, Color.White);

                spriteBatch.DrawString(font, "Winner!!", new Vector2(363, 263), Color.Black);
                spriteBatch.DrawString(font, "Press Return to Exit", new Vector2(220, 375), Color.Black);
                
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
