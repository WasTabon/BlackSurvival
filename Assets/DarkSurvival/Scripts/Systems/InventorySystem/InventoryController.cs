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
            _view.Initialize(_model.Slots.Count);
        }

        public void AddItem(ItemData itemData, int count)
        {
            _model.AddItem(itemData, count);
        }

        public void RemoveItem(int slotIndex, int count)
        {
            _model.RemoveItem(slotIndex, count);
        }

        private void UpdateView()
        {
            _view.UpdateView(_model.Slots);
        }
    }
}
