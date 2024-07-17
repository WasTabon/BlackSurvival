using DarkSurvival.Scripts.InputSystem;
using UnityEngine;

namespace DarkSurvival.Scripts.Gameplay.Player
{
   public class PlayerMovement
   {
      private readonly float _moveSpeed;
      private readonly float _jumpForce;

      private readonly Rigidbody _rigidbody;

      private readonly InputManager _inputManager;

      private Vector3 _inputDirection;
      private Vector3 _worldDirection;
   
      public PlayerMovement(Rigidbody rigidbody, float moveSpeed, float jumpForce, InputManager inputManager)
      {
         _moveSpeed = moveSpeed;
         _jumpForce = jumpForce;
         _rigidbody = rigidbody;
         _inputManager = inputManager;
      }

      public void HandleMovement(float runMultiplier)
      {
         _inputDirection = new Vector3(_inputManager.HorizontalKeyboard, 0, _inputManager.VerticalKeyboard);
         
         _worldDirection = _rigidbody.gameObject.transform.TransformDirection(_inputDirection);

         _rigidbody.MovePosition(_rigidbody.position + _worldDirection * Time.deltaTime * _moveSpeed * runMultiplier);
      }
   }
}
