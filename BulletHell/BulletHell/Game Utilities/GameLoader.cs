﻿namespace BulletHell.Game_Utilities
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using global::BulletHell.Sprites.Entities;
    using global::BulletHell.Sprites.The_Player;
    using global::BulletHell.Waves;
    using Newtonsoft.Json.Linq;

    internal class GameLoader
    {
        private static Dictionary<string, object> gameDictionary = null; // singleton

        public static void LoadGameDictionary(string jsonToLoad)
        {
            string jsonFilePath = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "JSONs\\" + jsonToLoad + ".json");
            string json = File.ReadAllText(jsonFilePath);
            JObject jsonObj = JObject.Parse(json);
            gameDictionary = (Dictionary<string, object>)ToCollections(jsonObj);
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
                return Convert.ToInt32(o);
            }

            return o;
        }
    }
}
