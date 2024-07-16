using System;
using System.Collections.Generic;
using DarkSurvival.Scripts.Interfaces;
using UnityEngine;

namespace DarkSurvival.Scripts.Systems.Utils
{
    public class Updater : MonoBehaviour
    {
        private List<IUpdatable> _updatableObjects;

        public void Initialize()
        {
            _updatableObjects = new List<IUpdatable>();
        }

        public void RegisterUpdatable(params IUpdatable[] updatables)
        {
            foreach (var updatable in updatables)
            {
                _updatableObjects.Add(updatable);
            }
        }
    
        public void UnregisterUpdatable(IUpdatable updatable)
        {
            _updatableObjects.Remove(updatable);
        }
        private void Update()
        {
            foreach (var updatable in _updatableObjects)
            {
                if (updatable != null)
                {
                    updatable.Update();
                }
            }
        }
    }
}
