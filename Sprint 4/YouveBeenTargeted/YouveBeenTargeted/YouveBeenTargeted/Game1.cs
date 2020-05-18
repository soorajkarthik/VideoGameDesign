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

namespace YouveBeenTargeted
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D tankTexture;
        Texture2D projectileTexture;

        Rectangle tankFrame;
        Rectangle projectileFrame;

        Vector2 centerOfTank;

        double angle;
        double x;
        double y;
        double dx;
        double dy;
        int preferredSpeed;

        MouseState oldMouse;

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

            this.IsMouseVisible = true;
            graphics.PreferredBackBufferHeight = 800;
            graphics.ApplyChanges();

            tankFrame = new Rectangle(400, 400, 200, 200);
            projectileFrame = new Rectangle(1000, 1000, 20, 20);

            centerOfTank = new Vector2(115, 100);

            angle = 0;
            x = 1000;
            y = 1000;
            dx = 0;
            dy = 0;
            preferredSpeed = 5;

            oldMouse = Mouse.GetState();

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

            tankTexture = Content.Load<Texture2D>("Tank");
            projectileTexture = Content.Load<Texture2D>("White Square");

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

            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();

            if (keyboard.IsKeyDown(Keys.Escape))
                this.Exit();

            angle = Math.Atan2(mouse.Y - 400, mouse.X - 400) + MathHelper.ToRadians(90);

            if(mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton != ButtonState.Pressed)
            {
                x = 400;
                y = 400;
                int changeX = mouse.X - 400;
                int changeY = mouse.Y - 400;
                double hyp = Math.Sqrt(changeX * changeX + changeY * changeY);
                int numUpdates = (int)(hyp / preferredSpeed);
                dx = (double)changeX / numUpdates;
                dy = (double)changeY / numUpdates;

            }

            x += dx;
            y += dy;

            projectileFrame.X = (int)x;
            projectileFrame.Y = (int)y;


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();

            spriteBatch.Draw(projectileTexture, projectileFrame, Color.Black);
            spriteBatch.Draw(tankTexture, tankFrame, null, Color.White, (float)angle, centerOfTank, SpriteEffects.None, 0f);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
