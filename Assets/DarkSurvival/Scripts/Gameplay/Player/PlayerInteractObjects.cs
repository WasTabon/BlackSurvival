using System;
using System.Collections.Generic;
using DarkSurvival.Scripts.Interfaces;
using DarkSurvival.Scripts.Systems.InventorySystem;
using UnityEngine;

namespace DarkSurvival.Scripts.Gameplay.Player
{
    public class PlayerInteractObjects
    {
        public event Action<bool, string, int> OnSeeCollectable; 
        
        private readonly LayerMask _playerLayer;
        private readonly InventoryController _inventoryController;
        
        private readonly Transform _cameraTransform;

        private readonly int _maxCollectDistance;

        private readonly int _layerMask;
        
        private readonly Dictionary<Type, Action<object>> _interactionHandlers = new Dictionary<Type, Action<object>>();
        
        public PlayerInteractObjects(InventoryController inventoryController, int maxCollectDistance)
        {
            _inventoryController = inventoryController;
            _maxCollectDistance = maxCollectDistance;
            _cameraTransform = Camera.main.transform;
            
            _playerLayer = LayerMask.NameToLayer("Player");
            _layerMask = ~_playerLayer.value;
            
            RegisterHandler<ICollectable>(HandleCollectable);
            RegisterHandler<IInteractable>(HandleInteractable);
        }
        
        public void CheckForItems()
        {
            Debug.DrawRay(_cameraTransform.position, _cameraTransform.forward * _maxCollectDistance, Color.red);
            
            if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out RaycastHit hit, _maxCollectDistance, _layerMask))
            {
                if (hit.collider.TryGetComponent(out ICollectable collectable))
                {
                    OnSeeCollectable?.Invoke(true, collectable.Name, collectable.ItemsCount);
                }
                else
                {
                    OnSeeCollectable?.Invoke(false, null, 0);
                }
            }
            else
            {
                OnSeeCollectable?.Invoke(false, null, 0);
            }
        }
        
        public void UseItem()
        {
            if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out RaycastHit hit, _maxCollectDistance, _layerMask))
            {
                var component = hit.collider.gameObject;
                
                foreach (var (type, handler) in _interactionHandlers)
                {
                    if (component.TryGetComponent(type, out var targetInterface))
                    {
                        handler(targetInterface);
                        return;
                    }
                }
            }
        }
        
        private void RegisterHandler<T>(Action<T> handler) where T : class
        {
            _interactionHandlers[typeof(T)] = obj => handler((T)obj);
        }
        
        private void HandleCollectable(ICollectable collectable)
        {
            _inventoryController.AddItem(collectable.Collect(), collectable.ItemsCount);
        }
        private void HandleInteractable(IInteractable oInteractable)
        {
           
        }
    }
}
