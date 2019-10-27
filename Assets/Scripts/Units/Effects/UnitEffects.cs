using System;
using System.Collections.Generic;
using UnityEngine;

namespace LilMage.Units
{
    public class UnitEffects : MonoBehaviour, IUnitEffects
    {
#pragma warning disable 649
        [SerializeField] private Transform projectileSpawnPoint;
#pragma warning restore 649
        
        private readonly Dictionary<Type, IUnitEffect> effects = new Dictionary<Type, IUnitEffect>();

        private void Start()
        {
            var unitEffects = GetComponentsInChildren<IUnitEffect>();
            foreach (var effect in unitEffects)
            {
                effects.Add(effect.GetType(), effect);
                effect.Stop();
            }
        }

        public Vector3 GetProjectileSpawnPoint() => projectileSpawnPoint.position;

        public void Play<T>(float duration = -1) where T : IUnitEffect
        {
            effects[typeof(T)].Play(duration);
        }

        public void Stop<T>() where T : IUnitEffect
        {
            effects[typeof(T)].Stop();
        }
    }
}