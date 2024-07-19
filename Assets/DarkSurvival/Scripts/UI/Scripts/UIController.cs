using System;
using DarkSurvival.Data.Serializables.Player;
using DarkSurvival.Data.Serializables.UI;
using UnityEngine;
using DarkSurvival.Scripts.Interfaces;
using DarkSurvival.Scripts.Systems.DI;
using DarkSurvival.Scripts.Systems.Utils.JsonLoader;
using TMPro;

namespace DarkSurvival.Scripts.UI.Scripts
{
    public class UIController : IUpdatable
    {
        public event Action<Vector2> InputPressed;
        public event Action InputCanceled;
        public event Action RunningPressed;
        public event Action RunningCanceled;
        
        public event Action<Vector2> MouseMoved;
        public event Action MouseMoveCanсeled;

        public event Action JumpPerformed;
        
        [Inject] private InputControls _inputControls;
        [Inject] private UIView _uiView;

        private UITextData _uiTextData;
        
        private TextMeshProUGUI _canCollectText;
        
        private Vector2 _mousePosition;

        private string _collectTextCheck;
        
        private bool _canCollectItem;
        
        private float _mouseX;
        private float _mouseY;

        public void Initialize()
        {
            _uiTextData = JsonLoader.LoadJsonFile<UITextData>("UITextData");
            
            _canCollectText = _uiView.GetCollectItemText;
            
            _inputControls.Player.Move.performed += ctx => OnMovePerformed(ctx.ReadValue<Vector2>());
            _inputControls.Player.Move.canceled += _ => OnMoveCanceled();

            _inputControls.Player.Run.performed += _ => OnRunningPerformed();
            _inputControls.Player.Run.canceled += _ => OnRunningCanceled();
            
            _inputControls.Player.MouseX.performed += ctx => _mouseX = ctx.ReadValue<float>();
            _inputControls.Player.MouseY.performed += ctx => _mouseY = ctx.ReadValue<float>();
            _inputControls.Player.MouseX.canceled += _ => OnMoveCanceled();

            _inputControls.Player.Jump.performed += _ => OnJumpPerformed();
            
            _mousePosition = new Vector2();
        }
        
        public void Update()
        {
            _mousePosition.x = _mouseX;
            _mousePosition.y = _mouseY;
            
            OnMouseMovePerformed(_mousePosition);
        }
        
        public void ManageCanCollectAItemText(bool state, string itemName)
        {
            SetActiveState(state);
            SetText(state, itemName);
            _canCollectItem = state;
        }

        private void SetActiveState(bool state)
        {
            if (_canCollectText.gameObject.activeSelf != state)
            {
                _canCollectText.gameObject.SetActive(state);
            }
        }

        private void SetText(bool state, string itemName)
        {
            if (!state || string.IsNullOrEmpty(itemName)) return;
    
            string newText = $"{_uiTextData.CollectText} {itemName}";
            if (_canCollectText.text != newText)
            {
                _canCollectText.text = newText;
            }
        }
        
        private void OnMovePerformed(Vector2 movement)
        {
            InputPressed?.Invoke(movement);
        }
        private void OnMoveCanceled()
        {
            InputCanceled?.Invoke();
        }
        
        private void OnRunningPerformed()
        {
            RunningPressed?.Invoke();
        }
        private void OnRunningCanceled()
        {
            RunningCanceled?.Invoke();
        }
        
        private void OnMouseMovePerformed(Vector2 movement)
        {
            MouseMoved?.Invoke(movement);
        }
        private void OnMouseCanceled()
        {
            MouseMoveCanсeled?.Invoke();
        }

        private void OnJumpPerformed()
        {
            JumpPerformed?.Invoke();
        }
    }
}
