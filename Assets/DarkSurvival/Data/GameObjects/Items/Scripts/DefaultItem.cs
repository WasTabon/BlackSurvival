using DarkSurvival.Data.ScriptableObjects;
using DarkSurvival.Scripts.Interfaces;
using UnityEngine;

namespace DarkSurvival.Data.GameObjects.Items.Scripts
{
    public class DefaultItem : MonoBehaviour, ICollectable
    {
        [SerializeField] protected ItemData _itemData;

        public string Name { get; set; }
        public int ItemsCount { get; set; }

        private void Awake()
        {
            if (_itemData != null)
                Name = _itemData.Name;
        }

        public void Initialize(ItemData itemData, int itemsCount)
        {
            _itemData = itemData;
            Name = _itemData.Name;
            ItemsCount = itemsCount;
        }
        
        public ItemData Collect()
        {
            Destroy(gameObject);
            return _itemData;
        }
    }
}
