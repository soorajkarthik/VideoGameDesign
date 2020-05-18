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

namespace BeamMeUp
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;

        Dictionary<Keys, SoundEffect> effects;    

        Texture2D trekTexture;
        Rectangle trekFrame;

        long tics;

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

            trekFrame = new Rectangle(200, 10, 400, 225);
            effects = new Dictionary<Keys, SoundEffect>();
            tics = 0;
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
            trekTexture = Content.Load<Texture2D>("startrek");

            effects[Keys.NumPad1] = Content.Load<SoundEffect>("alert10");
            effects[Keys.NumPad2] = Content.Load<SoundEffect>("autodestructsequencearmed_ep");
            effects[Keys.NumPad3] = Content.Load<SoundEffect>("borg_cut_clean");
            effects[Keys.NumPad4] = Content.Load<SoundEffect>("commandcodesverified_ep");
            effects[Keys.NumPad5] = Content.Load<SoundEffect>("ds9_door");
            effects[Keys.NumPad6] = Content.Load<SoundEffect>("klingon_torpedo_clean");
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

            KeyboardState currentState = Keyboard.GetState();
            Keys[] currentKeys = currentState.GetPressedKeys();

            if (tics < -20)
            {
                for (int i = 0; i < currentKeys.Length; i++)
                {
                    effects[currentKeys[i]].Play();
                    tics = (long)(effects[currentKeys[i]].Duration.TotalSeconds * 60);
                    Console.WriteLine(tics);
                    break;
                }
            }

            tics--;
            

           
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            spriteBatch.Draw(trekTexture, trekFrame, Color.White);
            spriteBatch.DrawString(font, "Alert -             NumPad1", new Vector2(200, 250), Color.Black);
            spriteBatch.DrawString(font, "Destruct Sequence - NumPad2", new Vector2(200, 270), Color.Black);
            spriteBatch.DrawString(font, "Borg -              NumPad3", new Vector2(200, 290), Color.Black);
            spriteBatch.DrawString(font, "Code Verified -     NumPad4", new Vector2(200, 310), Color.Black);
            spriteBatch.DrawString(font, "Door -              NumPad5", new Vector2(200, 330), Color.Black);
            spriteBatch.DrawString(font, "Torpedo -           NumPad6", new Vector2(200, 350), Color.Black);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
