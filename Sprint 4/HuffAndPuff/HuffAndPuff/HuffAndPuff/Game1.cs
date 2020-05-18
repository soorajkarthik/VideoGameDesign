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

namespace HuffAndPuff
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D spriteSheet;
        SpriteFont font;

        Rectangle boyFrame;
        Rectangle featherFrame;

        Rectangle[] boyLeftSources;
        Rectangle[] boyRightSources;
        Rectangle[] featherSources;

        int direction;
        int boyIndex;
        int featherIndex;
        int featherSpeedY;
        int featherSpeedX;

        int tics;
        int puffTimer;
        int points;
        int boyAction;//0 = standing still, 1 = moving, 2 = puffing

        bool isGameOver;

        KeyboardState oldState;
        Random rand;

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

            boyFrame = new Rectangle(388, 402, 48, 68);
            featherFrame = new Rectangle(388, 10, 48, 48);

            boyLeftSources = new Rectangle[]
            {
                new Rectangle(0, 0, 24, 34),
                new Rectangle(25, 0, 24, 34),
                new Rectangle(50, 0, 24, 34),
                new Rectangle(75, 0, 24, 34),
                new Rectangle(100, 0, 24, 34)
            };

            boyRightSources = new Rectangle[]
            {
                new Rectangle(0, 35, 24, 34),
                new Rectangle(25, 35, 24, 34),
                new Rectangle(50, 35, 24, 34),
                new Rectangle(75, 35, 24, 34),
                new Rectangle(100, 35, 24, 34)
            };

            featherSources = new Rectangle[]
            {
                new Rectangle(0, 70, 24, 24),
                new Rectangle(25, 70, 24, 34),
                new Rectangle(50, 70, 24, 34),
                new Rectangle(75, 70, 24, 34)

            };

            direction = -1;
            boyIndex = 0;
            featherIndex = 0;
            boyAction = 0;
            tics = 0;
            puffTimer = 0;
            featherSpeedX = 0;
            featherSpeedY = 2;
            points = 0;

            isGameOver = false;
            
            oldState = Keyboard.GetState();
            rand = new Random();

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

            spriteSheet = Content.Load<Texture2D>("HuffNPuff");
            font = Content.Load<SpriteFont>("SpriteFont1");
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
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Escape))
                this.Exit();

            if (puffTimer <= 0)
            {
                boyAction = 0;

                if (state.IsKeyDown(Keys.Space) && oldState.IsKeyUp(Keys.Space))
                {
                    boyAction = 2;

                    if (featherFrame.Right > boyFrame.Left && featherFrame.Left < boyFrame.Right && featherFrame.Y > 300)
                    {
                        featherSpeedY -= 10;
                        points++;
                    }

                    puffTimer = 5;

                }

                else if (state.IsKeyDown(Keys.Left))
                {
                    direction = -1;
                    boyAction = 1;
                }

                else if (state.IsKeyDown(Keys.Right))
                {
                    direction = 1;
                    boyAction = 1;
                }

            }

            if(boyAction == 1)
                boyFrame.X += direction*3;

            if(tics % 10 == 0)
            {
                boyIndex++;
                featherIndex++;
                boyIndex %= 4;
                featherIndex %= 4; 
            }

            if (tics % 30 == 0)
                featherSpeedY += rand.Next(-10, 0);   
            

            if(tics % 60 == 0)
                featherSpeedX += rand.Next(-1, 1);

            featherFrame.Y += featherSpeedY;
            featherFrame.X += featherSpeedX;

            featherSpeedY++;
            if (featherSpeedY > 3)
                featherSpeedY = 3;

            //keep everything inbound
            if (boyFrame.X < 0)
                boyFrame.X = 0;

            if (boyFrame.Right > 800)
                boyFrame.X = 752;

            if (featherFrame.X < 0 || featherFrame.Right > 800)
                featherSpeedX *= -1;

            if (featherFrame.Y < 0)
                featherFrame.Y = 0;

            if (featherFrame.Y > 432)
                isGameOver = true;

            if (isGameOver && state.IsKeyDown(Keys.R))
                Initialize();

            tics++;
            puffTimer--;
            oldState = state;
            base.Update(gameTime);
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Wheat);

            spriteBatch.Begin();

            if(!isGameOver)
            {
                spriteBatch.Draw(spriteSheet, featherFrame, featherSources[featherIndex], Color.White);

                switch (boyAction)
                {
                    case 0:
                        if (direction == -1)
                            spriteBatch.Draw(spriteSheet, boyFrame, boyLeftSources[0], Color.White);
                        else
                            spriteBatch.Draw(spriteSheet, boyFrame, boyRightSources[0], Color.White);
                        break;

                    case 1:
                        if (direction == -1)
                            spriteBatch.Draw(spriteSheet, boyFrame, boyLeftSources[boyIndex], Color.White);
                        else
                            spriteBatch.Draw(spriteSheet, boyFrame, boyRightSources[boyIndex], Color.White);
                        break;

                    case 2:
                        if (direction == -1)
                            spriteBatch.Draw(spriteSheet, boyFrame, boyLeftSources[4], Color.White);
                        else
                            spriteBatch.Draw(spriteSheet, boyFrame, boyRightSources[4], Color.White);
                        break;
                }

                spriteBatch.DrawString(font, "Score: " + points, new Vector2(10, 10), Color.Black);

            }

            else
            {
                spriteBatch.DrawString(font, "Game Over :(", new Vector2(300, 180), Color.Black);
                spriteBatch.DrawString(font, "Score: " + points, new Vector2(300, 220), Color.Black);
                spriteBatch.DrawString(font, "Press R to restart", new Vector2(300, 260), Color.Black);
                spriteBatch.DrawString(font, "Press Esc to exit", new Vector2(300, 300), Color.Black);

            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
