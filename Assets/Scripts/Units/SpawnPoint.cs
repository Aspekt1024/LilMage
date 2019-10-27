using UnityEngine;

namespace LilMage.Units
{
    public class SpawnPoint : MonoBehaviour
    {
        public GameObject SpawnPrefab;
        public bool HasRespawn = true;
        public float SpawnTimer = 20f;

        private IUnit spawnedUnit;
        private Transform tf;
        private bool requireRespawn;
        private float timeOfDeath;
        
        private void Start()
        {
            tf = transform;
            SpawnUnit();
        }

        private void Update()
        {
            if (!requireRespawn) return;
            if (Time.time >= timeOfDeath + SpawnTimer)
            {
                SpawnUnit();
                requireRespawn = false;
            }
            
        }

        private void SpawnUnit()
        {
            if (SpawnPrefab == null)
            {
                Debug.LogWarning("Spawn exists with no spawn prefab");
                return;
            }
            
            var obj = Instantiate(SpawnPrefab, tf.position, tf.rotation, tf);
            spawnedUnit = obj.GetComponent<IUnit>();
            if (spawnedUnit == null)
            {
                Debug.LogError("spawned something that isn't a unit. Check your spawn prefabs to ensure they are units");
                Destroy(obj);
                return;
            }

            Debug.Log(spawnedUnit.Name + " has spawned");
            spawnedUnit.OnDeath += OnUnitDeath;
        }

        private void OnUnitDeath()
        {
            spawnedUnit.OnDeath -= OnUnitDeath;
            requireRespawn = true;
            timeOfDeath = Time.time;
        }
    }
}