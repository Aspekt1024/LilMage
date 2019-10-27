using UnityEngine;

namespace LilMage.Units
{
    public interface IMovement
    {
        /// <summary>
        /// Moves the unit in a specified direction in 3D space
        /// </summary>
        void Move(Vector3 direction);

        /// <summary>
        /// Stops the directional movement of the unit
        /// </summary>
        void Stop();
    }
}