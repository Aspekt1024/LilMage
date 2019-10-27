using UnityEngine;

namespace Aspekt.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIElement : MonoBehaviour, IUIElement
    {
        private enum States
        {
            Hidden, Visible
        }
        private States state;

        private CanvasGroup canvas;

        private void Awake()
        {
            canvas = GetComponent<CanvasGroup>();
            Show();
        }

        public void Show()
        {
            canvas.blocksRaycasts = true;
            canvas.alpha = 1f;
            state = States.Visible;
        }

        public void Hide()
        {
            canvas.blocksRaycasts = false;
            canvas.alpha = 0f;
            state = States.Hidden;
        }
    }
}