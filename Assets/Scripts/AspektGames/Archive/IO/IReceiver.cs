namespace Aspekt.IO
{
    /// <summary>
    /// Has the ability to receive inputs from a <see cref="VirtualController"/>
    /// </summary>
    public interface IReceiver<T>
    {
        /// <summary>
        /// Used when an input key/button is pressed
        /// </summary>
        void OnInputReceived(T action);
        
        /// <summary>
        /// Used when an axis input (a trigger or control stick) reaches its threshold value 
        /// </summary>
        void OnAxisTriggered(T action);
        
        /// <summary>
        /// Used when an axis input that was triggered is released 
        /// </summary>
        void OnAxisReleased(T action);
    }
}