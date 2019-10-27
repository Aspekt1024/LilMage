
using System;

namespace LilMage.Units
{
    /// <summary>
    /// An ability that can be cast by the player
    /// </summary>
    public interface IAbility
    {
        event Action<IAbility> OnFinishedCasting;

        void Init(IUnit caster);
        
        CastResult Cast(IUnit caster, IUnit target);
        
        /// <summary>
        /// Similar to Update(), and returns the castbar percentage
        /// </summary>
        float Tick();
        
        void Cancel();
    }
}