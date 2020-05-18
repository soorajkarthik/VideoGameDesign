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

namespace ChessBoard
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Rectangle boardRect;
        List<Rectangle> pieceRects;

        Texture2D boardTex;
        List<Texture2D> pieceTexs;

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

            graphics.PreferredBackBufferWidth = 500;
            graphics.PreferredBackBufferHeight = 500;
            graphics.ApplyChanges();

            boardRect = new Rectangle(0, 0, 500, 500);

            pieceRects = new List<Rectangle>();
            pieceTexs = new List<Texture2D>();

            for(int row = 0; row < 4; row++)
            {
                for(int column = 0; column < 8; column++)
                {
                    switch (row)
                    {
                        case 0:
                            pieceRects.Add(new Rectangle((62 * column) + 16, 13, 33, 50));
                            break;

                        case 1:
                            pieceRects.Add(new Rectangle((62 * column) + 16, 75, 33, 50));
                            break;

                        case 2:
                            pieceRects.Add(new Rectangle((62 * column) + 16, 378, 33, 50));
                            break;

                        case 3:
                            pieceRects.Add(new Rectangle((62 * column) + 16, 440, 33, 50));
                            break;
                    }
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

            boardTex = Content.Load<Texture2D>("board");

            for (int i = 0; i < 32; i++)
            {
                if (i == 0 || i == 7)
                    pieceTexs.Add(Content.Load<Texture2D>("bc"));

                if(i == 1 || i == 6)
                    pieceTexs.Add(Content.Load<Texture2D>("bkn"));

                if(i == 2 || i == 5)
                    pieceTexs.Add(Content.Load<Texture2D>("bb"));

                if(i == 3)
                    pieceTexs.Add(Content.Load<Texture2D>("bki"));

                if(i == 4)
                    pieceTexs.Add(Content.Load<Texture2D>("bq"));

                if(i >= 8 && i <= 15)
                    pieceTexs.Add(Content.Load<Texture2D>("bp"));

                if (i == 24 || i == 31)
                    pieceTexs.Add(Content.Load<Texture2D>("wc"));
                                                           
                if (i == 25 || i == 30)                    
                    pieceTexs.Add(Content.Load<Texture2D>("wkn"));
                                                           
                if (i == 26 || i == 29)                    
                    pieceTexs.Add(Content.Load<Texture2D>("wb"));
                                                           
                if (i == 27)                               
                    pieceTexs.Add(Content.Load<Texture2D>("wki"));
                                                           
                if (i == 28)                               
                    pieceTexs.Add(Content.Load<Texture2D>("wq"));
                                                           
                if (i >= 16 && i <= 23)                    
                    pieceTexs.Add(Content.Load<Texture2D>("wp"));

            }

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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            // TODO: Add your update logic here

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

            spriteBatch.Draw(boardTex, boardRect, Color.White);

            for(int i = 0; i < 32; i++)
            {
                spriteBatch.Draw(pieceTexs.ElementAt(i), pieceRects.ElementAt(i), Color.White);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
