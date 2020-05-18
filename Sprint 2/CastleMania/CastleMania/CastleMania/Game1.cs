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
 * 9 Texture2D, one for each picture
 * 7 Rectagle Frames: background, carriage, king, archer, knight, herald, arrow
 * int tics
 * 
 * in initialize:
 * initialize all 7 rectangles to correct size
 * set all initial positions off the screen except the herald
 * 
 * in loadcontent:
 * load all 9 of the textures using the images
 * 
 * in update:
 * ALL OF THESE TIC VALUES ARE "GUESSTIMATIONS"
 * for first 120 tics, make herald walk into frame from left
 * between 120 and 240 tics, make herald stop moving
 * between 240 and 360 tics, make herald move out of frame to the left again
 * between 360 and 540 tics, make carriage come into the scene
 * at 570 tics, make king get out of carriage
 * between 660 and 780 tics, make archer walk in from the right
 * at 840 tics, make arrow come from archer's bow
 * between 840 and 900 tics, make arrow travel across the screen and hit king
 * at 910 tics, change king's texture change to the dead king texture
 * between 960 and 1320 tics, make knight move across screen to the right
 * when knight gets across screen, make him run into archer, and then leave the screen
 * make archer's texture the dead archer texture
 * THE END
 * 
 * in draw:
 * draw all components during the time intervals they appear on the screen
 * between 100 and 400 tics, increase the amount of black the scene is multiplied by when its drawn
 * between 400 and 800 tics, decrease the amount
 * 
 * 
 */
namespace CastleMania
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;

        int tics;
        float darknessMulitplier;

        Texture2D archerTexture;
        Texture2D arrowTexture;
        Texture2D carriageTexture;
        Texture2D deadArcherTexture;
        Texture2D deadKingTexture;
        Texture2D heraldTexture;
        Texture2D kingTexture;
        Texture2D knightTexture;
        Texture2D sceneTexture;
        Texture2D personTexture;

        Rectangle archerFrame;
        Rectangle arrowFrame;
        Rectangle carriageFrame;
        Rectangle kingFrame;
        Rectangle heraldFrame;
        Rectangle knightFrame;
        Rectangle sceneFrame;
        Rectangle person1Frame;
        Rectangle person2Frame;

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

            tics = 0;
            darknessMulitplier = 1f;

            archerFrame = new Rectangle(801, 300, 125, 159);
            arrowFrame = new Rectangle(1000, 357, 60, 30);
            carriageFrame = new Rectangle(-269, 300, 270, 159);
            kingFrame = new Rectangle(1000, 300, 139, 159);
            heraldFrame = new Rectangle(-154, 300, 154, 159);
            knightFrame = new Rectangle(-181, 280, 181, 179);
            sceneFrame = new Rectangle(0, 0, 800, 480);
            person1Frame = new Rectangle(200, 300, 86, 159);
            person2Frame = new Rectangle(300, 300, 86, 159);
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
            archerTexture = Content.Load<Texture2D>("archer");
            arrowTexture = Content.Load<Texture2D>("arrow");
            carriageTexture = Content.Load<Texture2D>("carriage");
            deadArcherTexture = Content.Load<Texture2D>("deadArcher");
            deadKingTexture = Content.Load<Texture2D>("deadKing");
            heraldTexture = Content.Load<Texture2D>("herald");
            kingTexture = Content.Load<Texture2D>("king");
            knightTexture = Content.Load<Texture2D>("knight");
            sceneTexture = Content.Load<Texture2D>("scene");
            personTexture = Content.Load<Texture2D>("person");

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
            int seconds = tics / 60;
            if (tics < 154)
                heraldFrame.X++;

            if (tics > 300 && tics < 480)
            {
                heraldFrame.X--;
                person1Frame.X += 2;
                person2Frame.X += 2;
            }
                

            if (tics > 480 && tics < 749)
            {
                carriageFrame.X++;
                person1Frame.X += 2;
                person2Frame.X += 2;
            }
                

            if (tics == 810)
                kingFrame.X = 50;

            if (tics > 840 && tics < 900)
                kingFrame.X++;

            if (tics > 900 && tics < 1029)
            {
                kingFrame.X++;
                archerFrame.X--;
            }

            if (tics == 1050)
                arrowFrame.X = 671;

            if (tics > 1050 && tics < 1145)
                arrowFrame.X -= 4;

            if(tics == 1160)
            {
                kingTexture = deadKingTexture;
                kingFrame = new Rectangle(229, 300, 159, 139);
                arrowFrame.X = 800;
            }

            if (tics > 1160 && tics < 1185)
                kingFrame.Y += 2;

            if(tics > 1200 && tics < 1580)
            {
                if (tics % 60 == 0)
                    knightFrame.Width = 181;

                else if (tics % 30 == 0)
                    knightFrame.Width = 0;

                knightFrame.X += 2;
                darknessMulitplier -= 0.0025f;
            }

            if(tics == 1620)
            {
                archerTexture = deadArcherTexture;
                archerFrame = new Rectangle(671, 350, 159, 129);
            }

            if(tics > 1640 && tics < 2020)
            {
                knightFrame.X++;
                darknessMulitplier += 0.0025f;
            }


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            spriteBatch.Draw(sceneTexture, sceneFrame, Color.White * darknessMulitplier);
            spriteBatch.Draw(heraldTexture, heraldFrame, Color.White * darknessMulitplier);
            spriteBatch.Draw(carriageTexture, carriageFrame, Color.White * darknessMulitplier);
            spriteBatch.Draw(kingTexture, kingFrame, Color.White * darknessMulitplier);
            spriteBatch.Draw(archerTexture, archerFrame, Color.White * darknessMulitplier);
            spriteBatch.Draw(arrowTexture, arrowFrame, Color.White * darknessMulitplier);
            spriteBatch.Draw(knightTexture, knightFrame, Color.White * darknessMulitplier);
            spriteBatch.Draw(personTexture, person1Frame, Color.White * darknessMulitplier);
            spriteBatch.Draw(personTexture, person2Frame, Color.White * darknessMulitplier);

            if (tics > 180 && tics < 300)
                spriteBatch.DrawString(font, "Make way for the king!", new Vector2(20, 270), Color.Black);

            if (tics > 1800)
                spriteBatch.DrawString(font, "The valant ghost knight slayed the evil archer \nUnfortunately he was 460 tics too late :(\nThe End", new Vector2(90, 150), Color.Black);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
