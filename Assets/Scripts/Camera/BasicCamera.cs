using LilMage.Units;
using UnityEngine;

namespace LilMage
{
    /// <summary>
    /// A basic camera that follows the attached unit
    /// </summary>
    public class BasicCamera : MonoBehaviour
    {
        private Transform trackedTf;

        private enum States
        {
            None, Tracking
        }
        private States state = States.None;

        public void StartTracking(Transform tf)
        {
            state = States.Tracking;
            trackedTf = tf;
        }
        
        private void LateUpdate()
        {
            if (state != States.Tracking) return;

            var offset = trackedTf.forward * -10;
            offset.y = 5f;
            transform.position = trackedTf.transform.position + offset;
            
            transform.LookAt(trackedTf);
        }
    }
}