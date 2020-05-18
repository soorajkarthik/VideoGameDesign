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

namespace RocketMan
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;

        Texture2D shipTexture;
        Rectangle shipFrame;

        double angle;
        double velocity;


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

            angle = 0;
            velocity = 0;
            shipFrame = new Rectangle(325, 190, 150, 100);
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
            shipTexture = Content.Load<Texture2D>("ship");
            font = Content.Load<SpriteFont>("SpriteFont1");

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

            GamePadState state = GamePad.GetState(PlayerIndex.One);

            if (!(state.ThumbSticks.Right.X == 0 && state.ThumbSticks.Right.Y == 0))
            {
                angle = Math.Atan2(state.ThumbSticks.Right.Y, state.ThumbSticks.Right.X);
                angle = (-MathHelper.ToDegrees((float)angle) + 450) % 360;
            }

            velocity += state.ThumbSticks.Left.Y;

            if (velocity > 20)
                velocity = 20;
            if (velocity < 0)
                velocity = 0;

            shipFrame.X += (int)(velocity * Math.Cos(MathHelper.ToRadians((float)(-angle+90)))) + 800;
            shipFrame.Y -= (int)(velocity * Math.Sin(MathHelper.ToRadians((float)(-angle+90)))) - 800;

            shipFrame.X %= 800;
            shipFrame.Y %= 800;



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

            
            spriteBatch.DrawString(font, "Velocity: " + (int)velocity, new Vector2(10, 10), Color.Black);
            spriteBatch.DrawString(font, "Angle   : " + (int)angle, new Vector2(10, 40), Color.Black);
            spriteBatch.Draw(shipTexture, shipFrame, Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
