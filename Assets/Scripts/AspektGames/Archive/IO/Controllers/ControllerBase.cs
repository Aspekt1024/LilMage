using System.Collections.Generic;
using UnityEngine;

namespace Aspekt.IO
{
    public abstract class ControllerBase<T> : IControllerInputHandler<T>
    {
        protected Dictionary<KeyCode, T> KeyDownBindings { get; } = new Dictionary<KeyCode, T>();
        protected Dictionary<KeyCode, T> KeyUpBindings { get; } = new Dictionary<KeyCode, T>();
        protected Dictionary<KeyCode, T> KeyBindings { get; } = new Dictionary<KeyCode, T>();

        public abstract bool ProcessInput();
        public abstract Vector2 GetDirection(T xAxis, T yAxis, Vector2 relativeToPoint, bool invertYAxis);

        public abstract Vector2 GetDirection(T xAxis, T yAxis, bool invertYAxis);

        protected void MapKeyDown(KeyCode key, T action)
        {
            if (KeyDownBindings.ContainsKey(key)) return;
            KeyDownBindings.Add(key, action);
            // TODO report unbound action
        }

        protected void MapKeyUp(KeyCode key, T action)
        {
            if (KeyUpBindings.ContainsKey(key)) return;
            KeyUpBindings.Add(key, action);
            // TODO report unbound action
        }
        
        protected void MapKey(KeyCode key, T action)
        {
            if (KeyBindings.ContainsKey(key)) return;
            KeyBindings.Add(key, action);
            // TODO report unbound action
        }

    }
}