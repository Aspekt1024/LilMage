using System.Collections;
using UnityEngine;

namespace Aspekt.UI
{
    public class UIFadeAnimator : IUIAnimator
    {
        private readonly CanvasGroup canvasGroup;
        private const float FadeTime = 0.1f;
        
        public UIFadeAnimator(CanvasGroup canvasGroup)
        {
            this.canvasGroup = canvasGroup;
        }
        
        public IEnumerator AnimateIn(float delay = 0f)
        {
            yield return new WaitForSecondsRealtime(delay);
            
            float startAlpha = canvasGroup.alpha;
            float fadeTime = FadeTime * (1f - startAlpha) / 1f;
            
            float animStartTime = Time.unscaledTime;
            while (Time.unscaledTime < animStartTime + fadeTime)
            {
                canvasGroup.alpha = Mathf.Lerp(startAlpha, 1f, (Time.unscaledTime - animStartTime) / fadeTime);;
                yield return null;
            }
        }

        public IEnumerator AnimateOut(float delay = 0f)
        {
            yield return new WaitForSecondsRealtime(delay);
            
            float startAlpha = canvasGroup.alpha;
            float fadeTime = FadeTime * startAlpha;
            
            float animStartTime = Time.unscaledTime;
            while (Time.unscaledTime < animStartTime + fadeTime)
            {
                canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, (Time.unscaledTime - animStartTime) / fadeTime);;
                yield return null;
            }
        }

        public void SetClosed()
        {
            canvasGroup.alpha = 0f;
        }

        public void SetOpened()
        {
            canvasGroup.alpha = 1f;
        }
    }
}