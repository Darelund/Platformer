using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public class LevelConfig
    {
        public string LevelFile { get; }
        public Vector2 LevelStartPosition { get; }
       // public Vector2 PlayerStartPosition { get; }
        public List<(char TileName, Texture2D tileTexture, TileType type, Color tileColor)> TileData { get; }
        //  public string GameObjectsFile { get; }
        public List<char> GameObjectData;

        public LevelConfig
            (string levelFile, 
            Vector2 levelStartPosition,
            List<(char TileName, Texture2D tileTexture, TileType type, Color tileColor)> tileData,
            List<char> gameObjectData
           /*  string gameObjectsFile, Vector2 playerStartPosition*/)
        {
            LevelFile = levelFile;
            LevelStartPosition = levelStartPosition;
            TileData = tileData;
            GameObjectData = gameObjectData;
           // GameObjectsFile = gameObjectsFile;
          //  PlayerStartPosition = playerStartPosition;
        }
        public LevelConfig(string levelFile, Vector2 levelStartPosition
          /*  string gameObjectsFile, Vector2 playerStartPosition*/)
        {
            LevelFile = levelFile;
            LevelStartPosition = levelStartPosition;
            // GameObjectsFile = gameObjectsFile;
            //  PlayerStartPosition = playerStartPosition;
        }
    }
}
