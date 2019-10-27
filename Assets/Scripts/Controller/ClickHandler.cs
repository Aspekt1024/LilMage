using LilMage.Units;
using UnityEngine;

namespace LilMage
{
    /// <summary>
    /// Handles the user clicking on the screen
    /// </summary>
    public class ClickHandler
    {
        private Rewired.Player player;
        
        public ClickHandler(Rewired.Player player)
        {
            this.player = player;
        }
        
        public void HandleClicks()
        {
            if (player.GetButtonDown(InputBindings.LeftClick))
            {
                CheckUnitClicked();
            }
        }

        private void CheckUnitClicked()
        {
            var mousePos = Input.mousePosition;
            var ray = Camera.main.ScreenPointToRay(mousePos);
            var mask = 1 << LayerMask.NameToLayer("Unit");

            RaycastHit hitInfo;
            var hit = Physics.SphereCast(ray, 0.2f, out hitInfo, 100f, mask);

            if (!hit)
            {
                GameManager.UI.Get<HUD>().SetTarget(null);
                return;
            }

            var unit = hitInfo.collider.GetComponent<IUnit>();
            
            GameManager.Units.PlayerHero.SetTarget(unit);
        }
    }
}