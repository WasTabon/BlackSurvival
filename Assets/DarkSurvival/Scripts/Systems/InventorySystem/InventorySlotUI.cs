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

        public void Initialize(InventorySlot slot)
        {
            _slot = slot;
            UpdateUI();
        }

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = gameObject.AddComponent<CanvasGroup>();
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
            
            var raycastedObect = eventData.pointerCurrentRaycast.gameObject;
            
            if (raycastedObect != null)
            {
                var dropSlot = raycastedObect.GetComponent<InventorySlotUI>();

                if (dropSlot != null && dropSlot != this)
                {
                    TransferItems(dropSlot);
                }
            }

            _rectTransform.anchoredPosition = _originalPosition;
        }

        private void TransferItems(InventorySlotUI targetSlot)
        {
            (targetSlot._slot, _slot) = (_slot, targetSlot._slot);

            targetSlot.UpdateUI();
            UpdateUI();
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
