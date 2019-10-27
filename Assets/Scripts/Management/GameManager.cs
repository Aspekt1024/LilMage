using UnityEngine;

namespace LilMage
{
    /// <summary>
    /// Handles the operation of the game from initialisation to completion
    /// </summary>
    public class GameManager : Singleton<GameManager>
    {
#pragma warning disable 649
        [SerializeField] private UIManager uiManager;
#pragma warning restore 649
        
        private UnitManager unitManager;
        private GameplayManager gameplayManager;

        public static UnitManager Units => instance.unitManager;
        public static UIManager UI => instance.uiManager;
        public static GameplayManager Gameplay => instance.gameplayManager;

        private enum States
        {
            Initialising,
            Running,
            Paused,
        }

        private States state = States.Initialising;

        protected override void Init()
        {
            Debug.Log("GameManager bootstrap initiated.");
            state = States.Initialising;
            CreateAndInitializeManagers();
            Debug.Log("GameManager and Managers initialised.");
            
            gameplayManager.Start();
        }

        private void Update()
        {
            if (state == States.Initialising)
            {
                StartGame();
            }
        }

        private void StartGame()
        {
            state = States.Running;
        }

        private void CreateAndInitializeManagers()
        {
            unitManager = new UnitManager();
            gameplayManager = new GameplayManager();
            
            unitManager.Init();
            uiManager.Init();
            gameplayManager.Init();
        }
        
    }
}