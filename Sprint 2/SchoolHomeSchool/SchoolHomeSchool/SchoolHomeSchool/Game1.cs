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
 * 6 Rectangles, 3 for images, 3 for border
 * array of textures
 * array of colors
 * array of strings
 * int textureIndex
 * int colorIndex
 * int tics
 * 
 * in initialize:
 * set location and size of rectangles
 * initialize array colors
 * 
 * in loadcontent:
 * initialize array of textures
 * 
 * in update:
 * tics++
 * 
 * if(tics % 420 == 0)
 * textureIndex++
 * textureIndex % 3
 * 
 * if(tics % 240 == 0)
 * colorsIndex-- (to make the colors go clockwise)
 * colorIndex % 3
 * 
 * in draw:
 * draw 3 times
 * draw(texture[textureIndex], left, Color.white)
 * draw(texture[(textureIndex+1)%3), middle, color.white)
 * ...
 * 
 * same idea with the border rectangles, except increment and mod color index
 * do the same thing when drawing the titles except increment both textureIndex AND colorIndex
 * 
 * 
 */

namespace SchoolHomeSchool
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        Rectangle left;
        Rectangle middle;
        Rectangle right;
        Rectangle leftBorder;
        Rectangle middleBorder;
        Rectangle rightBorder;
        int tics;
        int colorIndex;
        int textureIndex;
        Color[] colors;
        String[] titles;
        Texture2D[] textures;
        Texture2D emptyTexture;



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
            left = new Rectangle(100, 100, 150, 150);
            leftBorder = new Rectangle(98, 98, 154, 154);

            middle = new Rectangle(300, 300, 150, 150);
            middleBorder = new Rectangle(298, 298, 154, 154);

            right = new Rectangle(500, 100, 150, 150);
            rightBorder = new Rectangle(498, 98, 154, 154);

            colors = new Color[] { Color.Red, Color.Blue, Color.Gold };
            titles = new String[] { "Classroom slide", "Home slide", "School Slide" };

            tics = 0;
            colorIndex = 0;
            textureIndex = 0;

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
            emptyTexture = Content.Load<Texture2D>("empty");
            textures = new Texture2D[] { Content.Load<Texture2D>("classroom"), Content.Load<Texture2D>("home"), Content.Load<Texture2D>("school") };

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
            if (tics % 420 == 0)
            {
                textureIndex += 2;
                textureIndex %= 3;
            }

            if (tics % 240 == 0)
            {
                colorIndex++;
                colorIndex %= 3;
                Console.WriteLine(colorIndex);
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

            spriteBatch.Draw(emptyTexture, leftBorder, colors[colorIndex]);
            spriteBatch.Draw(emptyTexture, middleBorder, colors[(colorIndex + 1) % 3]);
            spriteBatch.Draw(emptyTexture, rightBorder, colors[(colorIndex + 2) % 3]);

            spriteBatch.Draw(textures[textureIndex], left, Color.White);
            spriteBatch.Draw(textures[(textureIndex + 1) % 3], middle, Color.White);
            spriteBatch.Draw(textures[(textureIndex + 2) % 3], right, Color.White);

            spriteBatch.DrawString(font, titles[textureIndex], new Vector2(100, 260), colors[colorIndex]);
            spriteBatch.DrawString(font, titles[(textureIndex + 1) % 3], new Vector2(300, 460), colors[(colorIndex + 1) % 3]);
            spriteBatch.DrawString(font, titles[(textureIndex + 2) % 3], new Vector2(500, 260), colors[(colorIndex + 2) % 3]);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
