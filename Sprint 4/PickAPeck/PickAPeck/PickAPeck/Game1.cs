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

namespace PickAPeck
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D spriteSheet;

        Rectangle[] sourceRectangles;
        Rectangle[] frames;

        SpriteFont font;

        int bigIndex;

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

            sourceRectangles = new Rectangle[]
            {
                new Rectangle(278, 0, 50, 50),
                new Rectangle(0, 201, 150, 50),
                new Rectangle(227, 0, 50, 150),
                new Rectangle(151, 0, 75, 200),
                new Rectangle(0, 0, 150, 200)
            };

            frames = new Rectangle[]
            {
                new Rectangle(100, 10, 50, 50),
                new Rectangle(200, 10, 150, 50),
                new Rectangle(400, 10, 50, 150),
                new Rectangle(500, 10, 75, 200),
                new Rectangle(625, 10, 150, 200)

            };

            bigIndex = 0;

            graphics.PreferredBackBufferWidth = 875;
            graphics.PreferredBackBufferHeight = 900;
            graphics.ApplyChanges();

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
            spriteSheet = Content.Load<Texture2D>("SpriteSheet");
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
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Escape))
                this.Exit();

            if (state.IsKeyDown(Keys.D1) || state.IsKeyDown(Keys.NumPad1))
                bigIndex = 0;

            if (state.IsKeyDown(Keys.D2) || state.IsKeyDown(Keys.NumPad2))
                bigIndex = 1;

            if (state.IsKeyDown(Keys.D3) || state.IsKeyDown(Keys.NumPad3))
                bigIndex = 2;

            if (state.IsKeyDown(Keys.D4) || state.IsKeyDown(Keys.NumPad4))
                bigIndex = 3;

            if (state.IsKeyDown(Keys.D5) || state.IsKeyDown(Keys.NumPad5))
                bigIndex = 4;

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

            for(int i = 0; i < sourceRectangles.Count(); i++)
            {
                if (i == bigIndex)
                {
                    Rectangle bigImageFrame = new Rectangle(450 - sourceRectangles[i].Width, 300, sourceRectangles[i].Width * 2, sourceRectangles[i].Height * 2);
                    spriteBatch.Draw(spriteSheet, bigImageFrame, sourceRectangles[i], Color.White);
                }
                spriteBatch.Draw(spriteSheet, frames[i], sourceRectangles[i], Color.White);

            }

            spriteBatch.DrawString(font, "1", new Vector2(115, 70), Color.Black);
            spriteBatch.DrawString(font, "2", new Vector2(265, 70), Color.Black);
            spriteBatch.DrawString(font, "3", new Vector2(415, 170), Color.Black);
            spriteBatch.DrawString(font, "4", new Vector2(527, 210), Color.Black);
            spriteBatch.DrawString(font, "5", new Vector2(690, 210), Color.Black);


            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
