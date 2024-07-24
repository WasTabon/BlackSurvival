using System;
using DarkSurvival.Scripts.UI.Scripts;
using UnityEngine;

namespace DarkSurvival.Scripts.InputSystem
{
    public class InputManager
    {
        public event Action JumpPerformed;
        public event Action InteractPerformed;
        
        public UIController UiController { get; private set; }
        
        public float VerticalKeyboard { get; private set; }
        public float HorizontalKeyboard { get; private set; }
        
        public bool IsRunning { get; private set; }
        
        public float VerticalMouse{ get; private set; }
        public float HorizontalMouse { get; private set; }
    
        public void Initialize(UIController uiController)
        {
            UiController = uiController;
            
            UiController.InputPressed += SetInputKeyboard;
            UiController.InputCanceled += CancelMove;
            UiController.MouseMoved += SetInputMouse;
            UiController.MouseMoveCanceled += CancelMouse;
            UiController.RunningPressed += SetRunning;
            UiController.RunningCanceled += CancelRunning;

            UiController.JumpPerformed += SetJump;

            UiController.InteractWithObject += SetInteractWithObject;
        }

        private void SetInputKeyboard(Vector2 value)
        {
            VerticalKeyboard = value.y;
            HorizontalKeyboard = value.x;
        }
        
        private void SetInputMouse(Vector2 value)
        {
            VerticalMouse = value.y;
            HorizontalMouse = value.x;
        }

        private void SetRunning()
        {
            IsRunning = true;
        }

        private void CancelMove()
        {
            VerticalKeyboard = 0;
            HorizontalKeyboard = 0;
        }
        private void CancelMouse()
        {
            VerticalMouse = default;
            HorizontalMouse = default;
        }

        private void CancelRunning()
        {
            IsRunning = false;
        }

        private void SetJump()
        {
            JumpPerformed?.Invoke();
        }

        private void SetInteractWithObject()
        {
            InteractPerformed?.Invoke();
        }
    }
}