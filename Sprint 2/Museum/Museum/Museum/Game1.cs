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
 * Texture2D armDown
 * Texture2D armUp
 * Texture2D painting
 * Texture2D museum
 * int tics;
 * 
 * have rectangles for all frames
 * 
 * in initialize:
 * initialize all rectangles
 * 
 * in draw: 
 * draw 2 armsDown images with a painting between
 * when x get to a certain value ex. 300, switch arms down to arms up and move painting up
 * wait a few seconds
 * then switch arms up to arms down and start moving arms down again
 * 
 * in update:
 * count tics,
 * if tics < 300, x++
 * if tics are between 300 and 420, stop increasing x
 * if tics > 420, increase x, stop increasing x of painting 
 * 
 */

namespace Museum
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D armDownTexture;
        Texture2D armUpTexture;
        Texture2D peopleTexture;
        Texture2D paintingTexture;
        Texture2D museumTexture;

        Rectangle person1;
        Rectangle person2;
        Rectangle museum;
        Rectangle painting;

        int tics;

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

            person1 = new Rectangle(70, 370, 98, 164);
            person2 = new Rectangle(0, 370, 98, 164);
            painting = new Rectangle(20, person1.Y - 90, 105, 90);
            museum = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
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

            armDownTexture = Content.Load<Texture2D>("handsDown");
            armUpTexture = Content.Load<Texture2D>("handsUp");
            museumTexture = Content.Load<Texture2D>("museum");
            paintingTexture = Content.Load<Texture2D>("painting");
            peopleTexture = armUpTexture;
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
            tics++;

            if(tics < 400)
            {
                person1.X++;
                person2.X++;
                painting.X++;
            }

            else if (tics < 520)
            {
                painting.Y--;
            }
            else if (tics == 520)
                peopleTexture = armDownTexture;
            

            else if(tics > 520)
            {
                person1.X++;
                person2.X++;
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
            spriteBatch.Draw(museumTexture, museum, Color.White);
            spriteBatch.Draw(paintingTexture, painting, Color.White);
            spriteBatch.Draw(peopleTexture, person1, Color.White);
            spriteBatch.Draw(peopleTexture, person2, Color.White);
            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
