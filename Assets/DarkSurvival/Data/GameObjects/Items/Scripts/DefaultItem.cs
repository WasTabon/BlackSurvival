using DarkSurvival.Data.ScriptableObjects;
using DarkSurvival.Scripts.Interfaces;
using UnityEngine;

namespace DarkSurvival.Data.GameObjects.Items.Scripts
{
    public class DefaultItem : MonoBehaviour, ICollectable
    {
        [SerializeField] protected ItemData _itemData;
        [field: SerializeField] public int ItemsCount { get; set; }
        
        public string Name { get; set; }

        private void Awake()
        {
            if (_itemData != null)
            {
                Name = _itemData.Name;
                if (ItemsCount > _itemData.MaxStackSize)
                    ItemsCount = _itemData.MaxStackSize;
            }
        }

        public void Initialize(ItemData itemData, int itemsCount)
        {
            _itemData = itemData;
            Name = _itemData.Name;
            ItemsCount = Mathf.Min(_itemData.MaxStackSize, itemsCount);
        }
        
        public ItemData Collect()
        {
            Destroy(gameObject);
            return _itemData;
        }
    }
}
