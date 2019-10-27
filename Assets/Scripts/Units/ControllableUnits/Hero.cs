using UnityEngine;

namespace LilMage.Units
{
    /// <summary>
    /// The playable character in LilMage
    /// </summary>
    [RequireComponent(typeof(LilMage.PlayerController), typeof(Rigidbody))]
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


        private void Start()
        {
            Name = "Lil Mage";
            
            var body = GetComponent<Rigidbody>();
            Movement = new BasicMovement(body, movementSettings);
            Rotation = new BasicRotation(body, rotationSettings);

            var controller = GetComponent<LilMage.PlayerController>();
            controller.Init(this, playerID);
            PossessByController(controller);

            GameManager.UI.Get<HUD>().SetPlayer(this);
            
            Abilities.Add(replenishManaAbility);
            Abilities.Add(stoneThrowAbility);
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
            var result = Abilities.Cast<StoneThrow>();
            if (result != CastResult.Success)
            {
                Debug.Log(result.ToString());
            }
        }

        public void ReplenishMana()
        {
            var result = Abilities.Cast<ReplenishMana>();
            if (result != CastResult.Success)
            {
                Debug.Log(result.ToString());
            }
        }
    }
}