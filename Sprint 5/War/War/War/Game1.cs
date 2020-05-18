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

namespace War
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteFont font;

        List<Card> cards;
        Card c1, c2;
        string s1, s2;



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

            cards = new List<Card>();

            for(int i = 1; i <= 13; i++)
            {
                cards.Add(new Card("c", i));
                cards.Add(new Card("d", i));
                cards.Add(new Card("h", i));
                cards.Add(new Card("s", i));
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

            for(int i = 0; i < cards.Count; i++)
            {
                cards.ElementAt(i).tex = Content.Load<Texture2D>(String.Format("{0}{1:D2}", cards.ElementAt(i).suit, cards.ElementAt(i).value));
            }

            cards = Shuffle_Cards(cards.ElementAt(0), cards);

            c1 = cards.ElementAt(0);
            c2 = cards.ElementAt(1);

            s1 = GetString(c1);
            s2 = GetString(c2);

        }

        private string GetString(Card c)
        {
            string res = "";

            switch(c.value)
            {
                case 1:
                    res += "Ace";
                    break;

                case 11:
                    res += "Jack";
                    break;

                case 12:
                    res += "Queen";
                    break;

                case 13:
                    res += "King";
                    break;

                default:
                    res += c.value;
                    break;

            }

            switch (c.suit)
            {
                case "c":
                    res += " of Clubs";
                    break;

                case "d":
                    res += " of Diamonds";
                    break;

                case "h":
                    res += " of Hearts";
                    break;

                case "s":
                    res += " of Spades";
                    break;
            }

            return res;
        }

        public List<T> Shuffle_Cards<T>(T Value, List<T> CList)
        {
            // Local Vars
            int I, R;
            bool Flag;
            Random Rand = new Random();
            // Local List of T type
            var CardList = new List<T>();
            // Build Local List as big as passed in list and fill it with default value
            for (I = 0; I < CList.Count; I++)
                CardList.Add(Value);
            // Shuffle the list of cards
            for (I = 0; I < CList.Count; I++)
            {
                Flag = false;
                // Loop until an empty spot is found
                do
                {
                    R = Rand.Next(0, CList.Count);
                    if (CardList[R].Equals(Value))
                    {
                        Flag = true;
                        CardList[R] = CList[I];
                    }
                } while (!Flag);
            }

            // Return the shuffled list
            return CardList;
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

            c1.Draw(spriteBatch, new Rectangle(106, 109, 188, 263));
            c2.Draw(spriteBatch, new Rectangle(506, 109, 188, 263));

            spriteBatch.DrawString(font, s1, new Vector2(106, 380), Color.Black);
            spriteBatch.DrawString(font, s2, new Vector2(506, 380), Color.Black);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
