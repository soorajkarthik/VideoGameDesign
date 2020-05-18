using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Platformer
{

    public enum TileCollision
    {
        Passable = 0,
        Impassable = 1,
        Platform = 2
    }
    class Tile
    {
        public string TileSheetName;
        public int TileSheetIndex;
        public TileCollision Collision;

        public const int Width = 64;
        public const int Height = 64;
        public static readonly Vector2 Size = new Vector2(Width, Height);

        public Tile(string _tileSheetName, int _tileSheetIndex, TileCollision _collision)
        {
            TileSheetName = _tileSheetName;
            TileSheetIndex = _tileSheetIndex;
            Collision = _collision;
        }
    }
}
