using System.Collections.Generic;
using LilMage.Units;
using UnityEngine;

namespace LilMage
{
    /// <summary>
    /// Manages all units in the game
    /// </summary>
    public class UnitManager : IManager
    {
        public Hero PlayerHero { get; private set; }
        private readonly List<IUnit> units = new List<IUnit>();
        
        public void Init()
        {
            Debug.Log("Unit manager online");
        }

        public void SetPlayerHero(Hero hero)
        {
            PlayerHero = hero;
        }

        public void RegisterUnit(IUnit unit)
        {
            units.Add(unit);
        }

        public void UnregisterUnit(IUnit unit)
        {
            if (units.Contains(unit))
            {
                units.Remove(unit);
            }
        }

        public void CreateUnit<T>(Vector3 spawnPosition) where T : IUnit
        {
        }
    }
}