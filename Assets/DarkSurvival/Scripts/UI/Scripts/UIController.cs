using System;
using System.Collections.Generic;
using System.Linq;
using DarkSurvival.Data.Serializables.Player;
using DarkSurvival.Data.Serializables.UI;
using UnityEngine;
using DarkSurvival.Scripts.Interfaces;
using DarkSurvival.Scripts.Systems.DI;
using DarkSurvival.Scripts.Systems.Management.Cursor;
using DarkSurvival.Scripts.Systems.Utils.JsonLoader;
using DarkSurvival.Scripts.Systems.Utils.MessageBus;
using TMPro;
using UnityEngine.UI;

namespace DarkSurvival.Scripts.UI.Scripts
{
    public class UIController : IUpdatable
    {
        public event Action<Vector2> InputPressed;
        public event Action InputCanceled;
        public event Action RunningPressed;
        public event Action RunningCanceled;
        
        public event Action<Vector2> MouseMoved;
        public event Action MouseMoveCanceled;

        public event Action JumpPerformed;
        public event Action<bool> InventoryOpen;
        public event Action InteractWithObject;
        
        [Inject] private InputControls _inputControls;
        [Inject] private UIView _uiView;
        [Inject] private CursorController _cursorController;

        private UICraftPanel _uiCraftPanel;
        
        private Dictionary<string, RectTransform> _ineractablePanels;
        
        private UITextData _uiTextData;
        private TextMeshProUGUI _canInteractText;
        
        private Vector2 _mousePosition;
        private bool _canCollectItem;
        private bool _isInventoryOpen;
        private float _mouseX;
        private float _mouseY;

        public void Initialize()
        {
            _uiTextData = JsonLoader.LoadJsonFile<UITextData>("UITextData");
            _canInteractText = _uiView.GetCollectItemText;
            
            SetupInputActions();

            InventoryOpen += ManageInventoryPanel;
            
            _ineractablePanels = new Dictionary<string, RectTransform>
            {
                { "WorkbenchPanel", _uiView.WorkbenchTransform },
            };
            
            InitializeUICraftPanel(_uiView);
            
            MessageBus.Subscribe<InteractionMessage>(HandleInteractEvent);
        }
        
        public void Update()
        {
            _mousePosition = new Vector2(_mouseX, _mouseY);
            MouseMoved?.Invoke(_mousePosition);
        }
        
        public void ManageCanCollectAItemText(bool state, string itemName, int itemsCount)
        {
            SetActiveStateText(_canInteractText.gameObject, state);

            if (state && !string.IsNullOrEmpty(itemName))
            {
                string newText = $"{_uiTextData.CollectText} {itemsCount} {itemName}";
                if (_canInteractText.text != newText)
                {
                    _canInteractText.text = newText;
                }
            }
        }

        public void ManageInteractText(bool state)
        {
            SetActiveStateText(_canInteractText.gameObject, state);

            if (state)
            {
                string newText = $"E";
                if (_canInteractText.text != newText)
                {
                    _canInteractText.text = newText;
                }
            }
        }

        private void HandleInteractEvent(InteractionMessage message)
        {
            if (_ineractablePanels.ContainsKey(message.InteractableName))
            {
                SetActiveStatePanel(_ineractablePanels[message.InteractableName].gameObject, true);
            }
        }
        
        private void ManageCursor(bool state)
        {
            if (state)
                _cursorController.LockCursor();
            else
                _cursorController.UnlockCursor();
        }
        
        private void SetupInputActions()
        {
            _inputControls.Player.Move.performed += ctx => InputPressed?.Invoke(ctx.ReadValue<Vector2>());
            _inputControls.Player.Move.canceled += _ => InputCanceled?.Invoke();

            _inputControls.Player.Run.performed += _ => RunningPressed?.Invoke();
            _inputControls.Player.Run.canceled += _ => RunningCanceled?.Invoke();
            
            _inputControls.Player.MouseX.performed += ctx => _mouseX = ctx.ReadValue<float>();
            _inputControls.Player.MouseY.performed += ctx => _mouseY = ctx.ReadValue<float>();
            _inputControls.Player.MouseX.canceled += _ => MouseMoveCanceled?.Invoke();

            _inputControls.Player.Jump.performed += _ => JumpPerformed?.Invoke();
            
            _inputControls.Player.OpenInventory.performed += _ => InventoryOpen?.Invoke(_isInventoryOpen);
            
            _inputControls.Player.InteractWithObject.performed += _ => InteractWithObject?.Invoke();
        }

        private void InitializeUICraftPanel(UIView uiView)
        {
            _uiCraftPanel = new UICraftPanel(uiView);
        }
        
        private void ManageInventoryPanel(bool state)
        {
            _isInventoryOpen = !_isInventoryOpen;
            SetActiveStatePanel(_uiView.GetInventoryTransform.gameObject, _isInventoryOpen);
        }

        private void SetActiveStatePanel(GameObject panel, bool state)
        {
            if (panel.activeSelf != state)
            {
                ManageCursor(!state);
                panel.SetActive(state);
            }
        }
        private void SetActiveStateText(GameObject textObject, bool state)
        {
            if (textObject.activeSelf != state)
            {
                textObject.SetActive(state);
            }
        }
    }
}
