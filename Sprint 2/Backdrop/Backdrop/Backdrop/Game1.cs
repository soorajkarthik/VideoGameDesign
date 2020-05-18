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

/*
 * Pseudocode:
 * 
 * Color c = red
 * int timer = 0
 * 
 * in update:
 * timer++
 * 
 * in draw:
 * color switches = (sec/180) % 5
 * 
 * switch {
 * 
 * case 0: change color
 * case 1:...
 * ..
 * case 5:...
 */

namespace Backdrop
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Color backgroundColor;
        int timer;

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
            backgroundColor = Color.Red;
            timer = 0;
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
            timer++;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            int colorNum = (timer / 180) % 5;
            
            switch(colorNum)
            {
                case 0:
                    GraphicsDevice.Clear(Color.Red);
                    break;
                case 1:
                    GraphicsDevice.Clear(Color.CornflowerBlue);
                    break;
                case 2:
                    GraphicsDevice.Clear(Color.DarkOliveGreen);
                    break;
                case 3:
                    GraphicsDevice.Clear(Color.DarkViolet);
                    break;
                case 4:
                    GraphicsDevice.Clear(Color.YellowGreen);
                    break;
                case 5:
                    GraphicsDevice.Clear(Color.Orange);
                    break;


            }


            base.Draw(gameTime);
        }
    }
}
