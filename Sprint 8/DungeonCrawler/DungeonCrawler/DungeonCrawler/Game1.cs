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

namespace DungeonCrawler
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Tile[,] tiles;
        readonly int tileSize = 32;
        List<Texture2D> imgs;
        Random rand;

        Rectangle camera;
        Rectangle man;
        Texture2D manImg;
        readonly int maxVelocity = 5;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.IsMouseVisible = true;
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
            tiles = new Tile[100, 50];
            rand = new Random();

            camera = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            man = new Rectangle((GraphicsDevice.Viewport.Width / 2) - (40 / 2), (GraphicsDevice.Viewport.Height / 2) - (64 / 2), 40, 64);

            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    tiles[x, y] = new Tile(new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize));
                }
            }

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
            imgs = new List<Texture2D>();
            for (int i = 0; i < 65; i++)
            {
                imgs.Add(this.Content.Load<Texture2D>("Tiles/" + i));
            }

            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    tiles[x, y].img = imgs[rand.Next(0, 65)];
                }
            }

            manImg = this.Content.Load<Texture2D>("Gingerbread Man");
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
            KeyboardState kb = Keyboard.GetState();
            GamePadState pad = GamePad.GetState(PlayerIndex.One);

            if (kb.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            if (pad.ThumbSticks.Right.Y > 0.3)  
            {
                if (man.Y > (GraphicsDevice.Viewport.Height / 2) - (man.Height / 2))
                {
                    if (man.Y - ((GraphicsDevice.Viewport.Height / 2) - (man.Height / 2)) < maxVelocity)
                    {
                        man.Y -= man.Y - ((GraphicsDevice.Viewport.Height / 2) - (man.Height / 2));
                    }
                    else
                    {
                        man.Y -= (int)(maxVelocity * pad.ThumbSticks.Right.Y);
                    }
                }
                else
                {
                    camera.Y -= (int)(maxVelocity * pad.ThumbSticks.Right.Y);
                    if (camera.Y < 0)
                    {
                        camera.Y = 0;
                        man.Y -= (int)(maxVelocity * pad.ThumbSticks.Right.Y);
                        if (man.Y < 0)
                        {
                            man.Y = 0;
                        }
                    }
                }
            }
            else if (pad.ThumbSticks.Right.Y < -0.3) 
            {
                if (man.Y < (GraphicsDevice.Viewport.Height / 2) - (man.Height / 2))
                {
                    if (-man.Y + (GraphicsDevice.Viewport.Height / 2) - (man.Height / 2) < maxVelocity)
                    {
                        man.Y += -man.Y + (GraphicsDevice.Viewport.Height / 2) - (man.Height / 2);
                    }
                    else
                    {
                        man.Y -= (int)(maxVelocity * pad.ThumbSticks.Right.Y);
                    }
                }
                else
                {
                    camera.Y -= (int)(maxVelocity * pad.ThumbSticks.Right.Y);
                    if (camera.Bottom > tiles.GetLength(1) * tileSize)
                    {
                        camera.Y = (tiles.GetLength(1) * tileSize) - camera.Height;
                        man.Y -= (int)(maxVelocity * pad.ThumbSticks.Right.Y);
                        if (man.Bottom > GraphicsDevice.Viewport.Height)
                        {
                            man.Y = GraphicsDevice.Viewport.Height - man.Height;
                        }
                    }
                }
            }

            if (pad.ThumbSticks.Right.X > 0.3) 
            {
                if (man.X < (GraphicsDevice.Viewport.Width / 2) - (man.Width / 2))
                {
                    if (-man.X + (GraphicsDevice.Viewport.Width / 2) - (man.Width / 2) < maxVelocity)
                    {
                        man.X += -man.X + (GraphicsDevice.Viewport.Width / 2) - (man.Width / 2);
                    }
                    else
                    {
                        man.X += (int)(maxVelocity * pad.ThumbSticks.Right.X);
                    }
                }
                else
                {
                    camera.X += (int)(maxVelocity * pad.ThumbSticks.Right.X);
                    if (camera.Right > tiles.GetLength(0) * tileSize)
                    {
                        camera.X = (tiles.GetLength(0) * tileSize) - camera.Width;
                        man.X += (int)(maxVelocity * pad.ThumbSticks.Right.X);
                        if (man.Right > GraphicsDevice.Viewport.Width)
                        {
                            man.X = GraphicsDevice.Viewport.Width - man.Width;
                        }
                    }
                }
            }
            else if (pad.ThumbSticks.Right.X < -0.3) 
            {
                if (man.X > (GraphicsDevice.Viewport.Width / 2) - (man.Width / 2))
                {
                    if (man.X - ((GraphicsDevice.Viewport.Width / 2) - (man.Width / 2)) < maxVelocity)
                    {
                        man.X -= man.X - ((GraphicsDevice.Viewport.Width / 2) - (man.Width / 2));
                    }
                    else
                    {
                        man.X += (int)(maxVelocity * pad.ThumbSticks.Right.X);
                    }
                }
                else
                {
                    camera.X += (int)(maxVelocity * pad.ThumbSticks.Right.X);
                    if (camera.X < 0)
                    {
                        camera.X = 0;
                        man.X += (int)(maxVelocity * pad.ThumbSticks.Right.X);
                        if (man.X < 0)
                        {
                            man.X = 0;
                        }
                    }
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
            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    if (tiles[x, y].loc.Intersects(camera))
                    {
                        tiles[x, y].Draw(spriteBatch, camera);
                    }
                }
            }
            spriteBatch.Draw(manImg, man, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
