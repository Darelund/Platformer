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
using static Pacman.GameFiles;

namespace Pacman
{
    public class Level
    {
        private Vector2 _startPosition;
        public Tile[,] _tiles;

        public static Vector2 TileSize { get; private set; } = new Vector2(31, 31);

        public bool LevelCompleted { get; set; } = false;
        private GameObjectFactory _factory;
       // public List<GameObject> GameObjectsInLevel { get; } = new();
        public Level()
        {
            _factory = new GameObjectFactory();
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
        /// <param name="file"></param>
        /// <param name="tileTexture"></param>
        public void CreateLevel(string file, Vector2 startPosition, List<(char TileName, Texture2D tileTexture, TileType type, Color tileColor)> tileConfigurations, List<char> GameObjectConfigurations)
        {
            List<string> result = FileManager.ReadFromFile(file);
            _startPosition = startPosition;
            _tiles = new Tile[result[0].Length, result.Count];

            for (int i = 0; i < result.Count; i++)
            {
                for (int j = 0; j < result[0].Length; j++)
                {
                    //Check if a specific tile was found, otherwise create a default one
                    bool tileFound = false;
                    foreach (var tileConfig in tileConfigurations)
                    {
                        if (result[i][j] == tileConfig.TileName)
                        {
                            _tiles[j, i] = new Tile(new Vector2(TileSize.X * j + _startPosition.X, TileSize.Y * i + _startPosition.Y), tileConfig.tileTexture, tileConfig.type, tileConfig.tileColor, tileConfig.TileName);
                          tileFound = true;
                            break;
                        }
                    }
                    if (!tileFound)
                    {
                        //Default - Path
                        string defaultTextureString = "empty";
                        TileType defaultTileType = TileType.Path;
                        Color defaultColor = Color.White;
                        char defaultName = 'p';
                        _tiles[j, i] = new Tile(new Vector2(TileSize.X * j + _startPosition.X, TileSize.Y * i + _startPosition.Y), ResourceManager.GetTexture(defaultTextureString), defaultTileType, defaultColor, defaultName);

                        foreach (char GameObjectName in GameObjectConfigurations)
                        {

                            if (result[i][j] == GameObjectName)
                            {
                                var startPos = new Vector2(TileSize.X * j + _startPosition.X, TileSize.Y * i + _startPosition.Y);
                                (char Type, Vector2 StartPos) Data = (GameObjectName, startPos);
                                // GameObjectsInLevel.Add(_factory.CreateGameObjectFromType(Data));
                                GameManager.GameObjects.Add(_factory.CreateGameObjectFromType(Data));
                                break;
                            }
                        }
                    }
                }
            }
            Debug.WriteLine(GameManager.GameObjects.Count);
            foreach (Teleport teleport in GameManager.GameObjects.FindAll(obj => obj.GetType() == typeof(Teleport)).ToList())
            {
                //Find the teleports array location 
                var location = GetTileAtPosition(teleport.Position);
              //  Debug.WriteLine($"X: {location.X} Y: {location.Y}");

                string tileKey = string.Empty;
                (int y, int x)[] directions = { (-1, 0), (0, 1), (1, 0), (0, -1) };

                foreach (var (y, x) in directions)
                {
                    int newY = location.Y + y;
                    int newX = location.X + x;

                    // tileKey += IsTileWall(new(newX, newY)) ? "0" : "1";
                    if (!TileExistsAtPosition(new Point(newX, newY)))
                    {
                        tileKey += "1";
                        continue;
                    }
                    tileKey +=  "0";
                }
                // tileKey;

                Vector2 teleportDirection = tileKey switch
                {
                    "1000" => new Vector2(0, 1),
                    "0100" => new Vector2(1, 0),
                    "0010" => new Vector2(0, -1),
                    "0001" => new Vector2(-1, 0),
                    _ => new Vector2(0, 1)
                };
              //  Debug.WriteLine(tileKey);
                float maxDistance = 0;
                int tileToPick = 0;
                char teleportLocation = 't';
                Tile tile;
               // Debug.WriteLine(teleportDirection);
                //This part
                if(teleportDirection.X != 0)
                {
                    //X
                    for (int i = 0; i < _tiles.GetLength(0); i++)
                    {
                        if (_tiles[i, location.Y].Name != teleportLocation) continue;
                        if (Vector2.Distance(location.ToVector2(), new Vector2(i, location.Y)) > maxDistance)
                        {
                            maxDistance = Vector2.Distance(location.ToVector2(), new Vector2(location.X, i));
                            tileToPick = i;
                        }
                    }
                    tile = _tiles[tileToPick, location.Y];
                   

                }
                else
                {
                    //Y 7 13
                    for (int i = 0; i < _tiles.GetLength(1); i++)
                    {
                        if (_tiles[location.X, i].Name != teleportLocation) continue;
                        if (Vector2.Distance(location.ToVector2(), new Vector2(location.X, i)) > maxDistance)
                        {
                            maxDistance = Vector2.Distance(location.ToVector2(), new Vector2(location.X, i));
                            tileToPick = i;
                           // Debug.WriteLine(i);
                        }
                    }
                    tile = _tiles[location.X, tileToPick];
                   
                }
                tile.Type = TileType.Path;
                tile.SwitchTile(ResourceManager.GetTexture("empty"));
                teleport.SetPosition(tile.Pos);
            }
        }

        public virtual bool IsLevelCompleted()
        {
            return GameManager.GameObjects.FindAll(obj => obj is Item).ToList().Count <= 0;
        }
       
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tile tile in _tiles)
            {
                tile.Draw(spriteBatch);
            }
        }
        public virtual void Update(GameTime gameTime)
        {

        }
        public void UnloadLevel()
        {
            _tiles = null;
            GameManager.GameObjects.Clear();
        }

        //Maybe use?
        public void CreateGameObjects(string objectsFilePath)
        {
            //List<string> fileLines = FileManager.ReadFromFile(objectsFilePath);

            //int i = 0;
            //while (i < fileLines.Count)
            //{
            //    // Get the object type from the first line
            //    string objectType = fileLines[i].Trim();
            //    i++;  // Move to the next line for object data

            //    // Read the relevant properties for each object
            //    List<string> objectData = new List<string>();

            //    while (i < fileLines.Count && !string.IsNullOrWhiteSpace(fileLines[i]))
            //    {
            //        objectData.Add(fileLines[i].Trim());
            //        i++;
            //    }

            //    // Create the appropriate game object based on the object type
            //    GameObject newObject = _factory.CreateGameObjectFromType(objectType, objectData);

            //    if (newObject != null)
            //    {
            //        GameObjectsInLevel.Add(newObject);
            //    }

            //    i++;  // Skip the blank line between object definitions
            //}
        }
       // public abstract void SetTarget();


        public static List<(char TileName, Texture2D tileTexture, TileType Type, Color tileColor)> ReadTileDataFromFile(string fileName)
        {
            List<(char, Texture2D, TileType, Color)> tileData = new List<(char, Texture2D, TileType, Color)>();

            using (StreamReader reader = new StreamReader(fileName))
            {
                while (!reader.EndOfStream)
                {
                    string[] line = reader.ReadLine().Split(' ');

                    if (line.Length == 4)
                    {
                        char tileName = line[0][0];
                        string textureName = line[1];
                        TileType type = (TileType)Enum.Parse(typeof(TileType), line[2]);
                        string colorName = line[3].Trim();
                        Color color = colorName switch
                        {
                            "White" => Color.White,
                            "Red" => Color.Red,
                            "Brown" => Color.Brown,
                            "Blue" => Color.LightPink,
                            "Green" => Color.Green,
                            "DarkGreen" => Color.DarkGreen,
                            _ => Color.White // Default color if not found
                        };
                        // Add the tile to the list, converting the texture name to a Texture2D object
                        tileData.Add((tileName, ResourceManager.GetTexture(textureName), type, color));
                    }
                }
            }

            return tileData;
        }
        public static List<char> ReadGameObjectDataFromFile(string fileName)
        {
            List<char> tileData = new List<char>();

            using (StreamReader reader = new StreamReader(fileName))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    tileData.Add(line[0]);
                }
            }
            return tileData;
        }









        //Used to check if tiles exists and are of x type
        //public bool IsTileWalkable(Vector2 vec)
        //{
        //    Point tilePos = GetTileAtPosition(vec);

        //    if (!TileExistsAtPosition(tilePos)) return false;

        //    return !(_tiles[tilePos.X, tilePos.Y].Type == TileType.NonWalkable);
        //}
        public bool IsTileWall(Vector2 position, Vector2 dir, GameObject gameObject)
        {
            Point tilePos = GetTileAtPosition(position);

            if(dir.X != 0)
            {
                tilePos.X += (int)dir.X;
            }
            else
            {
                tilePos.Y += (int)dir.Y;
            }
            if(!TileExistsAtPosition(tilePos))
            {
                Console.WriteLine("Outside of bounds, so will default to false");
                return false;
            }

            if(gameObject is PlayerController && PlayerController.Instance.PlayerState == PlayerState.Attacking)
            {
                bool isWall = (_tiles[tilePos.X, tilePos.Y].Type == TileType.Wall);
                if(isWall)
                {
                    var tile = _tiles[tilePos.X, tilePos.Y];
                    Texture2D pathTexture = ResourceManager.GetTexture("empty");
                    TileType pathTileType = TileType.Path;
                    char pathName = 'p';

                    tile.SwitchTile(pathTexture);
                    tile.Type = pathTileType;
                    tile.Name = pathName;
                }
            }

            return (_tiles[tilePos.X, tilePos.Y].Type == TileType.Wall) || (_tiles[tilePos.X, tilePos.Y].Type == TileType.Unbreakable);
        }
        private Point GetTileAtPosition(Vector2 vec)
        {
            vec -= _startPosition;
            return new Point((int)vec.X / (int)TileSize.X, (int)vec.Y / (int)TileSize.Y);
        }
        private bool TileExistsAtPosition(Point tilePos)
        {
            if (tilePos.X < 0 || tilePos.X >= _tiles.GetLength(0)) return false;
            if (tilePos.Y < 0 || tilePos.Y >= _tiles.GetLength(1)) return false;
           // TileSteppedOnHandler?.Invoke(_tiles[tilePos.X, tilePos.Y]);
            return true;
        }

        public string GetTileKey(Point tilePosition)
        {
            Point tilePos = GetTileAtPosition(tilePosition.ToVector2());

            string tileKey = string.Empty;
            (int y, int x)[] directions = { (-1, 0), (0, 1), (1, 0), (0, -1) };

            foreach (var (y, x) in directions)
            {
                int newY = tilePos.Y + y;
                int newX = tilePos.X + x;

               // tileKey += IsTileWall(new(newX, newY)) ? "0" : "1";
               if(!TileExistsAtPosition(new Point(newX, newY)))
                {
                    tileKey += "0";
                    continue;
                }
                tileKey += (_tiles[newX, newY].Type == TileType.Path) || (_tiles[newX, newY].Type == TileType.Unbreakable) ? "0" : "1";
            }
            return tileKey;
        }
    }
}
