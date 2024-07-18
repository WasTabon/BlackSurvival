using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DarkSurvival.Scripts.Systems.InventorySystem
{
    public class InventorySlotView 
    {
        private readonly TextMeshProUGUI _stackSizeText;
        private readonly Image _iconImage;

        public InventorySlotView(Transform slotTransform)
        {
            _stackSizeText = slotTransform.Find("StackSizeText").GetComponent<TextMeshProUGUI>();
            _iconImage = slotTransform.Find("IconImage").GetComponent<Image>();
        }

        public void UpdateView(InventorySlot slot)
        {
            if (slot.IsEmpty)
            {
                _iconImage.enabled = false;
                _stackSizeText.text = "";
            }
            else
            {
                _iconImage.enabled = true;
                _iconImage.sprite = slot.ItemData.Icon;
                _stackSizeText.text = slot.StackSize >= 0 ? slot.StackSize.ToString() : "";
            }
        }
    }
}
