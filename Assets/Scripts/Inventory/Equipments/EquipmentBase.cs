using System.Collections.Generic;
using UnityEngine;

namespace DeaLoux.Equipment
{
    using CoreSystems.Collision;

    public abstract class EquipmentBase : ScriptableObject
    {
        [Header("Animation Clips")]
        public List<EquipmentAnimation> specificAnimations;

        [Header("Stats")]
        public EquipmentType type;
        public EquipmentRange typeRange;
        public EquipmentType Ctype;
        public EquipmentRange CtypeRange;
        public float weaponOffset;

        public abstract void DoAttack(HitBox hitPoint);
        public abstract void DoHeavyAttack(HitBox hitPoint);
    }
}

public enum EquipmentType
{
    EIGHT_WAY,
    FOUR_WAY,
}

public enum EquipmentRange
{
    MELEE,
    LONG_RANGE,
}


