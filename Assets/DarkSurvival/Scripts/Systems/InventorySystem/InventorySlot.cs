using DarkSurvival.Data.ScriptableObjects;
using UnityEngine;

namespace DarkSurvival.Scripts.Systems.InventorySystem
{
    public class InventorySlot
    {
        public ItemData ItemData { get; private set; }
        public int StackSize { get; private set; }
        public bool IsEmpty => ItemData == null;

        public void SetItem(ItemData itemData, int stackSize)
        {
            ItemData = itemData;
            StackSize = stackSize;
        }

        public void Clear()
        {
            ItemData = null;
            StackSize = 0;
        }

        public void AddToStack(int count)
        {
            StackSize = Mathf.Min(StackSize + count, ItemData.MaxStackSize);
        }

        public void RemoveFromStack(int count)
        {
            StackSize = Mathf.Max(0, StackSize - count);
            if (StackSize == 0)
            {
                Clear();
            }
        }
    }
}

