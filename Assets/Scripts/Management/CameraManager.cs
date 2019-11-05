using LilMage.Units;
using UnityEngine;

namespace LilMage
{
    public class CameraManager : IManager
    {
        private BasicCamera playerCam;
        
        public void Init()
        {
            playerCam = Object.FindObjectOfType<BasicCamera>();
        }

        public void SetPlayerCamera(UnitBase unit)
        {
            playerCam.StartTracking(unit.transform);
        }
        
    }
}