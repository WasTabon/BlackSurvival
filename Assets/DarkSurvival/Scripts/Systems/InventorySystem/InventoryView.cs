using System.Collections.Generic;
using UnityEngine;

namespace DarkSurvival.Scripts.Systems.InventorySystem
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private Transform _slotsParent;

        [SerializeField] private List<InventorySlotUI> _slotUIs;

        public void Initialize(int slotCount, InventoryController inventoryController)
        {
            _slotUIs = new List<InventorySlotUI>(slotCount);

            var slotUIs = _slotsParent.GetComponentsInChildren<InventorySlotUI>(true);

            for (int i = 0; i < slotCount; i++)
            {
                if (i < slotUIs.Length)
                {
                    var slotUI = slotUIs[i];
                    slotUI.Initialize(new InventorySlot(), inventoryController, i);
                    _slotUIs.Add(slotUI);
                }
                else
                {
                    Debug.LogWarning($"Slot count {slotCount} is greater than available slot UIs {slotUIs.Length}");
                    break;
                }
            }
        }

        public void UpdateView(IReadOnlyList<InventorySlot> slots, InventoryController inventoryController)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                _slotUIs[i].Initialize(slots[i], inventoryController, i);
            }
        }
    }
}