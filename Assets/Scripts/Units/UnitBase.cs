using System;
using Photon.Pun;
using UnityEngine;

namespace LilMage.Units
{
    /// <summary>
    /// The base class for all units in LilMage
    /// </summary>
    public abstract class UnitBase : MonoBehaviour, IUnit, IPunObservable
    {
        public string Name { get; protected set; } = "Unknown";
        
        protected PhotonView photonView;

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
            
            photonView = GetComponent<PhotonView>();
            if (photonView == null) return;
            
            if (photonView.IsMine)
            {
                photonView.RPC("RPC_SyncName", RpcTarget.AllBuffered, PlayerInfo.Instance.PlayerName);
            }
        }

        private void Start()
        {
            Initialise();
            GameManager.Units.RegisterUnit(this);
            
            if (photonView == null || !photonView.IsMine) return;
            InitialiseMine();
        }

        public int GetViewID() => photonView == null ? -1 : photonView.ViewID;

        protected abstract void Initialise();
        protected abstract void InitialiseMine();

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

        protected void SetHealth(int value) => CurrentHealth = value;
        protected void SetMana(int value) => CurrentMana = value;

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
        
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(CurrentHealth);
                stream.SendNext(CurrentMana);
                stream.SendNext(Target?.GetViewID() ?? -1);
            }
            else
            {
                var health = (int)stream.ReceiveNext();
                var mana = (int)stream.ReceiveNext();
                var targetID = (int)stream.ReceiveNext();
                SetHealth(health);
                SetMana(mana);
                
                if (targetID < 0)
                {
                    Target = null;
                }
                else
                {
                    var targetObj = PhotonView.Find(targetID);
                    Target = targetObj == null ? null : targetObj.GetComponent<IUnit>();
                }
            }
        }

        [PunRPC] protected void RPC_SyncName(string name) => Name = name;
    }
}