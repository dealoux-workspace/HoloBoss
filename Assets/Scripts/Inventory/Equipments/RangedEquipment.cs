using System.Collections.Generic;
using UnityEngine;

namespace DeaLoux.Player
{
    using CoreSystems.Collision;
    using DeaLoux.Equipment;

    [CreateAssetMenu(fileName = "newRangedEquipment", menuName = "Data/Equipment/Ranged Equipment")]
    [System.Serializable]
    public class RangedEquipment : EquipmentBase
    {
        //typeRange = EquipmentRange.LONG_RANGE;
        public int poolSize = 5;
        public Vector3 HBSize = new Vector3(1.2f, 1.2f, 0);
        public ProjectilePoolSO lightPool = default;
        public ProjectilePoolSO heavyPool = default;
        public ProjectileFactorySO lightFactory;
        public ProjectileFactorySO heavyFactory;

        void Awake()
        {
            lightPool.Factory = lightFactory;
            heavyPool.Factory = heavyFactory;

            /*if (!lightPool.HasBeenPrewarmed)
            {
                lightPool.Prewarm(poolSize);
                lightPool.SetParent(new GameObject("lightProjectilePooler").transform);
            }

            if (!heavyPool.HasBeenPrewarmed)
            {
                heavyPool.Prewarm(poolSize);
                heavyPool.SetParent(new GameObject("heavyProjectilePooler").transform);
            }*/
        }

        public override void DoAttack(HitBox hitPoint)
        {
            Collider2D[] contactPoints = hitPoint.HitBoxCheck(HBSize);

            if (contactPoints.Length > 0)
            {
                // checking maybe?
            }
            else
            {
                Transform transform = hitPoint.gameObject.transform;

                var shotInstance = lightPool.Request();
                shotInstance.transform.position = transform.position;
                shotInstance.transform.rotation = transform.rotation;
                shotInstance.GetComponent<Projectile>().Init(transform.right);
            }
        }

        public override void DoHeavyAttack(HitBox hitPoint)
        {
            Collider2D[] contactPoints = hitPoint.HitBoxCheck(HBSize);

            if (contactPoints.Length > 0)
            {

            }
            else
            {
                Transform transform = hitPoint.gameObject.transform;

                var shotInstance = heavyPool.Request();
                shotInstance.transform.position = transform.position;
                shotInstance.transform.rotation = transform.rotation;
                shotInstance.GetComponent<Projectile>().Init(transform.right);
            }
        }
    }
}


