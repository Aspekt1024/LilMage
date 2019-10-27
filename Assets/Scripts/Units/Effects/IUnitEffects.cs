
using UnityEngine;

namespace LilMage.Units
{
    public interface IUnitEffects
    {
        void Play<T>(float duration = -1f) where T : IUnitEffect;
        void Stop<T>() where T : IUnitEffect;

        Vector3 GetProjectileSpawnPoint();
    }
}