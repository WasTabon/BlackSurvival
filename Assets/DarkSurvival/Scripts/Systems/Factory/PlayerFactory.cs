using UnityEngine;

namespace DarkSurvival.Scripts.Systems.Factory
{
    public class PlayerFactory : Factory<GameObject>
    {
        private const string PrefabName = "Player";
        
        public override GameObject GetProduct(Vector3 spawnPos, Quaternion quaternion)
        {
            GameObject prefab = Resources.Load<GameObject>(PrefabName);
            if (prefab == null)
            {
                Debug.LogError($"Prefab at {PrefabName} not found.");
                return null;
            }
            GameObject player = Object.Instantiate(prefab, spawnPos, quaternion);
            return player;
        }
    }
}
