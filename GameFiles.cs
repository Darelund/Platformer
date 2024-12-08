using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Pacman.GameFiles;

namespace Pacman
{
    public static class GameFiles
    {
        public static class Levels
        {
            private static readonly Vector2 defaultLevelStartPosition = new Vector2(96, 96);

            public static readonly LevelConfig LevelData0 = new LevelConfig(Maps.DISPLAYMAP, defaultLevelStartPosition, Level.ReadTileDataFromFile(ContentInLevel.TILESINLEVEL), Level.ReadGameObjectDataFromFile(ContentInLevel.GAMEOBJECTSINLEVEL));
            public static readonly LevelConfig LevelData1 = new LevelConfig(Maps.MAP1, defaultLevelStartPosition, Level.ReadTileDataFromFile(ContentInLevel.TILESINLEVEL), Level.ReadGameObjectDataFromFile(ContentInLevel.GAMEOBJECTSINLEVEL));
          //  public static readonly LevelConfig LevelData2 = new LevelConfig(Maps.MAP2, defaultLevelStartPosition, Level.ReadTileDataFromFile(ContentInLevel.TILESINLEVEL), Level.ReadGameObjectDataFromFile(ContentInLevel.GAMEOBJECTSINLEVEL));
          //  public static readonly LevelConfig LevelData3 = new LevelConfig(Maps.MAP3, defaultLevelStartPosition, Level.ReadTileDataFromFile(ContentInLevel.TILESINLEVEL), Level.ReadGameObjectDataFromFile(ContentInLevel.GAMEOBJECTSINLEVEL));

            //public static readonly LevelConfig LevelData = new LevelConfig("Content/Map.txt", defaultLevelStartPosition);
        }
        public static class Maps
        {
            //Create Level tiles
            public const string DISPLAYMAP = "Content/DisplayMap.txt";
            public const string MAP1 = "Content/Map1.txt";
          //  public const string MAP2 = "Content/Map2.txt";
          //  public const string MAP3 = "Content/Map3.txt";
        }
        public static class ContentInLevel
        {
            public const string TILESINLEVEL = "Content/LevelTilesConfig.txt";
            public const string GAMEOBJECTSINLEVEL = "Content/LevelGameObjectsConfig.txt";
        }
        public static class Character
        {
            public static string CHARACTERCOLOR = "";
        }

        //Maybe use in future
        public static class Config
        {
            public const string UICONFIG = "Content/UIConfig.txt";
            public const string GAMESETTINGS = "Content/GameSettings.txt";
        }
    }
}
