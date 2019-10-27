using UnityEngine;

namespace Aspekt.IO
{
    public interface IControllerInputHandler<T>
    {
        /// <summary>
        /// Processes the input for the controller and returns true if this controller received input
        /// </summary>
        bool ProcessInput();

        /// <summary>
        /// Returns a direction vector relative to a specified point
        /// </summary>
        Vector2 GetDirection(T xAxis, T yAxis, Vector2 relativeToPoint, bool invertYAxis);
        
        /// <summary>
        /// Returns a direction vector
        /// </summary>
        Vector2 GetDirection(T xAxis, T yAxis, bool invertYAxis);
    }
}