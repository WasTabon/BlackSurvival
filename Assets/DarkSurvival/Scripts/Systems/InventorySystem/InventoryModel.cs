using System;
using System.Collections.Generic;
using DarkSurvival.Data.GameObjects.Items.Scripts;
using DarkSurvival.Data.ScriptableObjects;
using UnityEngine;

namespace DarkSurvival.Scripts.Systems.InventorySystem
{
    public class InventoryModel
    {
        public event Action OnInventoryChanged;
        
        private List<InventorySlot> _slots;
        
        [InjectNamed("Player")] 
        private GameObject _characterInScene;

        public InventoryModel(int slotCount)
        {
            _slots = new List<InventorySlot>(slotCount);
            for (int i = 0; i < slotCount; i++)
            {
                _slots.Add(new InventorySlot());
            }
        }

        public IReadOnlyList<InventorySlot> Slots => _slots;

        public void AddItem(ItemData itemData, int count)
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

        public InventorySlot GetItem(int slotIndex)
        {
            if (slotIndex < 0 || slotIndex >= _slots.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            return _slots[slotIndex];
        }

        public void RemoveItem(int slotIndex, int count = 1)
        {
            if (slotIndex < 0 || slotIndex >= _slots.Count) throw new ArgumentOutOfRangeException();
            _slots[slotIndex].RemoveFromStack(count);
            OnInventoryChanged?.Invoke();
        }

        public void DropItem(ItemData itemData, int count)
        {
            GameObject itemPrefab = Resources.Load<GameObject>("DefaultItemPrefab");
            if (itemPrefab == null)
                Debug.Log("No in resources");
            GameObject droppedItem = UnityEngine.Object.Instantiate(itemPrefab, new Vector3(1,1,1), Quaternion.identity);
            DefaultItem defaultItem = droppedItem.GetComponent<DefaultItem>();
            defaultItem.Initialize(itemData, count);
        }
    }
}
