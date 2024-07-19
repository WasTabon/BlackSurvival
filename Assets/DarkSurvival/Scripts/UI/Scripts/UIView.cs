using UnityEngine;

namespace DarkSurvival.Scripts.UI.Scripts
{
    public class UIView : MonoBehaviour
    {
        [SerializeField] private RectTransform _collectItemText;

        public RectTransform GetCollectItemText => _collectItemText;
    }
}
