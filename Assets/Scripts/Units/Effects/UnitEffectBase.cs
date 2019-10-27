using UnityEngine;

namespace LilMage.Units
{
    public abstract class UnitEffectBase : MonoBehaviour, IUnitEffect
    {
        private ParticleSystem[] particles;

        private void Awake()
        {
            particles = GetComponentsInChildren<ParticleSystem>();
        }

        public void Play(float duration = -1)
        {
            foreach (var particle in particles)
            {
                particle.Play();
            }
        }

        public void Stop()
        {
            foreach (var particle in particles)
            {
                particle.Stop();
            }
        }
    }
}