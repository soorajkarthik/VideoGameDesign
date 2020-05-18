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
 * 2 Texture2D, one for pig on left and one for pig on right
 * 2 Rectangles, one for each pig
 * 3 strings
 * int tics
 * 
 * in initialize:
 * initialize rectangles 
 * initialize strings with values in english
 * 
 * in loadcontent:
 * initialize the textures
 * 
 * in update:
 * increment tics
 * 
 * in draw:
 * find number of seconds by dividing tics by 60
 * display english text for 2 seconds then display the translation 2 seconds later
 * repeat this for each string
 * 
 * in translate:
 * split string on all of the spaces and store in string array
 * loop through string array
 * if string starts with vowel, add "way" to the word
 * else substring the word and dont include the first letter and then add first letter to end and then add "way"
 * as you go, add all modified strigns to a resultant string
 * return resultant string 
 */

namespace RomanPigs
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        Rectangle leftFrame;
        Rectangle rightFrame;
        Texture2D leftTexture;
        Texture2D rightTexture;
        int tics;
        String firstString;
        String secondString;
        String thirdString;

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
            leftFrame = new Rectangle(100, 200, 205, 280);
            rightFrame = new Rectangle(495, 200, 205, 280);
            tics = 0;
            firstString = "My name is Sooraj";
            secondString = "I am a senior";
            thirdString = "I like coding";
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
            leftTexture = Content.Load<Texture2D>("left");
            rightTexture = Content.Load<Texture2D>("right");
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

            // TODO: Add your update logic here
            tics++;
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
            int switches = tics / 180;
            Vector2 englishPosition = new Vector2(100, 180);
            Vector2 pigLatinPosition = new Vector2(450, 180);

            spriteBatch.Begin();

            spriteBatch.Draw(leftTexture, leftFrame, Color.White);
            spriteBatch.Draw(rightTexture, rightFrame, Color.White);

            switch (switches)
            {
                case 0:
                    spriteBatch.DrawString(font, firstString, englishPosition, Color.Black);
                    break;
                case 1:
                    spriteBatch.DrawString(font, Translate(firstString), pigLatinPosition, Color.Black);
                    break;
                case 2:
                    spriteBatch.DrawString(font, secondString, englishPosition, Color.Black);
                    break;
                case 3:
                    spriteBatch.DrawString(font, Translate(secondString), pigLatinPosition, Color.Black);
                    break;
                case 4:
                    spriteBatch.DrawString(font, thirdString, englishPosition, Color.Black);
                    break;
                case 5:
                    spriteBatch.DrawString(font, Translate(thirdString), pigLatinPosition, Color.Black);
                    break;

            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public String Translate(String input)
        {
            String vowels = "aeiouAEIOU";
            String res = "";
            String[] words = input.Split(' ');

            for(int i = 0; i < words.Length; i++)
            {
                if (vowels.Contains(words[i].Substring(0, 1)))
                    res += words[i] + "way ";
                else
                    res += words[i].Substring(1) + words[i].Substring(0, 1) + "ay ";
            }

            return res;
        }
    }
}
