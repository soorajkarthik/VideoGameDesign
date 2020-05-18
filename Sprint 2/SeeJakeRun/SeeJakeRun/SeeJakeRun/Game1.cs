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

/*
 * Pseudocode:
 * Texture2D tigerTexture
 * Texture2D mouseTexture
 * Rectangle tigerFrame
 * Rectangle mouseFrame
 * double imageScale
 * 
 * in initialize:
 * initialize rectangles
 * 
 * in loadcontent:
 * set textures
 * set rectangles to correct size and coordinates
 * 
 * in update:
 * X++;
 * if X > width of window, 
 * reset X to 0;
 * 
 */

namespace SeeJakeRun
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D tigerTexture;
        Texture2D mouseTexture;
        Rectangle tigerFrame;
        Rectangle mouseFrame;
        double tigerImageScale;
        double mouseImageScale;

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

            tigerFrame = new Rectangle(0, 0, 0, 0);
            mouseFrame = new Rectangle(0, 0, 0, 0);
            tigerImageScale = 1;
            mouseImageScale = 0.1;

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

            tigerTexture = Content.Load<Texture2D>("tiger");
            mouseTexture = Content.Load<Texture2D>("mouse");

            tigerFrame.Width = (int) (tigerTexture.Width * tigerImageScale);
            tigerFrame.Height = (int)(tigerTexture.Height * tigerImageScale);
            mouseFrame.Width= (int)(mouseTexture.Width * mouseImageScale);
            mouseFrame.Height = (int)(mouseTexture.Height * mouseImageScale);

            tigerFrame.X = 0;
            tigerFrame.Y = GraphicsDevice.Viewport.Height - tigerFrame.Height;

            mouseFrame.X = tigerFrame.Width + 10;
            mouseFrame.Y = GraphicsDevice.Viewport.Height - mouseFrame.Height;
    
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

            tigerFrame.X = ++tigerFrame.X % GraphicsDevice.Viewport.Width;
            mouseFrame.X = ++mouseFrame.X % GraphicsDevice.Viewport.Width;

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
            spriteBatch.Draw(tigerTexture, tigerFrame, Color.White);
            spriteBatch.Draw(mouseTexture, mouseFrame, Color.White);
            spriteBatch.End();
                        
            base.Draw(gameTime);
        }
    }
}
