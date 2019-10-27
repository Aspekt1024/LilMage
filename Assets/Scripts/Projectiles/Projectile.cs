using System;
using UnityEngine;

namespace LilMage
{
    public class Projectile : MonoBehaviour
    {
#pragma warning disable 649
        [SerializeField] private ParticleSystem projectile;
        [SerializeField] private ParticleSystem explosion;
#pragma warning restore 649
        
        public struct Settings
        {
            public readonly Vector3 startPosition;
            public readonly Transform target;
            public readonly float speed;
            public readonly bool followTarget;

            public Settings(float speed, Transform target, Vector3 startPosition, bool followTarget = true)
            {
                this.startPosition = startPosition;
                this.speed = speed;
                this.target = target;
                this.followTarget = followTarget;
            }
        }
        
        public event Action<Projectile> OnTargetHit = delegate { };

        private Rigidbody body;
        private Settings settings;

        private void Awake()
        {
            body = GetComponent<Rigidbody>();
        }

        public void Cast(Settings settings)
        {
            this.settings = settings;
            transform.position = settings.startPosition;
            transform.LookAt(settings.target);
        }

        private void FixedUpdate()
        {
            if (!settings.followTarget) return;
            
            transform.LookAt(settings.target);
            var vel = transform.forward * settings.speed;
            body.velocity = vel;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform == settings.target)
            {
                OnTargetHit?.Invoke(this);
                Explode();
            }
        }

        private void Explode()
        {
            projectile.Stop();
            explosion.Play();
            Destroy(gameObject, 1f);
        }
    }
}