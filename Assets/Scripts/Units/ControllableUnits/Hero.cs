using Photon.Pun;
using UnityEngine;

namespace LilMage.Units
{
    /// <summary>
    /// The playable character in LilMage
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class Hero : UnitBase, IControllableUnit
    {
        #pragma warning disable 649
        [SerializeField] private int playerID = 0;
        [SerializeField] private BasicMovement.Settings movementSettings;
        [SerializeField] private BasicRotation.Settings rotationSettings;
        
        // TODO create ability manager
        [SerializeField] private ReplenishMana replenishManaAbility;
        [SerializeField] private StoneThrow stoneThrowAbility;
        #pragma warning restore 649

        public IMovement Movement { get; private set; }
        public IRotation Rotation { get; private set; }
        public IController CurrentController { get; private set; }

        protected override void Initialise()
        {
            Abilities.Add(replenishManaAbility);
            Abilities.Add(stoneThrowAbility);
        }
        
        protected override void InitialiseMine()
        {
            var body = GetComponent<Rigidbody>();
            Movement = new BasicMovement(body, movementSettings);
            Rotation = new BasicRotation(body, rotationSettings);

            var controller = Object.FindObjectOfType<LilMage.PlayerController>();
            controller.Init(this, playerID);
            PossessByController(controller);

            GameManager.UI.Get<HUD>().SetPlayer(this);
            GameManager.Camera.SetPlayerCamera(this);
        }

        private void Update()
        {
            Abilities.Tick();
        }

        public void PossessByController(IController controller)
        {
            CurrentController = controller;
        }

        public void Attack()
        {
            var result = Abilities.CheckCast<StoneThrow>(Target);
            if (result != CastResult.Success)
            {
                Debug.Log(result.ToString());
            } 
            else
            {
                photonView.RPC("RPC_Attack", RpcTarget.All, Target.GetViewID());
            }
            
        }

        public void ReplenishMana()
        {
            var result = Abilities.CheckCast<ReplenishMana>(this);
            if (result != CastResult.Success)
            {
                Debug.Log(result.ToString());
            }
            else
            {
                photonView.RPC("RPC_Replenish", RpcTarget.All);
            }
        }

        [PunRPC]
        private void RPC_Attack(int targetID)
        {
            var targetObj = PhotonView.Find(targetID);
            if (targetObj == null) return;
            var target = targetObj.GetComponent<IUnit>();
            Abilities.Cast<StoneThrow>(target);
        }
        [PunRPC] private void RPC_Replenish() => Abilities.Cast<ReplenishMana>(this);
    }
}