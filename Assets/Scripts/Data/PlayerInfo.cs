using UnityEngine;

namespace LilMage
{
    public class PlayerInfo : MonoBehaviour
    {
        public static PlayerInfo Instance;

        public string PlayerName;

        private void OnEnable()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                if (Instance != this)
                {
                    // Reset the player info singleton
                    Destroy(Instance.gameObject);
                    Instance = this;
                }
            }
            DontDestroyOnLoad(gameObject);
        }

        public void EditName(string name)
        {
            Instance.PlayerName = name;
        }
    }
}