using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Pacman;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Platformer
{
    public class JsonParser
    {
        private static JObject wholeObject;
        private static string currentFileName;

        public static Rectangle GetRectangle(string fileName, string propertyName)
        {
            if (wholeObject == null || currentFileName == null || currentFileName != null)
            {
                GetJObjectFromFile(fileName);
            }
            JObject obj = wholeObject.GetValue(propertyName) as JObject;
            return GetRectangle(obj);
        }
        public static List<Rectangle> GetRectangles(string fileName, string propertyName)
        {
            if (wholeObject == null || currentFileName == null || currentFileName != null)
            {
                GetJObjectFromFile(fileName);
            }

            List<Rectangle> list = new List<Rectangle>();
            JArray arrayObject = wholeObject.GetValue(propertyName) as JArray;

            for (int i = 0; i < arrayObject.Count; i++)
            {
                JObject obj = (JObject)arrayObject[i];

                list.Add(GetRectangle(obj));
            }
            return list;
        }
        private static void GetJObjectFromFile(string fileName)
        {
            currentFileName = fileName;
            StreamReader file = File.OpenText(fileName);
            JsonTextReader reader = new JsonTextReader(file);
            wholeObject = JObject.Load(reader);
            file.Close();
        }
        private static Rectangle GetRectangle(JObject obj)
        {
            int x = Convert.ToInt32(obj.GetValue("positionX"));
            int y = Convert.ToInt32(obj.GetValue("positionY"));
            int width = Convert.ToInt32(obj.GetValue("width"));
            int height = Convert.ToInt32(obj.GetValue("height"));

            return new Rectangle(x, y, width, height);
        }
        public static List<GameObject> CreateGameObjects(string fileName)
        {
            //TODO Should use Factory, add that later


            List<GameObject> gameObjects = new List<GameObject>();

            Rectangle playerRec = GetRectangle(fileName, "player");
            gameObjects.Add(new Player(playerRec));

            List<Rectangle> recList = GetRectangles(fileName, "platforms");
            foreach (Rectangle rect in recList)
            {
                Platform platform = new Platform(rect);
                gameObjects.Add(platform);
            }

            recList = GetRectangles(fileName, "enemies");
            foreach (Rectangle rect in recList)
            {
                Enemy enemy = new Enemy(rect);
                gameObjects.Add(enemy);
            }

            return gameObjects;
        }
    }
}
