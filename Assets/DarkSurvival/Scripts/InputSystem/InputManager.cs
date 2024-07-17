using System;
using DarkSurvival.Scripts.UI.Scripts;
using UnityEngine;

namespace DarkSurvival.Scripts.InputSystem
{
    public class InputManager
    {
        public event Action MovePerformed;
    
        public float VerticalKeyboard { get; private set; }
        public float HorizontalKeyboard { get; private set; }
        
        public bool IsRunning { get; private set; }
        
        public float VerticalMouse{ get; private set; }
        public float HorizontalMouse { get; private set; }
    
        public void Initialize(UIController uiController)
        {
            uiController.InputPressed += SetInputKeyboard;
            uiController.InputCanceled += CancelMove;
            uiController.MouseMoved += SetInputMouse;
            uiController.MouseMoveCanсeled += CancelMouse;
            uiController.RunningPressed += SetRunning;
            uiController.RunningCanceled += CancelRunning;
        }

        private void SetInputKeyboard(Vector2 value)
        {
            VerticalKeyboard = value.y;
            HorizontalKeyboard = value.x;
        
            MovePerformed?.Invoke();
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
            VerticalMouse = 0;
            HorizontalMouse = 0;
        }

        private void CancelRunning()
        {
            IsRunning = false;
        }
    }
}