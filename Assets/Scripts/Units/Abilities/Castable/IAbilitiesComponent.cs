using System;

namespace LilMage.Units
{
    public interface IAbilitiesComponent
    {
        event Action<float> OnCastProgressChanged;
        
        CastResult Cast<T>() where T : IAbility;
        void Add(IAbility ability);
        void Remove<T>() where T : IAbility;

        void Tick();
    }
}