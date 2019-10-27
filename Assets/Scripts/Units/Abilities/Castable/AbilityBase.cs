using System;
using UnityEngine;

namespace LilMage.Units
{
    public abstract class AbilityBase : ScriptableObject, IAbility
    {
        #pragma warning disable 649
        [SerializeField] protected int cost;
        [SerializeField] protected float castTime;
        [SerializeField] protected float channelTime;
        #pragma warning restore 649

        private IUnit caster;
        private float timeStartedCasting;
        
        public event Action<IAbility> OnFinishedCasting = delegate { };
        
        private enum States
        {
            None,
            Casting,
            Channeling,
        }

        private States state;

        public abstract CastResult Cast(IUnit caster, IUnit target);

        public void Init(IUnit caster)
        {
            this.caster = caster;
            state = States.None;
        }

        public virtual void Cancel()
        {
            StopCasting();
        }
        
        protected abstract CastResult Trigger();
        protected abstract CastResult Channel(float deltaTime);

        public virtual float Tick()
        {
            float castPercent = -1f;
            switch (state)
            {
                case States.None:
                    break;
                case States.Casting:
                    castPercent = (Time.time - timeStartedCasting) / castTime;
                    if (castPercent >= 1f)
                    {
                        Trigger();
                    }
                    return castPercent;
                case States.Channeling:
                    castPercent = 1 - (Time.time - timeStartedCasting) / channelTime;
                    if (castPercent >= 0f)
                    {
                        Channel(Time.deltaTime);
                    }
                    else
                    {
                        StopCasting();
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return castPercent;
        }

        protected bool IsActive => state == States.Casting || state == States.Channeling;
        protected bool IsIdle => state == States.None;

        protected void StartCasting()
        {
            state = States.Casting;
            timeStartedCasting = Time.time;
        }

        protected void StartChanelling()
        {
            state = States.Channeling;
            timeStartedCasting = Time.time;
        }

        protected void StopCasting()
        {
            state = States.None;
            OnFinishedCasting?.Invoke(this);
        }
    }
}