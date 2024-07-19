using UnityEngine;

namespace DarkSurvival.Data.ScriptableObjects
{
    public enum ItemType
    {
        Resource,
        Equipment
    }
    
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
    public class ItemData : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private string _name;
        [SerializeField] private Sprite _icon;
        [SerializeField] private ItemType _itemType;
        [SerializeField] private int _maxStackSize;
        
        public string Id => _id;
        public string Name => _name;
        public Sprite Icon => _icon;
        public ItemType ItemType => _itemType;
        public int MaxStackSize => _maxStackSize;
    }
}
