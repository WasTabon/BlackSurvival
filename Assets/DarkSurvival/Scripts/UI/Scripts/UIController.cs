using System;
using UnityEngine;
using DarkSurvival.Scripts.Interfaces;
using DarkSurvival.Scripts.Systems.DI;

namespace DarkSurvival.Scripts.UI.Scripts
{
    public class UIController : IUpdatable
    {
        public event Action<Vector2> InputPressed;
        public event Action<Vector2> MouseMoved;
        
        [Inject] private InputControls _inputControls;

        private Vector2 _mousePosition;
        
        private float _mouseX;
        private float _mouseY;

        public void Initialize()
        {
            _inputControls.Player.Move.performed += ctx => OnMovePerformed(ctx.ReadValue<Vector2>());
            _inputControls.Player.MouseX.performed += ctx => _mouseX = ctx.ReadValue<float>();
            _inputControls.Player.MouseY.performed += ctx => _mouseY = ctx.ReadValue<float>();

            _mousePosition = new Vector2();
        }
        
        public void Update()
        {
            _mousePosition.x = _mouseX;
            _mousePosition.y = _mouseY;
            
            OnMouseMovePerformed(_mousePosition);
        }
        
        private void OnMovePerformed(Vector2 movement)
        {
            InputPressed?.Invoke(movement);
        }
        private void OnMouseMovePerformed(Vector2 movement)
        {
            MouseMoved?.Invoke(movement);
        }
    }
}
