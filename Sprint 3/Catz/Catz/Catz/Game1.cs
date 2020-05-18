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

namespace Catz
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D catTexture;
        Texture2D mouseTexture;
        Rectangle catFrame;
        Rectangle mouseFrame;
        int tics;
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
            tics = 0;
            catFrame = new Rectangle(300, 300, 243, 207);
            mouseFrame = new Rectangle(100, 100, 115, 90);
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
            catTexture = Content.Load<Texture2D>("cat");
            mouseTexture = Content.Load<Texture2D>("mouse");
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

            // TODO: Add your update logic here
            tics++;
            KeyboardState state = Keyboard.GetState();
            Keys[] pressedKeys = state.GetPressedKeys();

            for(int i = 0; i < pressedKeys.Length; i++)
            {
                switch(pressedKeys[i])
                {
                    case Keys.W:
                        catFrame.Y--;
                        break;
                    case Keys.S:
                        catFrame.Y++;
                        break;
                    case Keys.A:
                        catFrame.X -= 2;
                        break;
                    case Keys.D:
                        catFrame.X += 2;
                        break;
                    case Keys.Up:
                        mouseFrame.Y -= 2;
                        break;
                    case Keys.Down:
                        mouseFrame.Y += 2;
                        break;
                    case Keys.Left:
                        mouseFrame.X--;
                        break;
                    case Keys.Right:
                        mouseFrame.X++;
                        break;
                }
            }

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

            spriteBatch.Draw(catTexture, catFrame, Color.White);
            spriteBatch.Draw(mouseTexture, mouseFrame, Color.White);
            spriteBatch.DrawString(font, "Time: " + (tics / 60), new Vector2(10, 10), Color.Black);
            
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
