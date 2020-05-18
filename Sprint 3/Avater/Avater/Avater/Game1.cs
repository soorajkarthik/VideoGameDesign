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

namespace Avater
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        GamePadState oldState;

        Texture2D[] textures;
        Rectangle[] frames;
        Texture2D selectorTexture;
        Rectangle selectorFrame;
        Rectangle largeFrame;
        int index;
        bool isSelected; 

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
            frames = new Rectangle[] 
            {
                new Rectangle(0, 120, 160, 160),
                new Rectangle(160, 120,160, 160),
                new Rectangle(320, 120, 160, 160),
                new Rectangle(480, 120, 160, 160),
                new Rectangle(640, 120, 160, 160)
            };

            selectorFrame = new Rectangle(0, 100, 160, 200);
            largeFrame = new Rectangle(200, 20, 400, 400);
            index = 0;
            isSelected = false;

            oldState = GamePad.GetState(PlayerIndex.One);           

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
            textures = new Texture2D[]
            {
                Content.Load<Texture2D>("bearded man"),
                Content.Load<Texture2D>("bishop"),
                Content.Load<Texture2D>("cop"),
                Content.Load<Texture2D>("glasses"),
                Content.Load<Texture2D>("snow boy")
            };

            selectorTexture = Content.Load<Texture2D>("empty");

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
            
            GamePadState currentState = GamePad.GetState(PlayerIndex.One);

            if (currentState.IsButtonDown(Buttons.DPadLeft) && !oldState.IsButtonDown(Buttons.DPadLeft))
            {
                index += 4;
                index %= 5;

                selectorFrame.X += 640;
                selectorFrame.X %= 800;

            }

            else if (currentState.IsButtonDown(Buttons.DPadRight) && !oldState.IsButtonDown(Buttons.DPadRight))
            {
                index++;
                index %= 5;

                selectorFrame.X += 160;
                selectorFrame.X %= 800;
            }

            else if (currentState.IsButtonDown(Buttons.Start))
                isSelected = true;

            else if (currentState.IsButtonDown(Buttons.Back))
                isSelected = false;

            oldState = currentState;

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

            if(isSelected)
                spriteBatch.Draw(textures[index], largeFrame, Color.White);
            
            else
            {
                spriteBatch.Draw(selectorTexture, selectorFrame, Color.White);

                for (int i = 0; i < textures.Length; i++)
                    spriteBatch.Draw(textures[i], frames[i], Color.White);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
