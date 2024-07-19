using TMPro;
using UnityEngine;

namespace DarkSurvival.Scripts.UI.Scripts
{
    public class UIView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _collectItemText;
        [SerializeField] private RectTransform _inventoryTransform;

        public TextMeshProUGUI GetCollectItemText => _collectItemText;
        public RectTransform GetInventoryTransform => _inventoryTransform;
    }
}
