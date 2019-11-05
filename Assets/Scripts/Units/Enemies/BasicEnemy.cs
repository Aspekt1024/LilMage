using UnityEngine;

namespace LilMage.Units
{
    public class BasicEnemy : UnitBase
    {
        [SerializeField] private float AggroRadius = 30f;
        
        private Hero targettedHero;
        private bool canSeeHero;

        protected override void Initialise()
        {
            
        }

        protected override void InitialiseMine()
        {
            
        }

        public override void Die()
        {
            base.Die();
            Destroy(gameObject, 2f);
        }
        
        private void Update()
        {
            if (canSeeHero)
            {
                InteractWithHero();
            }
            else
            {
                FindHero();
            }
        }

        private void FindHero()
        {
            var mask = 1 << LayerMask.NameToLayer("Unit");
            var colliders = Physics.OverlapSphere(transform.position, AggroRadius, mask);
            if (colliders.Length == 0) return;

            foreach (var c in colliders)
            {
                var hero = c.GetComponentInParent<Hero>();
                if (hero != null)
                {
                    targettedHero = hero;
                    canSeeHero = true;
                    return;
                }
            }
        }

        private void InteractWithHero()
        {
            var distToHero = Vector3.Distance(targettedHero.transform.position, transform.position);
            if (distToHero > AggroRadius)
            {
                targettedHero = null;
                canSeeHero = false;
                return;
            }
            transform.LookAt(targettedHero.transform);
        }
        
    }
}