using DarkSurvival.Scripts.InputSystem;
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
        
        private float _verticalRotation;
        private float _horizontalRotation;
        
        public PlayerCameraController(Transform playerTransform, Transform headTransform, InputManager inputManager)
        {
            _playerTransform = playerTransform;
            _headTransform = headTransform;
            _inputManager = inputManager;
        }

        public void UpdateCamera()
        {
            RotatePlayer();
            RotateHead();
        }

        private void RotatePlayer()
        {
            _horizontalRotation = _inputManager.HorizontalMouse * HorizontalSenitivity;
    
            Debug.Log($"Horizontal Rotation: {_horizontalRotation}");
            
            if (Mathf.Abs(_horizontalRotation) < 0.1f)
            {
                Vector3 currentRotation = _playerTransform.rotation.eulerAngles;
                _playerTransform.rotation = Quaternion.Euler(new Vector3(currentRotation.x, Mathf.Round(currentRotation.y), currentRotation.z));
                Debug.Log($"Current rotation: {currentRotation}");
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
