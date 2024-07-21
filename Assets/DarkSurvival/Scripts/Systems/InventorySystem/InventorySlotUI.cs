using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DarkSurvival.Scripts.Systems.InventorySystem
{
    public class InventorySlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private Image _iconImage;
        [SerializeField] private TextMeshProUGUI _stackSizeText;

        public InventorySlot slot;
        private CanvasGroup _canvasGroup;
        private RectTransform _rectTransform;
        private Vector2 _originalPosition;
        private InventoryController _inventoryController;
        private Canvas _canvas;

        public void Initialize(InventorySlot slot, InventoryController inventoryController)
        {
            this.slot = slot;
            _inventoryController = inventoryController;
            UpdateUI();
        }

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = gameObject.AddComponent<CanvasGroup>();
            _canvas = GetComponentInParent<Canvas>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _originalPosition = _rectTransform.anchoredPosition;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.alpha = 0.6f;
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.alpha = 1.0f;

            GameObject raycastedObject = eventData.pointerCurrentRaycast.gameObject;
            InventorySlotUI dropSlot = null;

            if (raycastedObject != null)
            {
                dropSlot = raycastedObject.GetComponentInParent<InventorySlotUI>();

                if (dropSlot != null && dropSlot != this)
                {
                    TransferItems(dropSlot);
                }
                else
                {
                    _rectTransform.anchoredPosition = _originalPosition;
                }
            }
            else
            {
                DropItemOutside();
            }

            _rectTransform.anchoredPosition = _originalPosition;
        }

        private void TransferItems(InventorySlotUI targetSlot)
        {
            if (!targetSlot.slot.IsEmpty)
            {
                var tempItemData = targetSlot.slot.ItemData;
                var tempItemCount = targetSlot.slot.StackSize;

                targetSlot.slot.SetItem(slot.ItemData, slot.StackSize);
                slot.SetItem(tempItemData, tempItemCount);
            }
            else
            {
                targetSlot.slot.SetItem(slot.ItemData, slot.StackSize);
                slot.Clear();
            }

            targetSlot.UpdateUI();
            UpdateUI();
        }

        private void DropItemOutside()
        {
            int slotIndex = transform.GetSiblingIndex();
            var itemData = slot.ItemData;
            var itemCount = slot.StackSize;
            _inventoryController.DropItem(itemData, itemCount);
            _inventoryController.RemoveItem(slotIndex, itemCount);
        }

        private void UpdateUI()
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
                _stackSizeText.text = slot.StackSize.ToString();
            }
        }
    }
}
