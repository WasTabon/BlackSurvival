using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace DarkSurvival.Scripts.UI.Scripts
{
    public class UICraftPanel
    {
        private readonly UIView _uiView;
        
        private Dictionary<Button, RectTransform> _craftPanels;
        
        private RectTransform _currentActiveCraftPanel;
        
        public UICraftPanel(UIView uiView)
        {
            _uiView = uiView;
            
            _craftPanels = new Dictionary<Button, RectTransform>();
            for (int i = 0; i < _uiView.GetCraftItemTypeButtons.Length; i++)
            {
                Button button = _uiView.GetCraftItemTypeButtons[i];
                RectTransform rectTransform = _uiView.GetCraftingPanels[i];

                _craftPanels[button] = rectTransform;
            }
            InitializeCraftPanel();
        }
        
        private void InitializeCraftPanel()
        {
            _currentActiveCraftPanel = _craftPanels.First().Value;
            _currentActiveCraftPanel.gameObject.SetActive(true);
            
            foreach (var kvp in _craftPanels)
            {
                Button button = kvp.Key;
                RectTransform panel = kvp.Value;
                
                button.onClick.AddListener(() => SetActiveStateCraftPanel(panel));
            }
        }
        
        private void SetActiveStateCraftPanel(RectTransform panel)
        {
            if (panel != _currentActiveCraftPanel)
            {
                _currentActiveCraftPanel.gameObject.SetActive(false);
                
                _currentActiveCraftPanel = panel;
                
                panel.gameObject.SetActive(true);
            }
        }
    }
}
