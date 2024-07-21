    using DarkSurvival.Data.ScriptableObjects;

    namespace DarkSurvival.Scripts.Interfaces
    {
        public interface ICollectable
        {
            string Name { get; set; }
            int ItemsCount { get; set; }
            ItemData Collect();
        }
    }
