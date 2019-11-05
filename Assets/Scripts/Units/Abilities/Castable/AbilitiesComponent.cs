using System;
using System.Collections.Generic;
using UnityEngine;

namespace LilMage.Units
{
    public class AbilitiesComponent : IAbilitiesComponent
    {
        private List<IAbility> abilities = new List<IAbility>();

        private enum States
        {
            None, Casting
        }

        private States state = States.None;

        private readonly IUnit caster;

        private IAbility currentAbility;
        
        public event Action<float> OnCastProgressChanged = delegate { };
        
        public AbilitiesComponent(IUnit caster)
        {
            this.caster = caster;
        }

        public void Tick()
        {
            if (state == States.None || currentAbility == null) return;
            
            float castPercent = currentAbility.Tick();
            OnCastProgressChanged?.Invoke(castPercent);
        }
        
        public CastResult Cast<T>(IUnit target) where T : IAbility
        {
            foreach (var ability in abilities)
            {
                if (ability is T)
                {
                    return CastAbility(ability, target);
                }
            }
            return CastResult.ErrorNotFound;
        }

        public CastResult CheckCast<T>(IUnit target) where T : IAbility
        {
            if (state == States.Casting) return CastResult.ErrorAlreadyCasting;
            foreach (var ability in abilities)
            {
                if (ability is T)
                {
                    return ability.CheckCast(caster, target);
                }
            }

            return CastResult.ErrorNotFound;
        }

        public void Add(IAbility ability)
        {
            foreach (var a in abilities)
            {
                if (a.GetType() == ability.GetType()) return;
            }
            abilities.Add(ability);
            ability.Init(caster);
        }

        public void Remove<T>() where T : IAbility
        {
            foreach (var ability in abilities)
            {
                if (ability.GetType() != typeof(T)) continue;
                abilities.Remove(ability);
                return;
            }
        }
        
        private CastResult CastAbility(IAbility ability, IUnit target)
        {
            var result = ability.Cast(caster, target);
            if (result == CastResult.Success)
            {
                currentAbility = ability;
                state = States.Casting;
                ability.OnFinishedCasting += AbilityFinished;
            }

            return result;
        }

        private void AbilityFinished(IAbility ability)
        {
            if (ability != currentAbility)
            {
                // TODO error in-game
                Debug.LogError("listening to the wrong ability");
            }

            state = States.None;
            ability.OnFinishedCasting -= AbilityFinished;
        }
        
        private void StopListening()
        {
            if (currentAbility != null)
            {
                currentAbility.OnFinishedCasting -= AbilityFinished;
            }
        }
    }
}