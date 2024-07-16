using DarkSurvival.Scripts.InputSystem;
using DarkSurvival.Scripts.Systems.DI;
using UnityEngine;

namespace DarkSurvival.Scripts.Gameplay.Player
{
   public class PlayerMovement
   {
      private readonly float _moveSpeed;
      private readonly float _jumpForce;

      private Rigidbody _rigidbody;

      [Inject] private InputManager _inputManager;

      private Vector3 _inputDirection;
   
      public PlayerMovement(Rigidbody rigidbody, float moveSpeed, float jumpForce, InputManager inputManager)
      {
         _moveSpeed = moveSpeed;
         _jumpForce = jumpForce;
         _rigidbody = rigidbody;
         _inputManager = inputManager;
      }

      public void HandleMovement()
      {
         _inputDirection = new Vector3(_inputManager.HorizontalKeyboard, 0, _inputManager.VerticalKeyboard);
      
         _rigidbody.MovePosition(_rigidbody.position + _inputDirection * Time.deltaTime * _moveSpeed);
      }
   }
}
