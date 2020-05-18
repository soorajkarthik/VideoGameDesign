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

namespace ScribblePlatformer
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {

        private Level level;
        private int levelNumber;

        private const int TargetFrameRate = 60;
        private const int BackBufferWidth = 1280;
        private const int BackBufferHeight = 720;

        public enum GameState
        {
            Start,
            Playing,
            End
        }

        GameState state;

        string gameOverText;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = BackBufferWidth;
            graphics.PreferredBackBufferHeight = BackBufferHeight;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";

            TargetElapsedTime = TimeSpan.FromTicks(TimeSpan.TicksPerSecond / TargetFrameRate);
        }
        
        protected override void Initialize()
        {
            levelNumber = 1;
            state = GameState.Start;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("SpriteFont1");

            LoadLevel(levelNumber);
        }

        private void LoadLevel(int _levelNumber)
        {
            level = new Level(Services, String.Format(@"Content/Levels/Level{0:00}.txt", _levelNumber));
        }
        
        protected override void UnloadContent()
        {
            level.Dispose();
        }
        
        protected override void Update(GameTime _gameTime)
        {
            KeyboardState kb = Keyboard.GetState();
            GamePadState gps = GamePad.GetState(PlayerIndex.One);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || kb.IsKeyDown(Keys.Escape))
                this.Exit();

            switch(state)
            {
                case (GameState.Start):
                case (GameState.End):
                    if (kb.IsKeyDown(Keys.Space) || gps.IsButtonDown(Buttons.A))
                    {
                        Initialize();
                        LoadContent();
                        state = GameState.Playing;
                    }
                    break;

                case (GameState.Playing):
                    level.Update(_gameTime);
                    if (level.Player.Lives == 0 || (level.EnemiesAlive == 0 && levelNumber == 3))
                    {
                        state = GameState.End;
                        if (level.Player.Lives > 0)
                            gameOverText = "You win!";
                        else
                            gameOverText = "You lose!";
                    }

                    else if (level.EnemiesAlive == 0)
                    {
                        int playerLives = level.Player.Lives;
                        int totalScore = level.Score;
                        levelNumber++;
                        LoadLevel(levelNumber);
                        level.Player.Lives = playerLives;
                        level.Score = totalScore;
                    }
                    break;
            }
            

            base.Update(_gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.LightSkyBlue);

            spriteBatch.Begin();

            switch(state)
            {
                case (GameState.Start):
                    spriteBatch.DrawString(font, "Press \"Space\" on Keyboard or \"A\" on Controller to Start", new Vector2(220, 320), Color.Black);
                    spriteBatch.DrawString(font, "Use WASD, Arrows and Space, or Controller to move character", new Vector2(200, 360), Color.Black);
                    break;
                case (GameState.Playing):
                    level.Draw(gameTime, spriteBatch, font);
                    break;
                case (GameState.End):
                    spriteBatch.DrawString(font, gameOverText, new Vector2(600, 220), Color.Black);
                    spriteBatch.DrawString(font, String.Format("\nScore: {0:00000}", level.Score), new Vector2(590, 240), Color.Black);
                    spriteBatch.DrawString(font, "Press \"Space\" on Keyboard or \"A\" on Controller to Play Again", new Vector2(190, 400), Color.Black);
                    spriteBatch.DrawString(font, "Press \"Escape\" on Keyboard or \"Back\" on Controller to Quit", new Vector2(210, 440), Color.Black);
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
