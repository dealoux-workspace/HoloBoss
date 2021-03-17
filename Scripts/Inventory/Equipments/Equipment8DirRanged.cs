using System.Collections.Generic;
using UnityEngine;
using DeaLoux.CoreSystems.Collision;

namespace Data
{
    [CreateAssetMenu(fileName = "8 Directional Ranged", menuName = "Data/Equipment/8 Directional Ranged")]
    [System.Serializable]
    public class Equipment8DirRanged : EquipmentBase
    {
        //typeRange = EquipmentRange.LONG_RANGE;
        public int poolSize = 5;
        public ProjectilePoolSO lightPool = default;
        public ProjectilePoolSO heavyPool = default;
        public ProjectileFactorySO lightFactory;
        public ProjectileFactorySO heavyFactory;

        void Awake()
        {
            Debug.Log("Awake");

            lightPool.Factory = lightFactory;
            heavyPool.Factory = heavyFactory;
            lightPool.Prewarm(poolSize);
            heavyPool.Prewarm(poolSize);
            lightPool.SetParent(new GameObject("lightProjectilePooler").transform);
            heavyPool.SetParent(new GameObject("heavyProjectilePooler").transform);
        }

        public override void DoAttack(HitBox hitPoint)
        {
            Transform transform = hitPoint.gameObject.transform;

            var shotInstance = lightPool.Request();
            shotInstance.transform.position = transform.position;
            shotInstance.transform.rotation = transform.rotation;
            shotInstance.GetComponent<Projectile>().Init(transform.right);
        }

        public override void DoChargedAttack(HitBox hitPoint)
        {
            Transform transform = hitPoint.gameObject.transform;

            var shotInstance = heavyPool.Request();
            shotInstance.transform.position = transform.position;
            shotInstance.transform.rotation = transform.rotation;
            shotInstance.GetComponent<Projectile>().Init(transform.right);
        }
    }
}


