using DarkSurvival.Scripts.Interfaces;
using DarkSurvival.Scripts.Systems.InventorySystem;
using UnityEngine;

namespace DarkSurvival.Scripts.Gameplay.Player
{
    public class PlayerCollectItems
    {
        private readonly LayerMask _playerLayer;
        private readonly InventoryController _inventoryController;
        
        private readonly Transform _cameraTransform;

        private readonly int _maxCollectDistance;
        
        
        public PlayerCollectItems(InventoryController inventoryController, int maxCollectDistance)
        {
            _inventoryController = inventoryController;
            _maxCollectDistance = maxCollectDistance;
            _cameraTransform = Camera.main.transform;
            
            _playerLayer = LayerMask.NameToLayer("Player");
        }

        public void Collect()
        {
            int layerMask = ~_playerLayer.value;

            Debug.DrawRay(_cameraTransform.position, _cameraTransform.forward * _maxCollectDistance, Color.red);
            
            if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out RaycastHit hit, _maxCollectDistance, layerMask))
            {
                if (hit.collider.TryGetComponent(out ICollectable collectable))
                {
                    Debug.Log(collectable.Collect().Name);
                }
            }
        }
    }
}
