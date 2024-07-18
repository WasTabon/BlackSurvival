using System;
using System.Collections.Generic;
using DarkSurvival.Data.ScriptableObjects;
using UnityEngine;

namespace DarkSurvival.Scripts.Systems.InventorySystem
{
    public class InventoryModel
    {
        public event Action OnInventoryChanged;

        [SerializeField]
        private List<InventorySlot> _slots;

        public InventoryModel(int slotCount)
        {
            _slots = new List<InventorySlot>(slotCount);
            for (int i = 0; i < slotCount; i++)
            {
                _slots.Add(new InventorySlot());
            }
        }

        public IReadOnlyList<InventorySlot> Slots => _slots;

        public void AddItem(ItemData itemData, int count = 1)
        {
            foreach (var slot in _slots)
            {
                if (slot.ItemData == itemData && slot.StackSize < itemData.MaxStackSize)
                {
                    slot.AddToStack(count);
                    OnInventoryChanged?.Invoke();
                    return;
                }
            }

            foreach (var slot in _slots)
            {
                if (slot.IsEmpty)
                {
                    slot.SetItem(itemData, count);
                    OnInventoryChanged?.Invoke();
                    return;
                }
            }
        }

        public void RemoveItem(int slotIndex, int count = 1)
        {
            if (slotIndex < 0 || slotIndex >= _slots.Count) throw new ArgumentOutOfRangeException();
            _slots[slotIndex].RemoveFromStack(count);
            OnInventoryChanged?.Invoke();
        }
    }
}
