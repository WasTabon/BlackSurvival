using UnityEngine;

namespace DarkSurvival.Scripts.Systems.Factory
{
    public abstract class Factory<T> where T : Object
    {
        public abstract T GetProduct( Vector3 spawnPos, Quaternion quaternion);
    }
}