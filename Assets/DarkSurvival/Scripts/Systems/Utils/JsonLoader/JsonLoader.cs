using Newtonsoft.Json;
using UnityEngine;

namespace DarkSurvival.Scripts.Systems.Utils.JsonLoader
{
    public class JsonLoader
    {
        public static T LoadJsonFile<T>(string fileName)
        {
            TextAsset jsonData = Resources.Load<TextAsset>(fileName);

            if (jsonData != null)
            {
                T data = JsonConvert.DeserializeObject<T>(jsonData.text);
                return data;
            }
            else
            {
                Debug.LogError($"JSON file '{fileName}' not found in Resources.");
                return default;
            }
        }
    }
}