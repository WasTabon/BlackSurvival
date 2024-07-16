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
        
        public float VerticalMouse{ get; private set; }
        public float HorizontalMouse { get; private set; }
    
        public void Initialize(UIController uiController)
        {
            uiController.InputPressed += SetInputKeyboard;
            uiController.MouseMoved += SetInputMouse;
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
    }
}