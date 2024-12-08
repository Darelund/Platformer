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

        public static Vector2 GetVector(string fileName, string propertyName)
        {
            if (wholeObject == null || currentFileName == null || currentFileName != null)
            {
                GetJObjectFromFile(fileName);
            }
            JObject obj = wholeObject.GetValue(propertyName) as JObject;
            return GetVector(obj);
        }
        public static List<Vector2> GetVectors(string fileName, string propertyName)
        {
            if (wholeObject == null || currentFileName == null || currentFileName != null)
            {
                GetJObjectFromFile(fileName);
            }

            List<Vector2> list = new List<Vector2>();
            JArray arrayObject = wholeObject.GetValue(propertyName) as JArray;

            for (int i = 0; i < arrayObject.Count; i++)
            {
                JObject obj = (JObject)arrayObject[i];

                list.Add(GetVector(obj));
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
        private static Vector2 GetVector(JObject obj)
        {
            int x = Convert.ToInt32(obj.GetValue("positionX"));
            int y = Convert.ToInt32(obj.GetValue("positionY"));
          //  int width = Convert.ToInt32(obj.GetValue("width"));
          //  int height = Convert.ToInt32(obj.GetValue("height"));

            return new Vector2(x, y);
        }
        public static List<GameObject> CreateGameObjects(string fileName)
        {
            string player = "player";
            string platform = "platform";
            string enemy = "enemy";

            List<GameObject> gameObjects = new List<GameObject>();

            Vector2 playerVec = GetVector(fileName, player);
            gameObjects.Add(GameObjectFactory.CreateGameObject((player, playerVec )));

            List<Vector2> vecList = GetVectors(fileName, platform);
            foreach (Vector2 vec in vecList)
            {
                gameObjects.Add(GameObjectFactory.CreateGameObject((platform, vec)));
            }

            vecList = GetVectors(fileName, enemy);
            foreach (Vector2 vec in vecList)
            {
                gameObjects.Add(GameObjectFactory.CreateGameObject((enemy, playerVec)));
            }

            return gameObjects;
        }
    }
}
