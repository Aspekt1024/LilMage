using UnityEngine;

namespace LilMage.Units
{
    [CreateAssetMenu(menuName = "Abilities/Replenish Mana")]
    public class ReplenishMana : AbilityBase
    {
        private IUnit caster;
        private IUnit target;

        private float tickTime;
        
        public override CastResult Cast(IUnit caster, IUnit target)
        {
            if (IsActive) return CastResult.ErrorAlreadyCasting;

            this.caster = caster;
            this.target = caster;
            
            StartCasting();
            return CastResult.Success;
        }

        protected override CastResult Trigger()
        {
            StartChanelling();
            tickTime = 0f;
            return CastResult.Success;
        }
        
        protected override CastResult Channel(float deltaTime)
        {
            tickTime += deltaTime;
            if (tickTime > 0.5f)
            {
                target.AddMana(1);
                tickTime = 0f;
                if (target.CurrentMana == target.MaxMana)
                {
                    StopCasting();
                }
            }
            return CastResult.Success;
        }

    }
}