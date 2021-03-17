using UnityEngine;
using DeaLoux.CoreSystems.Collision;

namespace Data
{
    [CreateAssetMenu(fileName = "4 Directional Melee", menuName = "Data/Equipment/4 Directional Melee")]
    [System.Serializable]
    public class Equipment4DirMelee : EquipmentBase
    {
        //typeRange = EquipmentRange.LONG_RANGE;
        public Vector3 lightHBSize = new Vector3(.4f, .6f, 0);
        public Vector3 heavyHBSize = new Vector3(.8f, .8f, 0);

        public override void DoAttack(HitBox hitPoint)
        {
            hitPoint.HitBoxCheck(lightHBSize);
        }

        public override void DoChargedAttack(HitBox hitPoint)
        {
            hitPoint.HitBoxCheck(heavyHBSize);
        }
    }
}


