using System.Collections.Generic;
using DarkSurvival.Scripts.Systems.Management.Cursor;
using DarkSurvival.Scripts.Systems.Utils.MessageBus;
using UnityEngine;

namespace DarkSurvival.Scripts.UI.Scripts
{
    public class UIInteractablePanels
    {
        private readonly UIController _uiController;
        private readonly UIView _uiView;
        private readonly CursorController _cursorController;
        
        private readonly Dictionary<string, RectTransform> _ineractablePanels;
        
        public UIInteractablePanels(UIView uiView, CursorController cursorController, UIController uiController)
        {
            _uiController = uiController;
            _uiView = uiView;
            _cursorController = cursorController;
            
            _ineractablePanels = new Dictionary<string, RectTransform>
            {
                { "WorkbenchPanel", _uiView.WorkbenchTransform },
            };
            
            MessageBus.Subscribe<InteractionMessage>(HandleInteractEvent);
        }
        
        private void HandleInteractEvent(InteractionMessage message)
        {
            if (_ineractablePanels.TryGetValue(message.InteractableName, out RectTransform panel))
            {
                SetActiveStatePanel(panel, true);
            }
        }
        
        private void SetActiveStatePanel(RectTransform panel, bool state)
        {
            if (panel.gameObject.activeSelf != state)
            {
                if (state)
                {
                    _uiController.AddPanelToStack(panel);
                }
                else
                {
                    _uiController.RemovePanelFromStack();
                }
                
                ManageCursor(!state);
                panel.gameObject.SetActive(state);
            }
        }
        private void ManageCursor(bool state)
        {
            if (state)
                _cursorController.LockCursor();
            else
                _cursorController.UnlockCursor();
        }
    }
}
