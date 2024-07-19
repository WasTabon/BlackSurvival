using TMPro;
using UnityEngine;

namespace DarkSurvival.Scripts.UI.Scripts
{
    public class UIView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _collectItemText;

        public TextMeshProUGUI GetCollectItemText => _collectItemText;
    }
}
