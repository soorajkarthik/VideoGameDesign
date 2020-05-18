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

namespace SideKick
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D smallTexture;
        Texture2D bigTexture;
        Texture2D stationaryTexture;

        Rectangle smallFrame;
        Rectangle bigFrame;
        Rectangle stationaryFrame;

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

            smallFrame = new Rectangle(30, 450, 50, 50);
            bigFrame = new Rectangle(0, 200, 150, 100);
            stationaryFrame = new Rectangle(400, 250, 100, 100);

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

            smallTexture = Content.Load<Texture2D>("SmallMovingObj");
            bigTexture = Content.Load<Texture2D>("BigMovingObj");
            stationaryTexture = Content.Load<Texture2D>("StationaryObj");

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

            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Escape))
                this.Exit();

            bigFrame.X++;
            smallFrame.X += 3;

            bigFrame.X %= 800;
            smallFrame.X %= 800;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(stationaryTexture, stationaryFrame, Color.White);

            if (smallFrame.Right > stationaryFrame.Left && smallFrame.Left < stationaryFrame.Right)
                spriteBatch.Draw(smallTexture, smallFrame, Color.Red);

            else
                spriteBatch.Draw(smallTexture, smallFrame, Color.White);


            if (bigFrame.Right > stationaryFrame.Left && bigFrame.Left < stationaryFrame.Right)
                spriteBatch.Draw(bigTexture, bigFrame, Color.Red);

            else
                spriteBatch.Draw(bigTexture, bigFrame, Color.White);

            

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
