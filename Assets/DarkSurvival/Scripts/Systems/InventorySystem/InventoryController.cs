using DarkSurvival.Data.ScriptableObjects;
using UnityEngine;

namespace DarkSurvival.Scripts.Systems.InventorySystem
{
    public class InventoryController : MonoBehaviour
    {
        private InventoryModel _model;
        private InventoryView _view;

        public void Initialize(InventoryModel model, InventoryView view)
        {
            _model = model;
            _view = view;

            _model.OnInventoryChanged += UpdateView;
            _view.Initialize(_model.Slots.Count, this);
        }

        public void AddItem(ItemData itemData, int count)
        {
            _model.AddItem(itemData, count);
        }

        public InventorySlot GetItemFromSlot(int slotIndex)
        {
            return _model.GetItem(slotIndex);
        }

        public void RemoveItem(int slotIndex, int count)
        {
            _model.RemoveItem(slotIndex, count);
        }
        public void DropItem(ItemData itemData, int count)
        {
            _model.DropItem(itemData, count);
        }

        private void UpdateView()
        {
            _view.UpdateView(_model.Slots, this);
        }
    }
}