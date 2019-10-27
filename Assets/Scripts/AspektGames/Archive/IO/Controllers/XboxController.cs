using System.Collections.Generic;
using UnityEngine;

namespace Aspekt.IO
{
    /// <summary>
    /// A button on the Xbox controller that can be pressed. Excludes triggers/axis inputs
    /// </summary>
    public enum XboxButton
    {
        A, B, X, Y,
        BumperL, BumperR,
        Back, Start,
        StickL, StickR,
    }

    /// <summary>
    /// An axis input (trigger, stick) on the Xbox controller
    /// </summary>
    public enum XboxAxis
    {
        TriggerL, TriggerR,
        StickLHorizontal, StickLVertical,
        StickRHorizontal, StickRVertical,
    }

    public class XboxController<T> : ControllerBase<T>
    {
        private readonly Dictionary<XboxAxis, float> axisValues = new Dictionary<XboxAxis, float>();
        
        private bool movedByAxis;
        private bool shotByAxis;
        
        /// <summary>
        /// Contains the mapping and conditions for binding an Xbox axis input to an action
        /// </summary>
        public struct AxisBinding
        {
            public XboxAxis Axis;
            public string Label;
            public float Threshold;
            public T Action;

            public AxisBinding(XboxAxis axis, string label, float threshold, T action)
            {
                Axis = axis;
                Label = label;
                Threshold = threshold;
                Action = action;
            }
        }
        
        private readonly List<AxisBinding> axisBindings = new List<AxisBinding>();
        private readonly Dictionary<T, XboxAxis> axisMappings = new Dictionary<T, XboxAxis>();

#if UNITY_STANDALONE_WIN || UNITY_WEBGL
        // These key bindings are for windows
        // see http://wiki.unity3d.com/index.php?title=Xbox360Controller
        private readonly Dictionary<XboxButton, KeyCode> buttons = new Dictionary<XboxButton, KeyCode>()
        {
            { XboxButton.A, KeyCode.JoystickButton0 },
            { XboxButton.B, KeyCode.JoystickButton1 },
            { XboxButton.X, KeyCode.JoystickButton2 },
            { XboxButton.Y, KeyCode.JoystickButton3 },
            { XboxButton.BumperL, KeyCode.JoystickButton4 },
            { XboxButton.BumperR, KeyCode.JoystickButton5 },
            { XboxButton.Back, KeyCode.JoystickButton6 },
            { XboxButton.Start, KeyCode.JoystickButton7 },
            { XboxButton.StickL, KeyCode.JoystickButton8 },
            { XboxButton.StickR, KeyCode.JoystickButton9 },
        };
#else
        // TODO define other platforms
#endif

        private readonly IVirtualController<T> virtualController;

        public XboxController(IVirtualController<T> virtualController)
        {
            this.virtualController = virtualController;
        }

        public void MapButtonDown(XboxButton button, T action) => MapKeyDown(buttons[button], action);
        public void MapButtonUp(XboxButton button, T action) => MapKeyDown(buttons[button], action);
        public void MapButton(XboxButton button, T action) => MapKeyDown(buttons[button], action);

        /// <summary>
        /// Maps an Xbox controller axis to a controller label defined in Unity.
        /// </summary>
        public void MapAxis(AxisBinding binding)
        {
            axisBindings.Add(binding);
            axisMappings.Add(binding.Action, binding.Axis);
        }
        
        public override bool ProcessInput()
        {
            bool inputReceived = false;
            foreach (var binding in KeyDownBindings)
            {
                if (!Input.GetKeyDown(binding.Key)) continue;
                inputReceived = true;
                virtualController.InputReceived(binding.Value);
            }

            foreach (var binding in KeyUpBindings)
            {
                if (!Input.GetKeyUp(binding.Key)) continue;
                inputReceived = true;
                virtualController.InputReceived(binding.Value);
            }

            foreach (var binding in KeyBindings)
            {
                if (!Input.GetKey(binding.Key)) continue;
                inputReceived = true;
                virtualController.InputReceived(binding.Value);
            }

            foreach (var binding in axisBindings)
            {
                var axisValue = Input.GetAxis(binding.Label);
                if (binding.Threshold >= 0)
                {
                    if (axisValue > binding.Threshold && axisValues[binding.Axis] < binding.Threshold)
                    {
                        virtualController.AxisTriggered(binding.Action);
                    }
                    else if (axisValue < binding.Threshold && axisValues[binding.Axis] > binding.Threshold)
                    {
                        virtualController.AxisReleased(binding.Action);
                    }
                }
                else
                {
                    if (axisValue <= binding.Threshold && axisValues[binding.Axis] > binding.Threshold)
                    {
                        virtualController.AxisTriggered(binding.Action);
                    }
                    else if (axisValue > binding.Threshold && axisValues[binding.Axis] < binding.Threshold)
                    {
                        virtualController.AxisReleased(binding.Action);
                    }
                }

                axisValues[binding.Axis] = axisValue;
            }

            return inputReceived;
        }

        public override Vector2 GetDirection(T xAxis, T yAxis, Vector2 relativeToPoint, bool invertYAxis)
        {
            return GetDirection(xAxis, yAxis, invertYAxis);
        }

        public override Vector2 GetDirection(T xAxis, T yAxis, bool invertYAxis)
        {
            var aimAxis = new Vector2(axisValues[axisMappings[xAxis]], axisValues[axisMappings[xAxis]]);
            var aimDirection = Vector2.zero;
            if (aimAxis.magnitude > 0.3f)
            {
                aimDirection = new Vector2(aimAxis.x, (invertYAxis ? -1 : 1) * aimAxis.y);
            }

            return aimDirection;
        }
    }
}

