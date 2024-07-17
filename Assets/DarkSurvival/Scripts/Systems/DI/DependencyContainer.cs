using System;
using System.Collections.Generic;
using System.Reflection;

namespace DarkSurvival.Scripts.Systems.DI
{
    public class DependencyContainer
    {
        private static DependencyContainer _instance;
        private static readonly object Lock = new object();

        private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();
        private readonly Dictionary<string, object> _namedServices = new Dictionary<string, object>();

        private DependencyContainer()
        {
        }

        public static DependencyContainer Instance
        {
            get
            {
                lock (Lock)
                {
                    if (_instance == null)
                    {
                        _instance = new DependencyContainer();
                    }

                    return _instance;
                }
            }
        }

        public void Register<T>(T instance)
        {
            var type = typeof(T);
            _services[type] = instance;
        }

        public void Register(string name, object instance)
        {
            _namedServices[name] = instance;
        }

        public T Resolve<T>()
        {
            var type = typeof(T);
            return (T)Resolve(type);
        }

        public object Resolve(Type type)
        {
            if (_services.TryGetValue(type, out var instance))
            {
                return instance;
            }

            throw new InvalidOperationException($"Service of type {type.Name} not registered.");
        }

        public object Resolve(string name)
        {
            if (_namedServices.TryGetValue(name, out var instance))
            {
                return instance;
            }

            throw new InvalidOperationException($"Service with name {name} not registered.");
        }

        public void InjectDependencies(object target)
        {
            var targetType = target.GetType();
            var fields = targetType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            var properties =
                targetType.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);

            foreach (var field in fields)
            {
                var attribute = field.GetCustomAttribute<InjectAttribute>();
                if (attribute != null)
                {
                    var dependency = Resolve(field.FieldType);
                    field.SetValue(target, dependency);
                }
                else
                {
                    var namedAttribute = field.GetCustomAttribute<InjectNamedAttribute>();
                    if (namedAttribute != null)
                    {
                        var dependency = Resolve(namedAttribute.Name);
                        field.SetValue(target, dependency);
                    }
                }
            }

            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttribute<InjectAttribute>();
                if (attribute != null)
                {
                    var dependency = Resolve(property.PropertyType);
                    property.SetValue(target, dependency);
                }
                else
                {
                    var namedAttribute = property.GetCustomAttribute<InjectNamedAttribute>();
                    if (namedAttribute != null)
                    {
                        var dependency = Resolve(namedAttribute.Name);
                        property.SetValue(target, dependency);
                    }
                }
            }
        }
    }
}