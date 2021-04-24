namespace BulletHell.Game_Utilities
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using BulletHell.Sprites.Entities;
    using BulletHell.Sprites.The_Player;
    using BulletHell.Waves;
    using Newtonsoft.Json.Linq;

    internal class GameLoader
    {
        private static Dictionary<string, object> gameDictionary = null; // singleton
        private static Dictionary<string, Entity> entityDictionary = null;

        public static void LoadGame(string jsonToLoad)
        {
            LoadGameDictionary(jsonToLoad);
            LoadEnemies();
        }

        public static List<Wave> LoadWaves()
        {
            List<object> waveProperties = (List<object>)gameDictionary["waves"];

            List<Wave> waves = new List<Wave>();

            foreach (object wave in waveProperties)
            {
                waves.Add(new Wave((Dictionary<string, object>)wave));
            }

            return waves;
        }

        public static Player LoadPlayer()
        {
            return (Player)EntityFactory.CreateEntity((Dictionary<string, object>)gameDictionary["player"]);
        }

        public static Entity GetEnemy(string enemyType)
        {
            return entityDictionary[enemyType];
        }

        private static void LoadGameDictionary(string jsonToLoad)
        {
            string jsonFilePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "JSONs\\" + jsonToLoad + ".json");
            string json = File.ReadAllText(jsonFilePath);
            JObject jsonObj = JObject.Parse(json);
            gameDictionary = (Dictionary<string, object>)ToCollections(jsonObj);
        }

        private static void LoadEnemies()
        {
            List<object> listOfEntityProperties = (List<object>)gameDictionary["enemies"];
            entityDictionary = new Dictionary<string, Entity>();

            foreach (object entity in listOfEntityProperties)
            {
                entityDictionary.Add((string)((Dictionary<string, object>)entity)["entityType"], EntityFactory.CreateEntity((Dictionary<string, object>)entity));
            }
        }

        // Source: https://stackoverflow.com/questions/14886800/convert-jobject-into-dictionarystring-object-is-it-possible
        private static object ToCollections(object o)
        {
            if (o is JObject jo)
            {
                return jo.ToObject<IDictionary<string, object>>().ToDictionary(k => k.Key, v => ToCollections(v.Value));
            }

            if (o is JArray ja)
            {
                return ja.ToObject<List<object>>().Select(ToCollections).ToList();
            }

            if (o is long)
            {
                return Convert.ToSingle(o);
            }

            if (o is double)
            {
                return Convert.ToSingle(o);
            }

            return o;
        }
    }
}
