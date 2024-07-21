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

        private InventorySlot _slot;
        private CanvasGroup _canvasGroup;
        private RectTransform _rectTransform;
        private Vector2 _originalPosition;
        private InventoryController _inventoryController;
        private Canvas _canvas;

        public void Initialize(InventorySlot slot, InventoryController inventoryController)
        {
            _slot = slot;
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

            var raycastedObject = eventData.pointerCurrentRaycast.gameObject;

            if (raycastedObject != null)
            {
                var dropSlot = raycastedObject.GetComponent<InventorySlotUI>();

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
            targetSlot._slot.SetItem(_slot.ItemData, _slot.StackSize);
            _slot.Clear();

            targetSlot.UpdateUI();
            UpdateUI();
        }

        private void DropItemOutside()
        {
            int slotIndex = transform.GetSiblingIndex();
            var itemData = _slot.ItemData;
            var itemCount = _slot.StackSize;
            _inventoryController.DropItem(itemData, itemCount);
            _inventoryController.RemoveItem(slotIndex, itemCount);
        }

        private void UpdateUI()
        {
            if (_slot.IsEmpty)
            {
                _iconImage.enabled = false;
                _stackSizeText.text = "";
            }
            else
            {
                _iconImage.enabled = true;
                _iconImage.sprite = _slot.ItemData.Icon;
                _stackSizeText.text = _slot.StackSize.ToString();
            }
        }
    }
}