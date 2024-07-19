using System;
using DarkSurvival.Scripts.Interfaces;
using DarkSurvival.Scripts.Systems.InventorySystem;
using UnityEngine;

namespace DarkSurvival.Scripts.Gameplay.Player
{
    public class PlayerCollectItems
    {
        public event Action<bool> OnSeeCollectable; 
        
        private readonly LayerMask _playerLayer;
        private readonly InventoryController _inventoryController;
        
        private readonly Transform _cameraTransform;

        private readonly int _maxCollectDistance;

        private readonly int layerMask;
        
        public PlayerCollectItems(InventoryController inventoryController, int maxCollectDistance)
        {
            _inventoryController = inventoryController;
            _maxCollectDistance = maxCollectDistance;
            _cameraTransform = Camera.main.transform;
            
            _playerLayer = LayerMask.NameToLayer("Player");
            layerMask = ~_playerLayer.value;
        }

        public void CheckForCollectables()
        {
            Debug.DrawRay(_cameraTransform.position, _cameraTransform.forward * _maxCollectDistance, Color.red);
            
            if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out RaycastHit hit, _maxCollectDistance, layerMask))
            {
                if (hit.collider.TryGetComponent(out ICollectable collectable))
                {
                    OnSeeCollectable?.Invoke(true);
                }
                else
                {
                    //OnSeeCollectable?.Invoke(false);
                }
            }
            else
            {
                OnSeeCollectable?.Invoke(false);
            }
        }
        
        public void Collect()
        {
            if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out RaycastHit hit, _maxCollectDistance, layerMask))
            {
                if (hit.collider.TryGetComponent(out ICollectable collectable))
                {
                    _inventoryController.AddItem(collectable.Collect(), 1);
                }
            }
        }
    }
}
