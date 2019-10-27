using Aspekt.UI;
using LilMage.Units;
using UnityEngine;

namespace LilMage
{
    public class HUD : UIPanel
    {
#pragma warning disable 649
        [SerializeField] private UnitPanel playerPanel;
        [SerializeField] private UnitPanel targetPanel;
#pragma warning restore 649

        public IUnit currentPlayer;
        public IUnit currentTarget;
        
        public UnitPanel PlayerPanel => playerPanel;
        public UnitPanel TargetPanel => targetPanel;

        private void Start()
        {
            if (currentTarget == null)
            {
                targetPanel.Hide();
            }
        }

        public void SetPlayer(IUnit player)
        {
            currentPlayer = player;
            
            playerPanel.SetUnit(player);
        }

        public void SetTarget(IUnit target)
        {
            currentTarget = target;
            if (currentTarget == null)
            {
                targetPanel.Hide();
                return;
            }
            targetPanel.SetUnit(target);
            targetPanel.Show();
        }
    }
}