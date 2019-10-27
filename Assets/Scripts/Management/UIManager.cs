using Aspekt.UI;
using UnityEngine;

namespace LilMage
{
    /// <summary>
    /// Manages the User Interface
    /// </summary>
    public class UIManager : MonoBehaviour, IManager
    {
        public Canvas topCanvas;
        
#pragma warning disable 649
        [SerializeField] private UIPanel[] uiPanels;
#pragma warning restore 649
        
        public void Init()
        {
            foreach (var panel in uiPanels)
            {
                panel.Init();
            }
        }

        public T Get<T>() where T : UIPanel
        {   
            foreach (var uiPanel in uiPanels)
            {
                if (uiPanel is T panel)
                {
                    return panel;
                }
            }

            Debug.LogError("Unable to find UI: " + typeof(T).Name);
            return default;
        }
        
        public void CloseAll()
        {
            foreach (var panel in uiPanels)
            {
                panel.Close();
            }
        }

        public void CloseAllImmediate()
        {
            foreach (var panel in uiPanels)
            {
                panel.CloseImmediate();
            }
        }
    }
}