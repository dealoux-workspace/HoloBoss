using UnityEngine;

namespace DeaLoux.Player
{
    using CoreSystems.Collision;
    using DeaLoux.Entity;
    using DeaLoux.Equipment;

    [CreateAssetMenu(fileName = "newMeleeEquipment", menuName = "Data/Equipment/Melee Equipment")]
    [System.Serializable]
    public class MeleeEquipment : EquipmentBase
    {
        public float LightDamage;
        public float HeavyDamage;
        public Vector3 lightHitbox = new Vector3(1.8f, 1.8f, 0);
        public Vector3 heavyHitbox = new Vector3(2f, 2f, 0);

        public override void DoAttack(HitBox hitPoint) => HandleAttack(hitPoint, lightHitbox, LightDamage);

        public override void DoHeavyAttack(HitBox hitPoint) => HandleAttack(hitPoint, heavyHitbox, HeavyDamage);

        private void HandleAttack(HitBox hitPoint, Vector3 hitbox, float damage)
        {
            Collider2D[] contactPoints = hitPoint.HitBoxCheck(hitbox);

            if (contactPoints.Length > 0)
            {
                switch (contactPoints[0].gameObject.layer)
                {
                    case 7:
                        hitPoint.GetComponentInParent<Entity>().KnockBack();
                        Debug.Log("Hitting the ground");
                        break;
                    case 8:
                        // enemy's hitbox
                        break;

                    case 11:
                        //var ent = contactPoints[0].gameObject;
                        //Debug.Log(ent.GetComponentInParent<AI.AI>());
                        contactPoints[0].gameObject.GetComponent<AI>().TakeDamage(damage);
                        break;
                }
            }
        }
    }
}


