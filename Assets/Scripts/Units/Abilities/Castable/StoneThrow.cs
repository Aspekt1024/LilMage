using UnityEngine;

namespace LilMage.Units
{
    [CreateAssetMenu(menuName = "Abilities/Stone Throw")]
    public class StoneThrow : AbilityBase
    {
#pragma warning disable 649
        [SerializeField] private Projectile projectilePrefab;
#pragma warning restore 649
        
        private IUnit caster;
        private IUnit target;
        
        public override CastResult Cast(IUnit caster, IUnit target)
        {
            if (caster.CurrentMana < cost) return CastResult.ErrorNotEnoughMana;
            if (IsActive) return CastResult.ErrorAlreadyCasting;
            if ((UnitBase)target == null) return CastResult.ErrorNoTarget;

            this.caster = caster;
            this.target = target;
            
            StartCasting();
            caster.Effects.Play<SpellcastEffect>(castTime);
            
            return CastResult.Success;
        }

        protected override CastResult Trigger()
        {
            StopCasting();
            if (caster.CurrentMana < cost) return CastResult.ErrorNotEnoughMana;
            if (target == null) return CastResult.ErrorNoTarget;
            
            caster.Effects.Stop<SpellcastEffect>();
            caster.TakeMana(cost);

            var casterTf = ((UnitBase) caster).transform;
            var projectile = Instantiate(projectilePrefab);
            var settings = new Projectile.Settings(20f, ((UnitBase)target).transform, caster.Effects.GetProjectileSpawnPoint());
            projectile.OnTargetHit += TargetHit;
            projectile.Cast(settings);
            
            
            return CastResult.Success;
        }
        
        protected override CastResult Channel(float deltaTime)
        {
            return CastResult.Success;
        }

        private void TargetHit(Projectile projectile)
        {
            projectile.OnTargetHit -= TargetHit;
            target.TakeDamage(1);
        }
    }
}