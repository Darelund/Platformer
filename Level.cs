using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer
{
    public class Level
    {
        private Vector2 _startPosition;
        public Platform[,] _tiles;

        public static Vector2 TileSize { get; private set; } = new Vector2(31, 31);

        public bool LevelCompleted { get; set; } = false;
       // public List<GameObject> GameObjectsInLevel { get; } = new();
        public Level()
        {
        }


      //  public List<GameObject> ItemsInLevel = new List<GameObject>();
        public void ActivateLevel()
        {
           // CreateLevel(Levels.LevelData.LevelFile, Levels.LevelData.LevelStartPosition, Levels.LevelData.TileData, Levels.LevelData.GameObjectData);
           // GameManager.ChangeGameState(GameManager.GameState.Playing);
        }

        /// <summary>
        /// Creates a 2D grid level based on the file you give it. In the file each character represents one tile. 
        /// So you give it a list of tuples where each tuple is its character, texture and if you can walk on it.
        /// </summary>
        /// <param name="levelFile"></param>
        /// <param name="tileTexture"></param>
        public void CreateLevel(string levelFile)
        {
            //   List<string> result = FileManager.ReadFromFile(levelFile);
            //  _tiles = new Tile[result[0].Length, result.Count];
            GameManager.GameObjects.AddRange(JsonParser.CreateGameObjects(levelFile));
        }

        public virtual bool IsLevelCompleted()
        {
            return GameManager.GameObjects.FindAll(obj => obj is Item).ToList().Count <= 0;
        }
       
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //foreach (Platform tile in _tiles)
            //{
            //    tile.Draw(spriteBatch);
            //}
        }
        public virtual void Update(GameTime gameTime)
        {

        }
        public void UnloadLevel()
        {
           // _tiles = null;
            GameManager.GameObjects.Clear();
        }

       //How should collision be checked?
        //public bool IsTileWall(Vector2 position, Vector2 dir, GameObject gameObject)
        //{
        //    Point tilePos = GetTileAtPosition(position);

        //    if(dir.X != 0)
        //    {
        //        tilePos.X += (int)dir.X;
        //    }
        //    else
        //    {
        //        tilePos.Y += (int)dir.Y;
        //    }
        //    if(!TileExistsAtPosition(tilePos))
        //    {
        //        Console.WriteLine("Outside of bounds, so will default to false");
        //        return false;
        //    }

        //    if(gameObject is PlayerController && PlayerController.Instance.PlayerState == PlayerState.Attacking)
        //    {
        //        bool isWall = (_tiles[tilePos.X, tilePos.Y].Type == PlatformType.Breakable);
        //        if(isWall)
        //        {
        //            var tile = _tiles[tilePos.X, tilePos.Y];
        //            Texture2D pathTexture = ResourceManager.GetTexture("empty");
        //            PlatformType pathTileType = PlatformType.Breakable;
        //            char pathName = 'p';

        //            tile.SwitchTile(pathTexture);
        //            tile.Type = pathTileType;
        //        }
        //    }

        //    return (_tiles[tilePos.X, tilePos.Y].Type == PlatformType.Breakable) || (_tiles[tilePos.X, tilePos.Y].Type == PlatformType.Unbreakable);
        //}
        //private Point GetTileAtPosition(Vector2 vec)
        //{
        //    vec -= _startPosition;
        //    return new Point((int)vec.X / (int)TileSize.X, (int)vec.Y / (int)TileSize.Y);
        //}
        //private bool TileExistsAtPosition(Point tilePos)
        //{
        //    if (tilePos.X < 0 || tilePos.X >= _tiles.GetLength(0)) return false;
        //    if (tilePos.Y < 0 || tilePos.Y >= _tiles.GetLength(1)) return false;
        //   // TileSteppedOnHandler?.Invoke(_tiles[tilePos.X, tilePos.Y]);
        //    return true;
        //}

        //public string GetTileKey(Point tilePosition)
        //{
        //    Point tilePos = GetTileAtPosition(tilePosition.ToVector2());

        //    string tileKey = string.Empty;
        //    (int y, int x)[] directions = { (-1, 0), (0, 1), (1, 0), (0, -1) };

        //    foreach (var (y, x) in directions)
        //    {
        //        int newY = tilePos.Y + y;
        //        int newX = tilePos.X + x;

        //       // tileKey += IsTileWall(new(newX, newY)) ? "0" : "1";
        //       if(!TileExistsAtPosition(new Point(newX, newY)))
        //        {
        //            tileKey += "0";
        //            continue;
        //        }
        //        tileKey += (_tiles[newX, newY].Type == PlatformType.Breakable) || (_tiles[newX, newY].Type == PlatformType.Unbreakable) ? "0" : "1";
        //    }
        //    return tileKey;
        //}
    }
}
