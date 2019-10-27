using UnityEngine;

namespace LilMage
{
    /// <summary>
    /// Manages the gameplay
    /// </summary>
    public class GameplayManager : IManager
    {

        public enum States
        {
            None, Running
        }
        public States GameState { get; private set; } = States.None;
        
        public void Init()
        {
        }

        public void Start()
        {
            GameState = States.Running;
            Debug.Log("Gameplay started");
        }

    }
}