using System;
using DarkSurvival.Data.Serializables.Player;
using DarkSurvival.Scripts.InputSystem;
using DarkSurvival.Scripts.Interfaces;
using DarkSurvival.Scripts.Systems.DI;
using DarkSurvival.Scripts.Systems.InventorySystem;
using DarkSurvival.Scripts.Systems.Management.Cursor;
using DarkSurvival.Scripts.Systems.Utils.JsonLoader;
using UnityEngine;

namespace DarkSurvival.Scripts.Gameplay.Player
{
    public class PlayerController : IUpdatable
    {
        private event Action<float> OnUpdateCalledFloat;
        private event Action OnUpdateCalled;
        
        [InjectNamed("Player")] 
        private GameObject _characterInScene;

        [Inject] private InputManager _inputManager;
        [Inject] private InventoryController _inventoryController;
        [Inject] private CursorController _cursorController;

        private float _runningMultiplier;
        
        private PlayerMovement _playerMovement;
        private PlayerCameraController _cameraController;
        private PlayerCollectItems _playerCollectItems;

        private Rigidbody _rigidbody;

        public void Initialize()
        {
            _rigidbody = _characterInScene.GetComponent<Rigidbody>();
            Transform headTransform = _characterInScene.transform.Find("Head");
            
            _playerMovement = new PlayerMovement(_rigidbody, ReadPlayerData().moveSpeed, ReadPlayerData().jumpHeight, _inputManager);
            _cameraController = new PlayerCameraController(_characterInScene.transform, headTransform, _inputManager, _cursorController);
            _playerCollectItems = new PlayerCollectItems(_inventoryController, ReadPlayerData().MaxCollectDistance);
            
            _runningMultiplier = ReadPlayerData().runSpeedMultiplier;

            _playerCollectItems.OnSeeCollectable += _inputManager.UiController.ManageCanCollectAItemText;
            OnUpdateCalledFloat += _playerMovement.HandleMovement;
            OnUpdateCalled += _cameraController.UpdateCamera;
            OnUpdateCalled += _playerCollectItems.CheckForCollectables;
            _inputManager.JumpPerformed += _playerMovement.HandleJump;
            _inputManager.CollectItemPerformed += _playerCollectItems.Collect;
        }

        public void Update()
        {
            OnUpdateCalled?.Invoke();
            
            if (_inputManager.IsRunning)
                OnUpdateCalledFloat?.Invoke(_runningMultiplier);
            else
                OnUpdateCalledFloat?.Invoke(1);
        }
    
        private PlayerData ReadPlayerData()
        {
            return JsonLoader.LoadJsonFile<PlayerData>("PlayerData");
        }
    }
}
