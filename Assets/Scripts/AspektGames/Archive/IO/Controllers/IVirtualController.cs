using System;

namespace Aspekt.IO
{
    /// <summary>
    /// A non-physical controller that is mapped to by one or more <see cref="IControllerInputHandler{T}"/>
    /// </summary>
    /// <typeparam name="T">The input label type</typeparam>
    public interface IVirtualController<T>
    {
        /// <summary>
        /// Used when an input key/button is pressed
        /// </summary>
        void InputReceived(T action);
        
        /// <summary>
        /// Used when an axis input (a trigger or control stick) reaches its threshold value 
        /// </summary>
        void AxisTriggered(T action);
        
        /// <summary>
        /// Used when an axis input that was triggered is released 
        /// </summary>
        void AxisReleased(T action);
    }
}