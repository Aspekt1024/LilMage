using UnityEngine;

namespace LilMage.Units
{
    public interface IUnitEffect
    {
        /// <summary>
        /// Plays the effect, with an optional duration modifier
        /// </summary>
        /// <param name="duration"></param>
        void Play(float duration = -1f);

        /// <summary>
        /// Stops the effect
        /// </summary>
        void Stop();
    }
}