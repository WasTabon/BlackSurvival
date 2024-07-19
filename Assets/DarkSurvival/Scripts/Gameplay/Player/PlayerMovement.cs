using DarkSurvival.Scripts.InputSystem;
using UnityEngine;

namespace DarkSurvival.Scripts.Gameplay.Player
{
   public class PlayerMovement
   {
      private const float GroundCheckDistance = 2f;
      private const string LayerPlayerName = "Player";
      private readonly LayerMask _playerLayer;
       
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

         _playerLayer = LayerMask.NameToLayer(LayerPlayerName);
      }

      public void HandleMovement(float runMultiplier)
      {
         _inputDirection = new Vector3(_inputManager.HorizontalKeyboard, 0, _inputManager.VerticalKeyboard);
         
         _worldDirection = _rigidbody.gameObject.transform.TransformDirection(_inputDirection);

         _rigidbody.MovePosition(_rigidbody.position + _worldDirection * (Time.deltaTime * _moveSpeed * runMultiplier));
      }

      public void HandleJump()
      {
         Debug.Log(IsGrounded());
         if (IsGrounded())
         {
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
         }
      }

      public bool IsGrounded()
      {
         int layerMask = ~_playerLayer.value;
         
         return Physics.Raycast(_rigidbody.position, Vector3.down, GroundCheckDistance, layerMask);
      }
   }
}
