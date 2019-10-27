using System;
using Boo.Lang;
using UnityEngine;

namespace LilMage.Units
{
    /// <summary>
    /// The base class for all units in LilMage
    /// </summary>
    public abstract class UnitBase : MonoBehaviour, IUnit
    {
        public string Name { get; protected set; } = "Unknown";

        public int MaxHealth { get; private set; } = 10;
        public int MaxMana { get; private set; } = 5;
        private int health = 10;
        private int mana = 5;

        public IAbilitiesComponent Abilities { get; private set; }
        public IUnitEffects Effects { get; private set; }

        public IUnit Target { get; private set; }

        public enum States
        {
            None, Dead
        }
        public States State { get; private set; }

        protected virtual void Awake()
        {
            Abilities = new AbilitiesComponent(this);
            Effects = GetComponentInChildren<UnitEffects>();
        }

        public int CurrentHealth
        {
            get => health;
            private set
            {
                health = Mathf.Min(value, MaxHealth);
                if (health <= 0)
                {
                    Die();
                    OnManaChanged?.Invoke(this);
                }
                OnHealthChanged?.Invoke(this);
            }
        }
        public int CurrentMana
        {
            get => mana;
            private set
            {
                mana = Mathf.Min(Mathf.Max(0, value), MaxMana);
                OnManaChanged.Invoke(this);
            }
        }

        public event Action OnDeath = delegate { };
        public event Action<IUnit> OnHealthChanged = delegate { };
        public event Action<IUnit> OnManaChanged = delegate { };

        /// <summary>
        /// Called when the unit dies, or kills the unit if it's not dead
        /// </summary>
        public virtual void Die()
        {
            health = 0;
            mana = 0;
            OnDeath?.Invoke();
        }

        public void TakeDamage(int damage) => CurrentHealth -= damage;
        public void AddHealth(int value) => CurrentHealth += value;
        public void TakeMana(int value) => CurrentMana -= value;
        public void AddMana(int value) => CurrentMana += value;

        public void Revive(float healthPercent, float manaPercent)
        {
            CurrentHealth = (int)(healthPercent * MaxHealth);
            CurrentMana = (int)(manaPercent * MaxMana);
        }

        public void SetTarget(IUnit target)
        {
            Target = target;
            GameManager.UI.Get<HUD>().SetTarget(target);
        }

    }
}