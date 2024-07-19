using System.Collections.Generic;
using UnityEngine;

namespace DarkSurvival.Scripts.Systems.InventorySystem
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField]
        private Transform _slotsParent;

        private List<InventorySlotUI> _slotUIs;

        public void Initialize(int slotCount)
        {
            _slotUIs = new List<InventorySlotUI>(slotCount);
            for (int i = 0; i < slotCount; i++)
            {
                var slotUI = _slotsParent.GetChild(i).GetComponent<InventorySlotUI>();
                slotUI.Initialize(new InventorySlot());
                _slotUIs.Add(slotUI);
            }
        }

        public void UpdateView(IReadOnlyList<InventorySlot> slots)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                _slotUIs[i].Initialize(slots[i]);
            }
        }
    }
}