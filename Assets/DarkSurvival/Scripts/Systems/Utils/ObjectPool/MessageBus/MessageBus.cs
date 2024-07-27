using System;
using System.Collections.Generic;

namespace DarkSurvival.Scripts.Systems.Utils.MessageBus
{
    public static class MessageBus
    {
        private static Dictionary<Type, List<Action<object>>> _subscribers = new Dictionary<Type, List<Action<object>>>();
        
        public static void Subscribe<T>(Action<T> callback) where T : class
        {
            if (!_subscribers.ContainsKey(typeof(T)))
            {
                _subscribers[typeof(T)] = new List<Action<object>>();
            }
            _subscribers[typeof(T)].Add((obj) => callback(obj as T));
        }
        
        public static void Publish<T>(T message) where T : class
        {
            if (_subscribers.ContainsKey(typeof(T)))
            {
                foreach (var callback in _subscribers[typeof(T)])
                {
                    callback(message);
                }
            }
        }
    }
}
