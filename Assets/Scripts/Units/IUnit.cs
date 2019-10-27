using System;

namespace LilMage.Units
{
    /// <summary>
    /// A unit in Lil Mage
    /// </summary>
    public interface IUnit
    {
        string Name { get; }
        int CurrentHealth { get; }
        int MaxHealth { get; }
        int CurrentMana { get; }
        int MaxMana { get; }
        
        IUnit Target { get; }

        IAbilitiesComponent Abilities { get; }
        IUnitEffects Effects { get; }
        
        event Action OnDeath;
        event Action<IUnit> OnHealthChanged;
        event Action<IUnit> OnManaChanged;
        
        void TakeDamage(int value);
        void AddHealth(int value);
        void TakeMana(int value);
        void AddMana(int value);
        void Die();
        void Revive(float healthPercent, float manaPercent);

        void SetTarget(IUnit target);
    }
}