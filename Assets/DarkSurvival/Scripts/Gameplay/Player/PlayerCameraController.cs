using DarkSurvival.Scripts.InputSystem;
using DarkSurvival.Scripts.Systems.Management.Cursor;
using UnityEngine;

namespace DarkSurvival.Scripts.Gameplay.Player
{
    public class PlayerCameraController
    {
        private const float _verticalRotationLimit = 90.0f;

        private const float HorizontalSenitivity = 0.3f;
        private const float VerticalSenitivity = 0.2f;
        
        private readonly Transform _playerTransform;
        private readonly Transform _headTransform;
        private readonly InputManager _inputManager;
        private readonly CursorController _cursorController;
        
        private float _verticalRotation;
        private float _horizontalRotation;
        private float _currentYRotation;
        
        public PlayerCameraController(Transform playerTransform, Transform headTransform, InputManager inputManager, CursorController cursorController)
        {
            _playerTransform = playerTransform;
            _headTransform = headTransform;
            _inputManager = inputManager;
            _cursorController = cursorController;
        }

        public void UpdateCamera()
        {
            if (_cursorController.CanSeeCursor == false)
            {
                RotatePlayer();
                RotateHead();   
            }
        }

        private void RotatePlayer()
        {
            _horizontalRotation = _inputManager.HorizontalMouse * HorizontalSenitivity;
            
            if (Mathf.Abs(_horizontalRotation) < 0.1f)
            {
                _currentYRotation = _playerTransform.eulerAngles.y;

                _currentYRotation = (int)(_currentYRotation + 0.5f);
                
                _playerTransform.rotation = Quaternion.Euler(0f, _currentYRotation, 0f);
                
                return;
            }
            
            _playerTransform.Rotate(Vector3.up, _horizontalRotation);
        }

        private void RotateHead()
        {
            float verticalRotation = _inputManager.VerticalMouse * VerticalSenitivity;
    
            _verticalRotation -= verticalRotation;
            _verticalRotation = Mathf.Clamp(_verticalRotation, -_verticalRotationLimit, _verticalRotationLimit);
    
            _headTransform.localRotation = Quaternion.Euler(_verticalRotation, 0, 0);
        }
    }
}
