using DarkSurvival.Scripts.InputSystem;
using UnityEngine;

namespace DarkSurvival.Scripts.Gameplay.Player
{
    public class PlayerCameraController
    {
        private readonly Transform _playerTransform;
        private readonly Transform _headTransform;
        private readonly InputManager _inputManager;
        
        private float _verticalRotation = 0.0f;
        private const float _verticalRotationLimit = 90.0f;

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
            float sensitivity = 0.5f; // Коэффициент чувствительности для горизонтального вращения.
            float horizontalRotation = _inputManager.HorizontalMouse * sensitivity;
    
            _playerTransform.Rotate(Vector3.up, horizontalRotation);
        }

        private void RotateHead()
        {
            float sensitivity = 0.4f; // Коэффициент чувствительности. Можно настроить по своему усмотрению.
            float verticalRotation = _inputManager.VerticalMouse * sensitivity;
    
            _verticalRotation -= verticalRotation;
            _verticalRotation = Mathf.Clamp(_verticalRotation, -_verticalRotationLimit, _verticalRotationLimit);
    
            _headTransform.localRotation = Quaternion.Euler(_verticalRotation, 0, 0);
        }
    }
}
