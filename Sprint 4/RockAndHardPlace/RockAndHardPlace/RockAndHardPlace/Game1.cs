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

namespace RockAndHardPlace
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D rockTexture;
        Texture2D hardPlaceTexture;

        Rectangle rockFrame;
        Rectangle hardPlaceFrame;

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
            graphics.PreferredBackBufferHeight = 800;
            graphics.ApplyChanges();

            rockFrame = new Rectangle(100, 100, 100, 100);
            hardPlaceFrame = new Rectangle(275, 275, 250, 250);

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

            rockTexture = Content.Load<Texture2D>("Rock");
            hardPlaceTexture = Content.Load<Texture2D>("Hard Place");
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

            if (state.IsKeyDown(Keys.Up))
                rockFrame.Y--;

            if (state.IsKeyDown(Keys.Down))
                rockFrame.Y++;

            if (state.IsKeyDown(Keys.Left))
                rockFrame.X--;

            if (state.IsKeyDown(Keys.Right))
                rockFrame.X++;


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

            spriteBatch.Draw(hardPlaceTexture, hardPlaceFrame, Color.White);

            if (isOverlapping(rockFrame, hardPlaceFrame))
                spriteBatch.Draw(rockTexture, rockFrame, Color.Red);

            else
                spriteBatch.Draw(rockTexture, rockFrame, Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public Boolean isOverlapping(Rectangle rec1, Rectangle rec2)
        {
            return (rec1.X < rec2.X + rec2.Width && rec1.X > rec2.X - rec1.Width)
                && (rec1.Y < rec2.Y + rec2.Height && rec1.Y > rec2.Y - rec1.Height);
        }
    }
}
