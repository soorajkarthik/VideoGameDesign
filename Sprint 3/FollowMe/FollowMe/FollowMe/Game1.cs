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

namespace FollowMe
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D standing;
        Texture2D walking;
        Texture2D current;
        Rectangle frame;
        int clickX;
        int xAdd;
        double y;
        double yAdd;


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
            frame = new Rectangle(300, 100, 54, 200);
            clickX = frame.X;
            xAdd = 0;
            y = frame.Y;
            yAdd = 0;
            this.IsMouseVisible = true;
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
            standing = Content.Load<Texture2D>("Standing Man");
            walking = Content.Load<Texture2D>("Walking Man");
            current = standing;
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

            current = standing;
            MouseState currState = Mouse.GetState();

            if(currState.LeftButton == ButtonState.Pressed)
            {
                clickX = currState.X;
                xAdd = frame.X < clickX ? 1 : -1;
                if (currState.X != frame.X)
                    yAdd = ((double)currState.Y - frame.Y)/(Math.Abs(currState.X - frame.X));
            
            }

            if(frame.X != clickX)
            {
                y += yAdd;
                frame.X += xAdd;
                frame.Y = (int)y;
                current = walking;
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
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here

            spriteBatch.Begin();
            spriteBatch.Draw(current, frame, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
