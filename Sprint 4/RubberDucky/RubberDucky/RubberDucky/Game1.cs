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

namespace RubberDucky
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D spriteSheet;
        Rectangle[] destinationRectangles;
        Rectangle[] sourceRectangles;

        float[] angles;

        KeyboardState oldState;

        string pressedLetter;
        int numKeyPressed;
        int numpadPressed;
        bool brotherDuckFound;

        int topDuckSpeed;
        int bottomDuckSpeed;

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

            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();

            destinationRectangles = new Rectangle[]
            {
                new Rectangle(150, 139, 200, 177),
                new Rectangle(400, 139, 200, 177),
                new Rectangle(650, 139, 200, 177),
                new Rectangle(150, 439, 200, 177),
                new Rectangle(400, 439, 200, 177),
                new Rectangle(650, 439, 200, 177)

            };

            sourceRectangles = new Rectangle[]
            {
                new Rectangle(0, 0, 200, 177),
                new Rectangle(200, 0, 200, 177),
                new Rectangle(400, 0, 200, 177),
                new Rectangle(600, 0, 200, 177),
                new Rectangle(800, 0, 200, 177),
                new Rectangle(1000, 0, 200, 177)
            };

            angles = new float[6];
            angles[4] = (float)Math.PI;

            pressedLetter = "";
            numKeyPressed = -1;
            numpadPressed = -1;

            topDuckSpeed = -2;
            bottomDuckSpeed = -4;

            brotherDuckFound = false;

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
            spriteSheet = Content.Load<Texture2D>("Duckies");

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

            KeyboardState currState = Keyboard.GetState();
            Keys[] pressedKeys = currState.GetPressedKeys();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || currState.IsKeyDown(Keys.Escape))
                this.Exit();

            if(brotherDuckFound)
            {
                if (numKeyPressed > numpadPressed)
                {
                    destinationRectangles[numKeyPressed - 1].X += topDuckSpeed;
                    destinationRectangles[numpadPressed - 1].X += bottomDuckSpeed;

                    if (destinationRectangles[numKeyPressed - 1].Left < 0 || destinationRectangles[numKeyPressed - 1].Right > 800)
                        topDuckSpeed *= -1;

                    if (destinationRectangles[numpadPressed - 1].Left < 0 || destinationRectangles[numpadPressed - 1].Right > 800)
                        bottomDuckSpeed *= -1;
                }

                else
                {
                    destinationRectangles[numpadPressed - 1].X += topDuckSpeed;
                    destinationRectangles[numKeyPressed - 1].X += bottomDuckSpeed;


                    if (destinationRectangles[numpadPressed - 1].X < 0 || destinationRectangles[numpadPressed - 1].Right > 800)
                        topDuckSpeed *= -1;

                    if (destinationRectangles[numKeyPressed - 1].X < 0 || destinationRectangles[numKeyPressed - 1].Right > 800)
                        bottomDuckSpeed *= -1;
                }


                if (oldState.GetPressedKeys().Length == 0)
                {
                    if (currState.IsKeyDown(Keys.Space))
                        Initialize();

                    if (currState.IsKeyDown(Keys.A))
                    {
                        Initialize();
                        pressedLetter = "a";
                    }

                    if (currState.IsKeyDown(Keys.E))
                    {
                        Initialize();
                        pressedLetter = "e";
                    }

                    if (currState.IsKeyDown(Keys.O))
                    {
                        Initialize();
                        pressedLetter = "o";
                    }

                    for (int i = 0; i < pressedKeys.Length; i++)
                    {
                        Initialize();
                        if (pressedKeys[i] >= Keys.D1 && pressedKeys[i] <= Keys.D6)
                            numKeyPressed = (int)pressedKeys[i] - 48;
                    }

                }

            }

            else
            {
                if (currState.IsKeyDown(Keys.A))
                {
                    pressedLetter = "a";
                    numKeyPressed = -1;
                }

                if (currState.IsKeyDown(Keys.E))
                {
                    pressedLetter = "e";
                    numKeyPressed = -1;
                }

                if (currState.IsKeyDown(Keys.O))
                {
                    pressedLetter = "o";
                    numKeyPressed = -1;
                }

                for (int i = 0; i < pressedKeys.Length; i++)
                {
                    if (pressedKeys[i] >= Keys.D1 && pressedKeys[i] <= Keys.D6)
                    {
                        numKeyPressed = (int)pressedKeys[i] - 48;
                        pressedLetter = "";
                    }

                    if (pressedKeys[i] >= Keys.NumPad1 && pressedKeys[i] <= Keys.NumPad6)
                    {
                        numpadPressed = (int)pressedKeys[i] - 96;
                        pressedLetter = "";
                    }
                }

                if (numKeyPressed > 0)
                    angles[numKeyPressed - 1] += MathHelper.ToRadians(30f);

                switch (pressedLetter)
                {
                    case "a":
                        for (int i = 0; i < angles.Length; i++)
                        {
                            if(numKeyPressed - 1 != i)
                                angles[i] -= MathHelper.ToRadians(10f);
                        }
                        break;

                    case "e":
                        for (int i = 1; i < angles.Length; i += 2)
                        {
                            if (numKeyPressed - 1 != i)
                                angles[i] += MathHelper.ToRadians(30f);
                        }
                        break;

                    case "o":
                        for (int i = 0; i < angles.Length; i += 2)
                        {
                            if (numKeyPressed - 1 != i)
                                angles[i] -= MathHelper.ToRadians(30f);
                        }
                        break;
                }

                if (numKeyPressed + numpadPressed == 7)
                {
                    brotherDuckFound = true;
                    if (numKeyPressed > numpadPressed)
                    {
                        destinationRectangles[numKeyPressed - 1].Y = 50;
                        destinationRectangles[numKeyPressed - 1].X = 311;

                        destinationRectangles[numpadPressed - 1].Y = 350;
                        destinationRectangles[numpadPressed - 1].X = 311;
                    }

                    else
                    {
                        destinationRectangles[numpadPressed - 1].Y = 50;
                        destinationRectangles[numpadPressed - 1].X = 311;

                        destinationRectangles[numKeyPressed - 1].Y = 350;
                        destinationRectangles[numKeyPressed - 1].X = 311;
                    }
                    
                }

                if (currState.IsKeyDown(Keys.Space))
                    Initialize();
            }

            oldState = currState;           

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);


            spriteBatch.Begin();

            if (brotherDuckFound)
            {

                if (numKeyPressed > numpadPressed)
                {
                    if(destinationRectangles[numKeyPressed - 1].Left < destinationRectangles[numpadPressed - 1].Right && destinationRectangles[numKeyPressed - 1].Right > destinationRectangles[numpadPressed - 1].Left)
                        spriteBatch.Draw(spriteSheet, destinationRectangles[numKeyPressed - 1], sourceRectangles[numKeyPressed - 1], Color.Blue);

                    else
                        spriteBatch.Draw(spriteSheet, destinationRectangles[numKeyPressed - 1], sourceRectangles[numKeyPressed - 1], Color.White);

                    spriteBatch.Draw(spriteSheet, destinationRectangles[numpadPressed - 1], sourceRectangles[numpadPressed - 1], Color.White);

                }

                else
                {
                    if (destinationRectangles[numpadPressed - 1].Left < destinationRectangles[numKeyPressed - 1].Right && destinationRectangles[numpadPressed - 1].Right > destinationRectangles[numKeyPressed - 1].Left)
                        spriteBatch.Draw(spriteSheet, destinationRectangles[numpadPressed - 1], sourceRectangles[numpadPressed - 1], Color.Blue);

                    else
                        spriteBatch.Draw(spriteSheet, destinationRectangles[numpadPressed - 1], sourceRectangles[numpadPressed - 1], Color.White);

                    spriteBatch.Draw(spriteSheet, destinationRectangles[numKeyPressed - 1], sourceRectangles[numKeyPressed - 1], Color.White);

                }

            }

            else
            {
                for (int i = 0; i < sourceRectangles.Length; i++)
                {
                    spriteBatch.Draw(spriteSheet, destinationRectangles[i], sourceRectangles[i], Color.White, angles[i], new Vector2(100, 89), SpriteEffects.None, 0f);

                }
            }
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
