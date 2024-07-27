using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DarkSurvival.Scripts.UI.Scripts
{
    public class UIView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _collectItemText;
        
        [SerializeField] private RectTransform _inventoryTransform;
        
        [SerializeField] private RectTransform _workbenchTransform;
        [SerializeField] private Button[] _craftItemTypeButtons;
        [SerializeField] private RectTransform[] _craftingPanels;

        public TextMeshProUGUI GetCollectItemText => _collectItemText;
        public RectTransform GetInventoryTransform => _inventoryTransform;
        public RectTransform WorkbenchTransform => _workbenchTransform;
        public Button[] GetCraftItemTypeButtons => _craftItemTypeButtons;
        public RectTransform[] GetCraftingPanels => _craftingPanels;
    }
}
