using UnityEngine;

namespace DarkSurvival.Data.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
    public class ItemData : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private string _name;
        [SerializeField] private Sprite _icon;
        [SerializeField] private int _maxStackSize;
        
        public string Id => _id;
        public string Name => _name;
        public Sprite Icon => _icon;
        public int MaxStackSize => _maxStackSize;
    }
}
