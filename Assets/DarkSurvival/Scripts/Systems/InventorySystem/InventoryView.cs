using System.Collections.Generic;
using UnityEngine;

namespace DarkSurvival.Scripts.Systems.InventorySystem
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField]
        private Transform _slotsParent;

        private List<InventorySlotView> _slotViews;

        public void Initialize(int slotCount)
        {
            _slotViews = new List<InventorySlotView>(slotCount);
            for (int i = 0; i < slotCount; i++)
            {
                var slotView = new InventorySlotView(_slotsParent.GetChild(i));
                _slotViews.Add(slotView);
            }
        }

        public void UpdateView(IReadOnlyList<InventorySlot> slots)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                _slotViews[i].UpdateView(slots[i]);
            }
        }
    }
}
