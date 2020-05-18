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

namespace JokersWild
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    /// 
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D aceTexture;
        Texture2D tenTexture;
        Texture2D jackTexture;
        Texture2D jokerTexture;
        Texture2D cardBackTexture;

        SoundEffect foghornEffect;
        SoundEffect ovationEffect;

        List<Texture2D> textures;
        Rectangle[] frames;

        Random random;

        SpriteFont font;

        //top left:0, top right: 1, bottom left: 2, top left: 3
        int jokerIndex;
        List<int> visitedIndexes;
        int mouseIndex;

        bool hasLost;
        bool hasWon;


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

            random = new Random();

            frames = new Rectangle[]
            {
                new Rectangle(0, 0, 400, 200),
                new Rectangle(400, 0, 400, 200),
                new Rectangle(0, 200, 400, 200),
                new Rectangle(400, 200, 400, 200)
            };

            textures = new List<Texture2D>();
            visitedIndexes = new List<int>();
            jokerIndex = random.Next(4);

            hasLost = false;
            hasWon = false;

            IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.

            aceTexture = Content.Load<Texture2D>("AceofDiamonds");
            tenTexture = Content.Load<Texture2D>("10ofSpades");
            jackTexture = Content.Load<Texture2D>("JackofHearts");
            jokerTexture = Content.Load<Texture2D>("Joker#1");
            cardBackTexture = Content.Load<Texture2D>("Card Back");

            foghornEffect = Content.Load<SoundEffect>("foghorn");
            ovationEffect = Content.Load<SoundEffect>("Ovation");

            font = Content.Load<SpriteFont>("SpriteFont1");

            for(int i = 0; i < 4; i++)
            {
                if(i == jokerIndex)
                {
                    textures.Add(jokerTexture);
                    continue;
                }

                if(!textures.Contains(aceTexture))
                {
                    textures.Add(aceTexture);
                    continue;
                }

                if (!textures.Contains(tenTexture))
                {
                    textures.Add(tenTexture);
                    continue;
                }

                if (!textures.Contains(jackTexture))
                {
                    textures.Add(jackTexture);
                    continue;
                }
            }

            spriteBatch = new SpriteBatch(GraphicsDevice);

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

            MouseState state = Mouse.GetState();

            for(int i = 0; i < frames.Count(); i++)
            {
                if (frames[i].Contains(state.X, state.Y))
                {
                    visitedIndexes.Add(i);
                    mouseIndex = i;
                    break;
                }
            }

            if(visitedIndexes.Contains(jokerIndex))
            {
                if(!visitedIndexes.Contains(0) || !visitedIndexes.Contains(1) || !visitedIndexes.Contains(2) || !visitedIndexes.Contains(3))
                {
                    hasLost = true;
                    foghornEffect.Play();
                }

                else
                {
                    hasWon = true;
                    ovationEffect.Play();
                }
            }

            if(hasLost || hasWon)
            {
                KeyboardState keyboardState = Keyboard.GetState();
                if (keyboardState.IsKeyDown(Keys.Y))
                    Initialize();

                if (keyboardState.IsKeyDown(Keys.N))
                    this.Exit();
            }

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

            // TODO: Add your drawing code here

            spriteBatch.Begin();
            if(hasLost)
            {
                spriteBatch.DrawString(font, "Joker's Wild You Lose!", new Vector2(200, 100), Color.Black);
                spriteBatch.DrawString(font, "Y to play again, N to quit", new Vector2(200, 375), Color.Black);
            }

            else if(hasWon)
            {
                spriteBatch.DrawString(font, "You Beat the Joker!", new Vector2(200, 100), Color.Black);
                spriteBatch.DrawString(font, "Y to play again, N to quit", new Vector2(200, 375), Color.Black);
            }

            else
            {
                for(int i = 0; i < textures.Count(); i++)
                {
                    if (i == mouseIndex)
                        spriteBatch.Draw(textures.ElementAt(i), frames[i], Color.White);

                    else
                        spriteBatch.Draw(cardBackTexture, frames[i], Color.White);
                }
            }


            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
