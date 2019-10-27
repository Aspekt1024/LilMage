using UnityEngine;

namespace LilMage
{
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
		public static T Instance;

        protected static T instance;

        protected abstract void Init();
        
		private void Awake()
		{
			if (instance != null)
			{
				Debug.LogError($"{typeof(T)} has already been instantiated. Destroying.");
				Destroy(gameObject);
				return;
			}
			
			instance = (T)this;
			Init();
		}
    }
}