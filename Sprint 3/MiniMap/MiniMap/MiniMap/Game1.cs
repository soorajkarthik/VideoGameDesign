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

namespace MiniMap
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D map;
        Texture2D empty;

        Rectangle bigMapFrame;
        Rectangle miniMapFrame;
        Rectangle border;
        Rectangle miniPlayer;

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

            bigMapFrame = new Rectangle(-800, -960, 2400, 2400);
            miniMapFrame = new Rectangle(0, 0, 160, 160);
            border = new Rectangle(0, 0, 164, 164);
            miniPlayer = new Rectangle(0, 0, 54, 32);

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
            map = Content.Load<Texture2D>("map");
            empty = Content.Load<Texture2D>("empty");

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

            GamePadState state = GamePad.GetState(PlayerIndex.One);

            bigMapFrame.X -= (int)(5 * state.ThumbSticks.Left.X);
            bigMapFrame.Y += (int) (5 * state.ThumbSticks.Left.Y);

            if (bigMapFrame.X < -1600) bigMapFrame.X = -1600;
            if (bigMapFrame.X > 0) bigMapFrame.X = 0;

            if (bigMapFrame.Y < -1920) bigMapFrame.Y = -1920;
            if (bigMapFrame.Y > 0) bigMapFrame.Y = 0;

            
          
            miniPlayer.X = (-bigMapFrame.X) / 15;
            miniPlayer.Y = (-bigMapFrame.Y) / 15;
            

            // TODO: Add your update logic here

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

            spriteBatch.Draw(map, bigMapFrame, Color.White);
            spriteBatch.Draw(empty, border, Color.Black);
            spriteBatch.Draw(map, miniMapFrame, Color.White);
            spriteBatch.Draw(empty, miniPlayer, Color.White * 0.5f);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
