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

namespace MoreBorg
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D turretTexture;
        Texture2D blankTexture;
        Texture2D borgTexture;
        Texture2D emptyBarTexture;

        SpriteFont font;

        Rectangle turretFrame;
        Rectangle[] tubeFrames;
        Rectangle LSUChargeBar;
        Rectangle powerBar;
        Rectangle propulsionBar;

        Torpedo torpedo;
        List<Borg> borgs;
        List<BorgBullet> borgBullets;

        Random random;

        int index;
        bool canFire;

        int LSUCharge;
        int power;
        int propulsion;
        int tics;

        KeyboardState oldState;

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

            index = 0;
            canFire = true;
            oldState = Keyboard.GetState();
            LSUCharge = 0;
            power = 1;
            propulsion = 1;

            turretFrame = new Rectangle(300, 140, 200, 200);
            tubeFrames = new Rectangle[]
            {
                new Rectangle(392, 110, 20, 40),
                new Rectangle(490, 230, 40, 20),
                new Rectangle(392, 330, 20, 40),
                new Rectangle(270, 230, 40, 20)
            };

            torpedo = new Torpedo(4, 0, 0);           

            borgs = new List<Borg>();
            borgBullets = new List<BorgBullet>();
            random = new Random();

            LSUChargeBar = new Rectangle(70, 12, 100, 20);
            powerBar = new Rectangle(70, 42, 100, 20);
            propulsionBar = new Rectangle(70, 72, 100, 20);


            IsMouseVisible = true;
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
            turretTexture = Content.Load<Texture2D>("turret");
            blankTexture = Content.Load<Texture2D>("empty");
            borgTexture = Content.Load<Texture2D>("borg");
            emptyBarTexture = Content.Load<Texture2D>("emptyBar");
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

            KeyboardState currState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();


            double angle = Math.Atan2((mouseState.Y - 240), (mouseState.X - 400));
            angle = (MathHelper.ToDegrees((float)angle) + 360) % 360;

            if (angle > 315)
                index = 1;
            else if (angle > 225)
                index = 0;
            else if (angle > 135)
                index = 3;
            else if (angle > 45)
                index = 2;
            else
                index = 1;


            if (currState.IsKeyDown(Keys.Space) && canFire)
            {
                canFire = false;
                

                if (power + propulsion > LSUCharge)
                {
                    torpedo = new Torpedo(index, LSUCharge/2, LSUCharge/2);
                    LSUCharge = 0;
                }

                else
                {
                    torpedo = new Torpedo(index, power, propulsion);
                    LSUCharge -= power;
                    LSUCharge -= propulsion;
                }

            }

            for (int i = 0; i < currState.GetPressedKeys().Length; i++)
            {
                int key = (int)currState.GetPressedKeys()[i];
                if (key >= 97 && key <= 105)
                    power = key - 96;

                if (key >= 49 && key <= 57)
                    propulsion = key - 48;
            }

            torpedo.Move();
            canFire = torpedo.IsOffScreen();

            if (tics % 60 == 0)
                LSUCharge += LSUCharge + 3 < 100 ? 3 : 100 - LSUCharge;

            if (random.NextDouble() < 0.0167)
            {
                int direction = random.Next(4);

                switch (direction)
                {
                    case 0:
                        borgs.Add(new Borg(0));
                        break;

                    case 1:
                        borgs.Add(new Borg(1));
                        break;

                    case 2:
                        borgs.Add(new Borg(2));
                        break;

                    case 3:
                        borgs.Add(new Borg(3));
                        break;
                }
            }

            for (int i = 0; i < borgs.Count; i++)
            {
                Borg temp = borgs.ElementAt(i);

                if (temp.frame.Intersects(torpedo.frame) || temp.timeVisible == 0)
                {
                    borgs.RemoveAt(i);
                    i--;
                    continue;
                }

                borgs.ElementAt(i).DecrementTime();

                if (temp.shotTime == 0)
                    borgBullets.Add(new BorgBullet(temp.GetShotRectangle(), temp.side));
            }

            for (int i = 0; i < borgBullets.Count; i++)
            {
                borgBullets.ElementAt(i).Move();
                if (borgBullets.ElementAt(i).frame.Intersects(turretFrame))
                {
                    borgBullets.RemoveAt(i);
                    i--;
                }
            }

            oldState = currState;
            tics++;

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

            spriteBatch.Draw(turretTexture, turretFrame, Color.White);

            for (int i = 0; i < tubeFrames.Length; i++)
            {
                if (i == index)
                    spriteBatch.Draw(blankTexture, tubeFrames[i], canFire ? Color.Green : Color.DarkRed);
                else
                    spriteBatch.Draw(blankTexture, tubeFrames[i], Color.Gray);
            }

            for (int i = 0; i < borgs.Count; i++)
                spriteBatch.Draw(borgTexture, borgs.ElementAt(i).frame, Color.White);

            for (int i = 0; i < borgBullets.Count; i++)
                spriteBatch.Draw(blankTexture, borgBullets.ElementAt(i).frame, Color.Red);

            spriteBatch.Draw(blankTexture, torpedo.frame, Color.DarkBlue);
            spriteBatch.DrawString(font, "LSU: ", new Vector2(10, 10), Color.White);
            spriteBatch.DrawString(font, "Pow: ", new Vector2(10, 40), Color.White);
            spriteBatch.DrawString(font, "Prp: ", new Vector2(10, 70), Color.White);

            spriteBatch.Draw(emptyBarTexture, LSUChargeBar, Color.White);
            spriteBatch.Draw(emptyBarTexture, powerBar, Color.White);
            spriteBatch.Draw(emptyBarTexture, propulsionBar, Color.White);

            spriteBatch.Draw(blankTexture, new Rectangle(70, 12, LSUCharge, 20), Color.White);
            spriteBatch.Draw(blankTexture, new Rectangle(70, 42, (power * 11), 20), Color.White);
            spriteBatch.Draw(blankTexture, new Rectangle(70, 72, (propulsion * 11), 20), Color.White);


            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
