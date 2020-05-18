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

namespace IIWGTI
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        enum SubjectState
        {
            CompSci, 
            English,
            Math,
            PE
        }

        Texture2D spriteSheet;
        KeyboardState oldKb;
        SubjectState state;
        SpriteFont font;

        SubjectImage[][] images;
        bool[] subjectIsFlipped;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1750;
            graphics.PreferredBackBufferHeight = 700;
            graphics.ApplyChanges();

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
            state = SubjectState.CompSci;
            images = new SubjectImage[4][];
            subjectIsFlipped = new bool[4];

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
            spriteSheet = Content.Load<Texture2D>("Sprint 7 Test Image");
            font = Content.Load<SpriteFont>("SpriteFont1");

            LoadImages();
        }

        private void LoadImages()
        {
            try
            {
                using (StreamReader nameReader = new StreamReader(@"Content/Sprint 7 Test Image - Names.txt"))
                {
                    using(StreamReader pointReader = new StreamReader(@"Content/Sprint 7 Test Image - Points.txt"))
                    {
                        for(int i = 0; i < images.Length; i++)
                        {
                            SubjectImage[] temp = new SubjectImage[4];
                            char[] nums = { '1', '2', '3', '4'};

                            for (int j = 0; j < temp.Length; j++)
                            {
                                string title = nameReader.ReadLine();
                                int index = title.IndexOfAny(nums);
                                int number = Convert.ToInt32(title[index]) - 48;

                                string[] pointString = pointReader.ReadLine().Split(' ');
                                int x = Convert.ToInt32(pointString[0]);
                                int y = Convert.ToInt32(pointString[1]);
                                int w = Convert.ToInt32(pointString[2]);
                                int h = Convert.ToInt32(pointString[3]);

                                Rectangle rect = new Rectangle(x, y, w, h);

                                temp[number - 1] = new SubjectImage(title, rect, spriteSheet);
                            }

                            images[i] = temp;
                        }    
                    }
                }
            }
            catch(Exception e)
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
            KeyboardState kb = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || kb.IsKeyDown(Keys.Escape))
                this.Exit();

            if(kb.IsKeyDown(Keys.Space) && !oldKb.IsKeyDown(Keys.Space))
            {
                int temp = (int)state;
                temp++;
                temp %= 4;
                state = (SubjectState)temp;
            }

            if (kb.IsKeyDown(Keys.Enter) && !oldKb.IsKeyDown(Keys.Enter))
            {
                int index = (int)state;
                subjectIsFlipped[index] = !subjectIsFlipped[index];
            }

            oldKb = kb;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            int x = 100;
            int subjectIndex = (int)state;

            spriteBatch.Begin();
            
            if(subjectIsFlipped[subjectIndex])
            {
                for (int i = images[subjectIndex].Length - 1; i >= 0; i--)
                {
                    Rectangle temp = images[subjectIndex][i].rect;
                    Rectangle destination = new Rectangle(x, 350 - (temp.Height / 2), temp.Width, temp.Height);
                    images[subjectIndex][i].Draw(spriteBatch, destination, font);
                    x += temp.Width + 150;

                }
            }
            
            else
            {
                for (int i = 0; i < images[subjectIndex].Length; i++)
                {
                    Rectangle temp = images[subjectIndex][i].rect;
                    Rectangle destination = new Rectangle(x, 350 - (temp.Height / 2), temp.Width, temp.Height);
                    images[subjectIndex][i].Draw(spriteBatch, destination, font);
                    x += temp.Width + 150;
                }
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
