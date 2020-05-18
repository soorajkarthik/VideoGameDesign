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
using System.IO;

namespace Breakout
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;

        enum GameState
        {
            START,
            LEVEL1,
            LEVEL2,
            LEVEL3,
            END
        }

        Brick[,] bricks;

        Ball ball;
        Rectangle paddle;

        Texture2D texture;
        Texture2D ballTexture;

        GameState state;
        string endText;
        int score;
        int lives;
        int ballOffScreenTimer;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 700;
            graphics.PreferredBackBufferHeight = 700;
            graphics.ApplyChanges();

        }


        protected override void Initialize()
        {
            base.Initialize();
            state = GameState.START;
            bricks = new Brick[20, 20];

            paddle = new Rectangle(275, 650, 150, 20);

            endText = "";

            lives = 5;
            score = 0;
            ballOffScreenTimer = -1;

        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("SpriteFont1");
            texture = Content.Load<Texture2D>("empty");
            ballTexture = Content.Load<Texture2D>("ball");

            ball = new Ball(new Rectangle(340, 629, 20, 20), ballTexture);

        }

        private void LoadBricks(int level)
        {
            try
            {
                using (StreamReader reader = new StreamReader(@"Content\" + level + ".txt"))
                {
                    
                    for(int i = 0; i < bricks.GetLength(0); i++)
                    {
                        string[] arr = reader.ReadLine().Split(' ');

                        for(int j = 0; j < arr.Length; j++)
                        {
                            bricks[j, i] = new Brick(new Rectangle(35 * j, 35 * i, 35, 35), arr[j], texture);
                        }
                    }
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        protected override void UnloadContent()
        {
        }

       
        protected override void Update(GameTime gameTime)
        {
            KeyboardState kb = Keyboard.GetState();
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || kb.IsKeyDown(Keys.Escape))
                this.Exit();

            switch (state)
            {
                case GameState.START:
                    if (kb.IsKeyDown(Keys.Space))
                    {
                        state = GameState.LEVEL1;
                        LoadBricks(1);
                    }
                    break;

                case GameState.END:
                    if (kb.IsKeyDown(Keys.Space))
                    {
                        state = GameState.LEVEL1;
                        endText = "";
                        lives = 5;
                        score = 0;
                        LoadBricks(1);
                    }
                    break;

                default:

                    bool shouldGoToNextLevel = true;

                    foreach (Brick brick in bricks)
                    {
                        if (brick.exists)
                        {
                            shouldGoToNextLevel = false;

                            if (brick.rect.Intersects(ball.rect))
                            {

                                if (ball.rect.Intersects(brick.bottom))
                                {
                                    ball.vel.Y *= -1;
                                    ball.y = brick.rect.Bottom + 1;
                                    ball.rect.Y = brick.rect.Bottom + 1;

                                    ball.vel.X -= ball.spinRate * 0.1f;
                                    ball.vel.Y += ball.spinRate * 0.1f;
                                    ball.spinRate *= 0.9f;

                                    Console.WriteLine("Bottom");
                                }

                                else if (ball.rect.Intersects(brick.top))
                                {
                                    ball.vel.Y *= -1;
                                    ball.y = brick.rect.Y - 21;
                                    ball.rect.Y = brick.rect.Y - 21;

                                    ball.vel.X -= ball.spinRate / 60;
                                    ball.vel.Y += ball.spinRate / 60;
                                    ball.spinRate *= 0.95f;

                                    Console.WriteLine("Top");
                                }

                                else if (ball.rect.Intersects(brick.right))
                                {
                                    ball.vel.X *= -1;
                                    ball.x = brick.rect.Right + 1;
                                    ball.rect.X = brick.rect.Right + 1;

                                    ball.vel.X += ball.spinRate * 0.1f;
                                    ball.vel.Y -= ball.spinRate * 0.1f;
                                    ball.spinRate *= 0.9f;

                                    Console.WriteLine("Right");
                                }

                                else if (ball.rect.Intersects(brick.left))
                                {
                                    ball.vel.X *= -1;
                                    ball.x = brick.rect.Left - 21;
                                    ball.rect.X = brick.rect.Left - 21;

                                    ball.vel.X += ball.spinRate * 0.1f;
                                    ball.vel.Y -= ball.spinRate * 0.1f;
                                    ball.spinRate *= 0.9f;

                                    Console.WriteLine("Left");
                                }

                                brick.health--;
                                score += 100;

                                if (brick.health == 0)
                                    brick.exists = false;

                                ball.Update(paddle, kb);
                                return;
                            }
                        }
                    }

                    //CHEAT CODES
                    //paddle.X = ball.rect.Center.X - 75;

                    if (kb.IsKeyDown(Keys.Left))
                        paddle.X -= 8;
                    else if (kb.IsKeyDown(Keys.Right))
                        paddle.X += 8;

                    if (paddle.Left <= 0)
                        paddle.X = 0;
                    else if (paddle.Right >= 700)
                        paddle.X = 550;

                    if (ball.rect.Top >= 700 && ballOffScreenTimer < -30)
                    {
                        ballOffScreenTimer = 120;
                        lives--;
                    }

                    if (ballOffScreenTimer == 0)
                        ball = new Ball(new Rectangle(paddle.Center.X - 10, 629, 20, 20), ballTexture);

                    ball.Update(paddle, kb);

                    if (shouldGoToNextLevel)
                    {
                        if (state == GameState.LEVEL3)
                        {
                            state = GameState.END;
                            endText += "    You win!    \n";
                            endText += String.Format("  Score: {0:00000}\n", score);
                            endText += "Space to Restart\n";
                            endText += " Escape to Quit ";
                        }

                        else
                        {
                            state++;
                            LoadBricks((int)state);
                            ball = new Ball(new Rectangle(paddle.Center.X - 10, 629, 20, 20), ballTexture); 
                        }
                    }


                    if(lives == 0)
                    {
                        state = GameState.END;
                        endText += "   You Lose :(  \n";
                        endText += String.Format("  Score: {0:00000}\n", score);
                        endText += "Space to Restart\n";
                        endText += " Escape to Quit \n";
                    }

                    

                    break;
            }

            ballOffScreenTimer--;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            switch(state)
            {
                case GameState.START:
                    spriteBatch.DrawString(font, "     Space to Start      ", new Vector2(135, 320), Color.White);
                    spriteBatch.DrawString(font, "Use Left and Right Arrows", new Vector2(135, 360), Color.White);
                    break;

                case GameState.END:
                    spriteBatch.DrawString(font, endText, new Vector2(220, 300), Color.White);
                    break;

                default:
                    spriteBatch.Draw(texture, paddle, Color.LightGray);
                    foreach (Brick b in bricks)
                        b.Draw(spriteBatch);
                    spriteBatch.DrawString(font, String.Format(" Score: {0:00000}", score), new Vector2(10, 10), Color.White);
                    spriteBatch.DrawString(font, "Balls: " + lives, new Vector2(540, 10), Color.White);
                    ball.Draw(spriteBatch);
                    break;
            }

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
