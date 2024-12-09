using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
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

        private static bool CheckIfPropertyExists(string fileName, string propertyName)
        {
            GetJObjectFromFile(fileName);
            return wholeObject[propertyName] != null;
        }
        private static void GetJObjectFromFile(string fileName)
        {
            if (wholeObject == null || currentFileName == null || currentFileName != null)
            {
                currentFileName = fileName;
                StreamReader file = File.OpenText($"Content/{fileName}");
                JsonTextReader reader = new JsonTextReader(file);
                wholeObject = JObject.Load(reader);
                file.Close();
            }
        }
        private static (JObject singleObject, JArray arrayObject) GetProperty(string fileName, string propertyName)
        {
            GetJObjectFromFile(fileName);
            JToken token = wholeObject[propertyName];

            if (token is JObject obj)
                return (obj, null);

            if (token is JArray array)
                return (null, array);

            throw new InvalidOperationException($"Property {propertyName} is neither an object nor an array.");
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
            // Define property names
            var properties = new Dictionary<string, Func<Rectangle, GameObject>>
        {
            { "player", rect => GameObjectFactory.CreateGameObject(("player", rect)) },
            { "platforms", rect => GameObjectFactory.CreateGameObject(("platforms", rect)) },
            { "enemies", rect => GameObjectFactory.CreateGameObject(("enemies", rect)) }
        };

            var gameObjects = new List<GameObject>();

            foreach (var property in properties)
            {
                string propertyName = property.Key;
                var objectCreator = property.Value;

                if (CheckIfPropertyExists(fileName, propertyName))
                {
                    var (singleObject, arrayObject) = GetProperty(fileName, propertyName);

                    if (singleObject != null)
                    {
                        Rectangle rect = GetRectangle(singleObject);
                        gameObjects.Add(objectCreator(rect));
                    }
                    else if (arrayObject != null)
                    {
                        foreach (JObject obj in arrayObject)
                        {
                            Rectangle rect = GetRectangle(obj);
                            gameObjects.Add(objectCreator(rect));
                        }
                    }
                }
            }

            return gameObjects;
        }
    }
}
