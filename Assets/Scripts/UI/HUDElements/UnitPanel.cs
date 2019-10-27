using Aspekt.UI;
using LilMage.Units;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LilMage
{
    public class UnitPanel : UIElement
    {
#pragma warning disable 649
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private TextMeshProUGUI manaText;
        [SerializeField] private Image healthBar;
        [SerializeField] private Image manaBar;
        [SerializeField] private Image castbar;
#pragma warning restore 649

        private IUnit currentUnit;

        public void SetUnit(IUnit unit)
        {
            if (unit == currentUnit) return;

            if (currentUnit != null) UnsetUnit();
            if (unit == null) return;

            currentUnit = unit;
            
            SetName(unit);
            SetHealth(unit);
            SetMana(unit);

            unit.OnHealthChanged += SetHealth;
            unit.OnManaChanged += SetMana;
            unit.Abilities.OnCastProgressChanged += SetCastProgress;

            castbar.fillAmount = 0f;
        }

        public void UnsetUnit()
        {
            if (currentUnit == null) return;
            
            currentUnit.OnHealthChanged -= SetHealth;
            currentUnit.OnManaChanged -= SetMana;
            currentUnit.Abilities.OnCastProgressChanged -= SetCastProgress;
        }

        private void SetName(IUnit unit)
        {
            nameText.text = unit.Name;
        }
        
        private void SetHealth(IUnit unit)
        {
            if (unit.CurrentHealth == 0)
            {
                healthText.text = "Dead";
            }
            else
            {
                healthText.text = "Health: " + unit.CurrentHealth + " / " + unit.MaxHealth;
            }

            healthBar.fillAmount = (float)unit.CurrentHealth / unit.MaxHealth;
        }

        private void SetMana(IUnit unit)
        {
            manaText.text = "Mana: " + unit.CurrentMana + " / " + unit.MaxMana;
            manaBar.fillAmount = (float)unit.CurrentMana / unit.MaxMana;
        }

        private void SetCastProgress(float percent)
        {
            if (percent < 0)
            {
                // TODO hide
                castbar.fillAmount = 0f;
                return;
            }
            castbar.fillAmount = percent;
        }

        
    }
}