using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Platformer
{
    class Level : IDisposable
    {
        private Tile[,] tiles;
        private Dictionary<string, Texture2D> tileSheets;
        public Dictionary<int, Rectangle> tileSourceRects;
        private List<Enemy> enemies;
        private List<Enemy> deadEnemies;

        public Player Player
        {
            get { return player; }
        }
        Player player;

        private Vector2 start;

        public ContentManager Content
        {
            get { return content; }
        }
        ContentManager content;

        private const int TileWidth = 64;
        private const int TileHeight = 64;
        private const int TilesPerRow = 5;
        private const int NumRowsPerSheet = 5;

        private Random random = new Random(1337);

        public int Width
        {
            get { return tiles.GetLength(0); }
        }

        public int Height
        {
            get { return tiles.GetLength(1); }
        }

        public void Dispose()
        {
            Content.Unload();
        }

        public Level(IServiceProvider _serviceProvider, string path)
        {
            content = new ContentManager(_serviceProvider, "Content");

            tileSheets = new Dictionary<string, Texture2D>();
            tileSheets.Add("Blocks", Content.Load<Texture2D>("Tiles/Blocks"));
            tileSheets.Add("Platforms", Content.Load<Texture2D>("Tiles/Platforms"));

            tileSourceRects = new Dictionary<int, Rectangle>();
            for (int i = 0; i < TilesPerRow * NumRowsPerSheet; i++)
            {
                Rectangle rectTile = new Rectangle(
                    (i % TilesPerRow) * TileWidth,
                    (i / TilesPerRow) * TileHeight,
                    TileWidth,
                    TileHeight);
                tileSourceRects.Add(i, rectTile);
            }

            enemies = new List<Enemy>();
            deadEnemies = new List<Enemy>();
            LoadTiles(path);
        }

        private void LoadTiles(string path)
        {
            int numOfTilesAcross = 0;
            List<string> lines = new List<string>();
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string line = reader.ReadLine();
                    numOfTilesAcross = line.Length;
                    while (line != null)
                    {
                        lines.Add(line);
                        int nextLineWidth = line.Length;
                        if (nextLineWidth != numOfTilesAcross)
                            throw new Exception(String.Format(
                                "The length of liine {0} is different from all preceeding lines.",
                                lines.Count));
                        line = reader.ReadLine();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file couldn't be read: ");
                Console.WriteLine(e.Message);
            }

            tiles = new Tile[numOfTilesAcross, lines.Count];

            for (int y = 0; y < Height; ++y)
            {
                for (int x = 0; x < Width; ++x)
                {
                    string currentRow = lines[y];
                    char tileType = currentRow[x];
                    tiles[x, y] = LoadTile(tileType, x, y);
                }
            }
        }

        private Tile LoadTile(char _tileType, int _x, int _y)
        {
            switch (_tileType)
            {
                case '.':
                    return new Tile(String.Empty, 0, TileCollision.Passable);

                case 'B':
                    return LoadVarietyTile("Platforms", 0, 5);

                case 'G':
                    return LoadVarietyTile("Platforms", 5, 5);

                case 'O':
                    return LoadVarietyTile("Platforms", 10, 5);

                case 'R':
                    return LoadVarietyTile("Platforms", 15, 5);

                case 'Y':
                    return LoadVarietyTile("Platforms", 20, 5);

                case 'b':
                    return LoadVarietyTile("Blocks", 0, 5);

                case 'g':
                    return LoadVarietyTile("Blocks", 5, 5);

                case 'o':
                    return LoadVarietyTile("Blocks", 10, 5);

                case 'r':
                    return LoadVarietyTile("Blocks", 15, 5);

                case 'y':
                    return LoadVarietyTile("Blocks", 20, 5);

                case '+':
                    return LoadStartTile(_x, _y);

                case 'e':
                    return LoadEnemyTile(_x, _y, "e");

                default:
                    throw new NotSupportedException(String.Format(
                        "Unsupported tile type character '{0}' at position {1} {2}.",
                        _tileType, _x, _y));
            }
        }

        private Tile LoadVarietyTile(string _tileSheetName, int _colorRow, int _variationCount)
        {
            int index = random.Next(_variationCount);
            int tileSheetIndex = _colorRow + index;
            return new Tile(_tileSheetName, tileSheetIndex, TileCollision.Impassable);
        }

        private Tile LoadStartTile(int _x, int _y)
        {
            if (Player != null)
                throw new NotSupportedException("A level may only have one starting point");

            start = new Vector2((_x * 64) + 48, (_y * 64) + 16);
            player = new Player(this, start);

            return new Tile(String.Empty, 0, TileCollision.Passable);
        }

        private Tile LoadEnemyTile(int _x, int _y, string _enemy)
        {
            Vector2 position = new Vector2((_x * 64) + 48, (_y * 64) + 20);
            enemies.Add(new Enemy(this, position, _enemy));
            return new Tile(String.Empty, 0, TileCollision.Passable);
        }

        public TileCollision GetCollision(int _x, int _y)
        {
            if (_x < 0 || _x >= Width)
                return TileCollision.Impassable;

            if (_y < 0 || _y >= Height)
                return TileCollision.Passable;

            return tiles[_x, _y].Collision;
        }

        public Rectangle GetBounds(int _x, int _y)
        {
            if (_x < 0 || _y < 0 || _x >= Width || _y >= Height)
                return new Rectangle(_x * Tile.Width, _y * Tile.Height, Tile.Width, Tile.Height);
            return new Rectangle(_x * Tile.Width, (_y * Tile.Height) + 5, Tile.Width, Tile.Height - 5);
        }
        
        public void Update(GameTime _gameTime)
        {
            Player.Update(_gameTime);
            if (Player.IsCompletelyDead)
                Player.Reset(start);
            UpdateEnemies(_gameTime);
        }

        private void UpdateEnemies(GameTime _gameTime)
        {
            foreach (Enemy enemy in enemies)
            {
                enemy.Update(_gameTime);
                if (player.IsAlive && enemy.IsAlive)
                {
                    if (enemy.BoundingRectangle.Intersects(Player.BoundingRectangle))
                    {
                        if (enemy.BoundingRectangle.Center.Y > Player.BoundingRectangle.Bottom && Player.Velocity.Y > 0)
                        {
                            OnEnemyKilled(enemy);
                        }
                        else
                        {
                            OnPlayerKilled();
                        }
                    }
                }
                else
                {
                    if (enemy.IsCompletelyDead)
                        deadEnemies.Add(enemy);
                }
            }
            if (deadEnemies.Count > 0)
            {
                foreach (Enemy deadEnemy in deadEnemies)
                {
                    enemies.Remove(deadEnemy);
                }
                deadEnemies.Clear();
            }

        }
        
        private void OnPlayerKilled()
        {
            Player.OnKilled();
        }

        private void OnEnemyKilled(Enemy _enemy)
        {
            _enemy.OnKilled();
        }


        public void Draw(GameTime _gameTime, SpriteBatch _spriteBatch)
        {
            DrawTiles(_spriteBatch);
            player.Draw(_gameTime, _spriteBatch);
            foreach (Enemy enemy in enemies)
                enemy.Draw(_gameTime, _spriteBatch);
        }


        private void DrawTiles(SpriteBatch spriteBatch)
        {

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (tileSheets.ContainsKey(tiles[x, y].TileSheetName))
                    {
                        Vector2 position = new Vector2(x, y) * Tile.Size;
                        spriteBatch.Draw(
                            tileSheets[tiles[x, y].TileSheetName],
                            position,
                            tileSourceRects[tiles[x, y].TileSheetIndex],
                            Color.White);
                    }
                }
            }
        }

    }
}
