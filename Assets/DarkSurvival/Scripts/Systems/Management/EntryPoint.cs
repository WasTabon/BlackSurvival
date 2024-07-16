using DarkSurvival.Scripts.Gameplay.Player;
using DarkSurvival.Scripts.InputSystem;
using DarkSurvival.Scripts.Systems.DI;
using DarkSurvival.Scripts.Systems.Factory;
using DarkSurvival.Scripts.Systems.Utils;
using DarkSurvival.Scripts.UI.Scripts;
using Unity.Mathematics;
using UnityEngine;

namespace DarkSurvival.Scripts.Systems.Management
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private Transform _playerSpawnPos;
        
        private InputManager _inputManager;
        private UIController _uiController;
        private PlayerController _playerController;

        private Updater _updater;

        private InputControls _inputControls;

        private PlayerFactory _playerFactory;
        
        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            InitializeInputControls();
        
            DependencyContainer.Instance.Register(_inputControls);
        
            InitializeUIController();
            
            InitializeInputManager();
            DependencyContainer.Instance.Register(_inputManager);
        
            InitializeUpdater();
        
            _updater.RegisterUpdatable(_uiController);
        
            CreatePlayerFactory();
            SpawnPlayer();
        }

        private void InitializeInputControls()
        {
            _inputControls = new InputControls();
            _inputControls.Enable();
        }

        private void InitializeInputManager()
        {
            _inputManager = new InputManager();
            _inputManager.Initialize(_uiController);
        }

        private void InitializeUIController()
        {
            _uiController = new UIController();
            DependencyContainer.Instance.InjectDependencies(_uiController);

            _uiController.Initialize();
        }
    
        private void InitializeUpdater()
        {
            GameObject created = new GameObject("Updater");
            Updater updater = created.AddComponent<Updater>();
            updater.Initialize();
            
            _updater = updater;
        }

        private void CreatePlayerFactory()
        {
            _playerFactory = new PlayerFactory();
        }

        private void SpawnPlayer()
        {
            GameObject player = _playerFactory.GetProduct(_playerSpawnPos.position, quaternion.identity);
            DependencyContainer.Instance.Register("Player", player);
            
            _playerController = new PlayerController();
            DependencyContainer.Instance.InjectDependencies(_playerController);
            _playerController.Initialize();
            
            _updater.RegisterUpdatable(_playerController); 
        }
    }
}
