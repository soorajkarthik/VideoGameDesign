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

namespace JourneyToBabel
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;

        KeyboardState oldkb;

        enum GameState
        {
            start, 
            save,
            done
        }

        enum Language
        {
            english, 
            spanish, 
            german
        }

        GameState state;
        Language language;

        Dictionary<string, string> english;
        Dictionary<string, string> spanish;
        Dictionary<string, string> german;
        List<string> statePhrases;

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

            oldkb = Keyboard.GetState();

            state = GameState.start;
            language = Language.english;

            english = new Dictionary<string, string>();
            spanish = new Dictionary<string, string>();
            german = new Dictionary<string, string>();
            statePhrases = new List<string>();

            try
            {
                using (StreamReader reader = new StreamReader(@"Content/Output Messages.txt"))
                {
                    while (!reader.EndOfStream)
                    {
                        string state = reader.ReadLine();
                        statePhrases.Add(state);

                        english.Add(state, reader.ReadLine());
                        spanish.Add(state, reader.ReadLine());
                        german.Add(state, reader.ReadLine());

                        reader.ReadLine();
                    }
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
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
            font = Content.Load<SpriteFont>("SpriteFont1");
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
            KeyboardState kb = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || kb.IsKeyDown(Keys.Escape))
                this.Exit();

            if (kb.IsKeyDown(Keys.D1) || kb.IsKeyDown(Keys.NumPad1))
                language = Language.english;

            if (kb.IsKeyDown(Keys.D2) || kb.IsKeyDown(Keys.NumPad2))
                language = Language.spanish;

            if (kb.IsKeyDown(Keys.D3) || kb.IsKeyDown(Keys.NumPad3))
                language = Language.german;

            if (kb.IsKeyDown(Keys.Space) && !oldkb.IsKeyDown(Keys.Space))
            {
                int temp = (int)state;
                temp++;
                temp %= 3;
                state = (GameState)temp;
            }


            oldkb = kb;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            string key = statePhrases[(int)state];

            spriteBatch.Begin();

            switch(language)
            {
                case Language.english:
                    spriteBatch.DrawString(font, english[key], new Vector2(200, 220), Color.Black);
                    break;

                case Language.spanish:
                    spriteBatch.DrawString(font, spanish[key], new Vector2(200, 220), Color.Black);
                    break;

                case Language.german:
                    spriteBatch.DrawString(font, german[key], new Vector2(200, 220), Color.Black);
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
