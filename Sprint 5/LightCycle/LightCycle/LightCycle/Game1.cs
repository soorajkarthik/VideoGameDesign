using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace LifeCycle
{
  
    public class Game1 : Microsoft.Xna.Framework.Game
    {

        GraphicsDeviceManager graphics;
        SpriteBatch sb;

        Random r;
        Bike blue, orange;
        Texture2D white;
        List<Trail> trails;

        int tics;              
        SpriteFont font;     

        enum State
        {
            START,
            COUNTDOWN,
            PLAYING,
            BLUE_WIN,
            ORANGE_WIN
        }

        State gameState;

  
        KeyboardState oldkb;
        Keys[] orangeInputs;
        Keys[] blueInputs;

        
        public Game1()
        {
           
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1080;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();
        }

        protected override void Initialize()
        {

            r = new Random();
            tics = 0;
            oldkb = Keyboard.GetState();
            gameState = State.START;

            orangeInputs = new Keys[] { Keys.D, Keys.S, Keys.A, Keys.W };
            blueInputs = new Keys[] { Keys.Right, Keys.Down, Keys.Left, Keys.Up };

            base.Initialize();
        }

        protected override void LoadContent()
        {

            sb = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("SpriteFont1");
            white = Content.Load<Texture2D>("w");
            InitBikes();
        }

        protected void InitBikes()
        {
            int side = r.Next(4);

            Rectangle temp = GetRandomRect(side);

            blue = new Bike(Content.Load<Texture2D>("blue"), Content.Load<Texture2D>("b"), temp, 90 * (side + 1));

            if (side % 2 == 0)
                temp.Y = 720 - temp.Y;
            else
                temp.X = 1080 - temp.X;
             
            orange = new Bike(Content.Load<Texture2D>("orange"), Content.Load<Texture2D>("o"), temp, 90 * (side + 3));

            trails = new List<Trail>();
            trails.Add(blue.currentTrail);
            trails.Add(orange.currentTrail);
            trails.Add(new Trail(white, new Rectangle(-5, -5, 10, 730)));    
            trails.Add(new Trail(white, new Rectangle(1075, -5, 10, 730))); 
            trails.Add(new Trail(white, new Rectangle(-5, -5, 1090, 10)));    
            trails.Add(new Trail(white, new Rectangle(-5, 715, 1090, 10)));

            tics = 3 * 60;
        }

        
        protected Rectangle GetRandomRect(int side)
        {

            int x = 0, y = 0;

            side %= 4;

            if (side % 2 == 0)
            {
                y = side == 0 ? 23 : 697;
                x = r.Next(998) + 41;
            }

            else
            {
                x = side == 3 ? 23 : 1057;
                y = r.Next(686) + 17;
            }

            return new Rectangle(x, y, 36, 12);
        }

 
        protected override void UnloadContent()
        {

        }
        
        protected override void Update(GameTime gameTime)
        {
            KeyboardState kb = Keyboard.GetState();

            if (kb.IsKeyDown(Keys.Escape))
                this.Exit();


            switch (gameState)
            {
                case State.START:     
                    if (kb.IsKeyDown(Keys.Space) && oldkb.IsKeyUp(Keys.Space))
                        gameState = State.COUNTDOWN;
                    
                    break;

                case State.COUNTDOWN:       
                    tics--;
                    if (tics == 0)
                    
                        gameState = State.PLAYING;
                    

                    break;

                case State.PLAYING:

                    if (kb.IsKeyDown(Keys.A) && !oldkb.IsKeyDown(Keys.A))
                    {
                        orange.Turn((orange.direction + 270) % 360);
                        trails.Add(orange.currentTrail);
                    }

                    if (kb.IsKeyDown(Keys.D) && !oldkb.IsKeyDown(Keys.D))
                    {
                        orange.Turn((orange.direction + 90) % 360);
                        trails.Add(orange.currentTrail);
                    }


                    if (kb.IsKeyDown(Keys.Left) && !oldkb.IsKeyDown(Keys.Left))
                    {
                        blue.Turn((blue.direction + 270) % 360);
                        trails.Add(blue.currentTrail);
                    }

                    if (kb.IsKeyDown(Keys.Right) && !oldkb.IsKeyDown(Keys.Right))
                    {
                        blue.Turn((blue.direction + 90) % 360);
                        trails.Add(blue.currentTrail);
                    }

                    //for (int i = 0; i < blueInputs.Length; i++)
                    //{
                    //    if (kb.IsKeyDown(orangeInputs[i]) && oldkb.IsKeyUp(orangeInputs[i]) && orange.direction % 180 == (i % 2 == 0 ? 90 : 0))
                    //    {
                    //        orange.Turn(90 * i);
                    //        trails.Add(orange.currentTrail);
                    //    }

                    //    if (kb.IsKeyDown(blueInputs[i]) && oldkb.IsKeyUp(blueInputs[i]) && blue.direction % 180 == (i % 2 == 0 ? 90 : 0))
                    //    {
                    //        blue.Turn(90 * i);
                    //        trails.Add(blue.currentTrail);
                    //    }
                    //}


                    blue.Update();
                    orange.Update();

                   
                    foreach (Trail trail in trails)
                    {
                        if (trail != blue.currentTrail && trail.rect.Intersects(blue.rectCollision))
                            gameState = State.ORANGE_WIN;
                        else if (trail != orange.currentTrail && trail.rect.Intersects(orange.rectCollision))
                            gameState = State.BLUE_WIN;
                    } 
                    if (blue.rectCollision.Intersects(orange.rect))
                        gameState = State.ORANGE_WIN;
                    if (orange.rectCollision.Intersects(blue.rect))
                        gameState = State.BLUE_WIN;

                    break;

                case State.BLUE_WIN:
                case State.ORANGE_WIN:
                    if (kb.IsKeyDown(Keys.Space) && oldkb.IsKeyUp(Keys.Space))
                    {
                        gameState = State.COUNTDOWN;
                        InitBikes();
                    }

                    break;
            }

            oldkb = kb;
            base.Update(gameTime);
        }

     
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            sb.Begin();

            if (gameState != State.START)
            {
                foreach (Trail trail in trails)
                    trail.Draw(sb, gameTime);

                blue.Draw(sb, gameTime);
                orange.Draw(sb, gameTime);
            }

            switch (gameState)
            { 
                case State.START:       
                    sb.DrawString(font, "         TRON        ", new Vector2(330, 260), Color.White);
                    sb.DrawString(font, " Press Space to Start", new Vector2(330, 300), Color.White);
                    sb.DrawString(font, "   Orange use A & D  ", new Vector2(330, 340), Color.White);
                    sb.DrawString(font, "Blue use Left & Right", new Vector2(330, 380), Color.White);
                    break;

                case State.COUNTDOWN:
                    sb.DrawString(font, (tics / 60 + 1) + "", new Vector2(537, 347), Color.White);
                    break;

                case State.PLAYING:
                    break;

                case State.BLUE_WIN:
                    sb.DrawString(font, "        Blue Wins!        ", new Vector2(322, 260), Color.White);
                    sb.DrawString(font, "Press Space to Play Again ", new Vector2(322, 400), Color.White);
                    sb.DrawString(font, "     Press Esc to Quit    ", new Vector2(322, 440), Color.White);
                    break;

                case State.ORANGE_WIN:
                    sb.DrawString(font, "       Orange Wins!       ", new Vector2(322, 260), Color.White);
                    sb.DrawString(font, "Press Space to Play Again ", new Vector2(322, 400), Color.White);
                    sb.DrawString(font, "     Press Esc to Quit    ", new Vector2(322, 440), Color.White);
                    break;

            }

            sb.End();

            base.Draw(gameTime);
        }
    }
}