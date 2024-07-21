using System;
using System.Collections.Generic;
using DarkSurvival.Data.GameObjects.Items.Scripts;
using DarkSurvival.Data.ScriptableObjects;
using UnityEngine;

namespace DarkSurvival.Scripts.Systems.InventorySystem
{
    public class InventoryModel
    {
        private const string DropItemPath = "DefaultItemPrefab";
        
        public event Action OnInventoryChanged;
        
        private List<InventorySlot> _slots;
        
        [InjectNamed("Player")] 
        private GameObject _characterInScene;

        private GameObject _dropItemPrefab;

        public InventoryModel(int slotCount)
        {
            _slots = new List<InventorySlot>(slotCount);
            for (int i = 0; i < slotCount; i++)
            {
                _slots.Add(new InventorySlot());
            }
            
            _dropItemPrefab = Resources.Load<GameObject>(DropItemPath);
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
            Vector3 position = _characterInScene.transform.position + _characterInScene.transform.forward * 2f + Vector3.up * 1f;
            GameObject droppedItem = UnityEngine.Object.Instantiate(_dropItemPrefab, position, Quaternion.identity);
            DefaultItem defaultItem = droppedItem.GetComponent<DefaultItem>();
            defaultItem.Initialize(itemData, count);
        }
    }
}
