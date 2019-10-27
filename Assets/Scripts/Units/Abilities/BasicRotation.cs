using System;
using UnityEngine;

namespace LilMage.Units
{
    public class BasicRotation : IRotation
    {
        [Serializable]
        public struct Settings
        {
            public float RotationSpeed;
        }

        private readonly Settings settings;
        private readonly Rigidbody body;
        
        public BasicRotation(Rigidbody body, Settings settings)
        {
            this.body = body;
            this.settings = settings;
        }

        public void Rotate(float value)
        {
            value *= Mathf.PI / 180f;
            body.transform.Rotate(Vector3.up, value * settings.RotationSpeed);
        }
    }
}