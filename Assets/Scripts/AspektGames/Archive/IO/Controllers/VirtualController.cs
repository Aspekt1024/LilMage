using System.Collections.Generic;
using UnityEngine;

namespace Aspekt.IO
{
    public class VirtualController<T> : IVirtualController<T>
    {
        private readonly IReceiver<T> receiver;
        private readonly List<IControllerInputHandler<T>> controllers = new List<IControllerInputHandler<T>>();
        private IControllerInputHandler<T> currentController;

        public VirtualController(IReceiver<T> receiver)
        {
            this.receiver = receiver;
        }

        public void AddController(IControllerInputHandler<T> controller)
        {
            controllers.Add(controller);
        }

        public void RemoveController(IControllerInputHandler<T> controller)
        {
            controllers.Remove(controller);
        }

        public void CheckForInput()
        {
            foreach (var controller in controllers)
            {
                bool receivedInput = controller.ProcessInput();
                if (receivedInput)
                {
                    currentController = controller;
                    return;
                }
            }
        }


        public void InputReceived(T action) => receiver.OnInputReceived(action);

        public void AxisTriggered(T action) => receiver.OnInputReceived(action);

        public void AxisReleased(T action) => receiver.OnInputReceived(action);
        
        public Vector2 GetDirection(T xAxis, T yAxis, Vector2 relativeToPoint, bool invertYAxis)
        {
            foreach (var controller in controllers)
            {
                if (controller == currentController)
                {
                    return controller.GetDirection(xAxis, yAxis, relativeToPoint, invertYAxis);
                }
            }
            return Vector2.zero;
        }

        public Vector2 GetDirection(T xAxis, T yAxis, bool invertYAxis)
        {
            foreach (var controller in controllers)
            {
                if (controller == currentController)
                {
                    return controller.GetDirection(xAxis, yAxis, invertYAxis);
                }
            }
            return Vector2.zero;
        }
    }
}

