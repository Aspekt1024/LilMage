using System;
using UnityEngine;

namespace LilMage.Units
{
    public class BasicMovement : IMovement
    {
        /// <summary>
        /// Describes the movement behaviour
        /// </summary>
        [Serializable]
        public struct Settings
        {
            public float forwardSpeed;
            public float reverseSpeed;
            public float strafeSpeed;
        }
        
        private readonly Rigidbody body;
        private readonly Settings settings;
        
        public BasicMovement(Rigidbody body, Settings settings)
        {
            this.body = body;
            this.settings = settings;
        }
        
        public void Move(Vector3 direction)
        {
            // This implementation of movement will only work on ground-plane movement,
            // ignoring vertical movement.
            direction = direction.normalized;

            var tf = body.transform;
            
            var xVel = settings.strafeSpeed * direction.x * tf.right;
            var zVel = (direction.z > 0 ? settings.forwardSpeed : settings.reverseSpeed) * direction.z * tf.forward;
            var vel = xVel + zVel;
            vel.y = body.velocity.y;

            body.velocity = vel;
        }

        public void Stop()
        {
            var vel = body.velocity;
            vel.x = 0;
            vel.z = 0;
            body.velocity = vel;
        }
    }
}