using DarkSurvival.Scripts.Systems.Management.Cursor;
using UnityEngine;

namespace DarkSurvival.Scripts.UI.Scripts
{
    public class UIInventoryPanel
    {
        private readonly UIController _uiController;
        private readonly UIView _uiView;
        private readonly CursorController _cursorController;
        
        private bool _isInventoryOpen;
        
        public UIInventoryPanel(UIView uiView, CursorController cursorController, UIController uiController)
        {
            _uiController = uiController;
            _uiView = uiView;
            _cursorController = cursorController;
        }
        
        public void ManageInventoryPanel(bool state)
        {
            _isInventoryOpen = !_isInventoryOpen;
            SetActiveStatePanel(_uiView.GetInventoryTransform, _isInventoryOpen);
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
