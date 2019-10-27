using Aspekt.UI;
using LilMage.Units;
using TMPro;
using UnityEngine;

namespace LilMage
{
    public sealed class UnitUI : UIPanel
    {
#pragma warning disable 649
        [SerializeField] private TextMeshProUGUI unitName;
#pragma warning restore 649

        public struct Details
        {
            public IUnit Unit;
        }
        
        public void Populate(Details details)
        {
            unitName.text = details.Unit.Name;
        }
    }
}