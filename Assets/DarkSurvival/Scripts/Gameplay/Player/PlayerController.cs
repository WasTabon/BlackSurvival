using System;
using DarkSurvival.Scripts.InputSystem;
using DarkSurvival.Scripts.Interfaces;
using DarkSurvival.Scripts.Systems.DI;
using UnityEngine;

namespace DarkSurvival.Scripts.Gameplay.Player
{
    public class PlayerController : IUpdatable
    {
        [InjectNamed("Player")] 
        private GameObject _characterInScene;

        private event Action OnUpdateCalled;

        [Inject] 
        private InputManager _inputManager;

        private PlayerMovement _playerMovement;

        private Rigidbody _rigidbody;

        public void Initialize()
        {
            _rigidbody = _characterInScene.GetComponent<Rigidbody>();
            _playerMovement = new PlayerMovement(_rigidbody, ReadPlayerData().moveSpeed, ReadPlayerData().jumpHeight, _inputManager);
            OnUpdateCalled += _playerMovement.HandleMovement;
        }

        public void Update()
        {
            OnUpdateCalled?.Invoke();
        }
    
        private PlayerData ReadPlayerData()
        {
            return JsonLoader.LoadJsonFile<PlayerData>("PlayerData");
        }
    }
}
