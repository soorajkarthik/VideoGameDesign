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

namespace Pong
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

        Rectangle ballRect;
        Rectangle leftPaddleRect;
        Rectangle rightPaddleRect;
        Rectangle backgroundRect;

        double ballX;
        double ballY;

        double ballSpeedX;
        double ballSpeedY;

        int leftWins;
        int rightWins;

        int leftPoints;
        int rightPoints;

        int gameState;

        double spinRate;
        double angle;

        Rectangle top;
        Rectangle bottom;
        Rectangle left;
        Rectangle right;

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
            int screenWidth = graphics.GraphicsDevice.Viewport.Width;
            int screenHeight = graphics.GraphicsDevice.Viewport.Height;

            top = new Rectangle(0, 0, screenWidth, 5);
            bottom = new Rectangle(0, screenHeight, screenWidth, 20);
            left = new Rectangle(0, 0, 0, screenHeight);
            right = new Rectangle(screenWidth, 0, 0, screenHeight);

            ballRect = new Rectangle(50, 50, 20, 20);
            leftPaddleRect = new Rectangle(16, 176, 32, 128);
            rightPaddleRect = new Rectangle(752, 176, 32, 128);
            backgroundRect = new Rectangle(0, 0, 800, 480);

            leftWins = 0;
            rightWins = 0;

            leftPoints = 0;
            rightWins = 0;

            gameState = 0;

            reset();

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

            // TODO: use this.Content to load your game content here
            spriteSheet = Content.Load<Texture2D>("Pong Sprite Sheet");
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
            // Allows the game to exit
            KeyboardState kbState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || kbState.IsKeyDown(Keys.Escape))
                this.Exit();

            // TODO: Add your update logic here


            if (gameState == 0)
            {

                ballX += ballSpeedX;
                ballY += ballSpeedY;

                ballRect.X = (int)ballX;
                ballRect.Y = (int)ballY;

                if (ballRect.Intersects(top) || ballRect.Intersects(bottom))
                {
                    ballSpeedY *= -1;
                    ballSpeedX -= spinRate/60;
                    ballSpeedY += spinRate/60;
                    spinRate *= 0.95;

                    
                }

                else if (ballRect.Intersects(leftPaddleRect) || ballRect.Intersects(rightPaddleRect))
                {
                    ballSpeedX *= -1;
                    ballSpeedY -= spinRate/60;
                    ballSpeedX += spinRate / 60;
                    spinRate *= 0.95;

                    if (ballRect.Intersects(leftPaddleRect))
                        ballX = 50;
                    else
                        ballX = 730;
                }

                if (ballRect.Intersects(left))
                {
                    rightPoints++;
                    if (rightPoints >= 11 && rightPoints - leftPoints >= 2)
                    {
                        rightWins++;
                        gameState = 2;
                    }
                    else
                        reset();
                }

                if (ballRect.Intersects(right))
                {
                    leftPoints++;
                    if (leftPoints >= 11 && leftPoints - rightPoints >= 2)
                    {
                        leftWins++;
                        gameState = 1;
                    }
                    else
                        reset();
                }

                if(kbState.IsKeyDown(Keys.W))
                    leftPaddleRect.Y += leftPaddleRect.Y <= 0 ? 0 : -2;

                if (kbState.IsKeyDown(Keys.S))
                    leftPaddleRect.Y += leftPaddleRect.Y >= 352 ? 0 : 2;

                if (kbState.IsKeyDown(Keys.Up))
                    rightPaddleRect.Y += rightPaddleRect.Y <= 0 ? 0 : -2;

                if (kbState.IsKeyDown(Keys.Down))
                    rightPaddleRect.Y += rightPaddleRect.Y >= 352 ? 0 : 2;

                angle += spinRate/30;

            }
            
            else if (gameState > 0 && kbState.IsKeyDown(Keys.R))
            {
                gameState = 0;
                leftPoints = 0;
                rightPoints = 0;
                reset();
            }

            base.Update(gameTime);
        }

        public void reset()
        {

            Random rand = new Random();

            ballX = 390;
            ballY = 230;

            ballRect.X = (int)ballX;
            ballRect.Y = (int)ballY;

            leftPaddleRect.Y = 176;
            rightPaddleRect.Y = 176;

            ballSpeedX = rand.Next(-5, 5);
            ballSpeedY = rand.Next(-5, 5);

            spinRate = (rand.NextDouble() * 6 - 3) * 2 * Math.PI;
            angle = 0;

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
            spriteBatch.Draw(spriteSheet, backgroundRect, new Rectangle(0, 0, 800, 480), Color.White);

           switch(gameState)
            {
                case 0:
                    for (int i = 0; i < 480; i += 32)
                        spriteBatch.Draw(spriteSheet, new Rectangle(392, i, 16, 16), new Rectangle(867, 714, 16, 16), Color.White);

                    spriteBatch.Draw(spriteSheet, leftPaddleRect, new Rectangle(834, 714, 32, 128), Color.White);
                    spriteBatch.Draw(spriteSheet, rightPaddleRect, new Rectangle(834, 714, 32, 128), Color.White);
                    spriteBatch.Draw(spriteSheet, new Vector2(ballRect.Center.X, ballRect.Center.Y), new Rectangle(801, 0, 713, 713), Color.White, (float)angle, new Vector2(356.5f, 356.5f), new Vector2(0.02805f, 0.02805f), SpriteEffects.None, 0f);


                    spriteBatch.DrawString(font, String.Format("{0,2}", leftWins), new Vector2(322, 10), Color.White);
                    spriteBatch.DrawString(font, String.Format("{0,2}", rightWins), new Vector2(410, 10), Color.White);

                    spriteBatch.DrawString(font, String.Format("{0,2}", leftPoints), new Vector2(322, 430), Color.White);
                    spriteBatch.DrawString(font, String.Format("{0,2}", rightPoints), new Vector2(410, 430), Color.White);
                    break;

                case 1:
                    spriteBatch.DrawString(font, "Left Player Wins!", new Vector2(150, 150), Color.White);
                    spriteBatch.DrawString(font, String.Format("Current score: {0}-{1}", leftWins, rightWins), new Vector2(150, 200), Color.White);
                    spriteBatch.DrawString(font, "Press R to play again", new Vector2(150, 250), Color.White);
                    spriteBatch.DrawString(font, "Press Escape to quit", new Vector2(150, 300), Color.White);
                    break;                 
                         
                case 2:
                    spriteBatch.DrawString(font, "Right Player Wins!", new Vector2(150, 150), Color.White);
                    spriteBatch.DrawString(font, String.Format("Current score {0}:{1}", leftWins, rightWins), new Vector2(150, 200), Color.White);
                    spriteBatch.DrawString(font, "Press R to play again", new Vector2(150, 250), Color.White);
                    spriteBatch.DrawString(font, "Press Escape to quit", new Vector2(150, 300), Color.White);
                    break;
            }

           

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
