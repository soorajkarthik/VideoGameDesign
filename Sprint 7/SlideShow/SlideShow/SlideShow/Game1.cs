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
using System.IO;

namespace SlideShow
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D spriteSheet;
        int current;
        int tics;
        List<Rectangle> sources;
        Rectangle drawRect;


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

            current = 0;
            tics = 0;
            drawRect = new Rectangle();
            sources = new List<Rectangle>();

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

            spriteSheet = Content.Load<Texture2D>("sprite sheet");
            loadRectangles(@"Content/data.txt");
            // TODO: use this.Content to load your game content here
        }


        private void loadRectangles(string path)
        {
            try
            {
                using(StreamReader reader = new StreamReader(path))
                {
                    while(!reader.EndOfStream)
                    {
                        string[] line = reader.ReadLine().Split(' ');

                        int x = Convert.ToInt32(line[0]);
                        int y = Convert.ToInt32(line[1]);
                        int w = Convert.ToInt32(line[2]);
                        int h = Convert.ToInt32(line[3]);

                        sources.Add(new Rectangle(x, y, w, h));
                    }
                }
            } 

            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
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

            if(tics % 120 == 0)
            {
                current++;
                current %= sources.Count;
            }

            tics++;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            drawRect.Width = sources[current].Width;
            drawRect.Height = sources[current].Height;

            drawRect.X = 400 - drawRect.Width / 2;
            drawRect.Y = 240 - drawRect.Height / 2;

            spriteBatch.Begin();
            spriteBatch.Draw(spriteSheet, drawRect, sources[current], Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
