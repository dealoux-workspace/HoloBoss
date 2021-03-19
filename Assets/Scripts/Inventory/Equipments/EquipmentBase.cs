using UnityEngine;
using DeaLoux.CoreSystems.Collision;

namespace Data
{
    public abstract class EquipmentBase : ScriptableObject
    {
        [Header("General Animation Clips")]
        public AnimationClip dash;
        public AnimationClip move;
        public AnimationClip moveUp;
        public AnimationClip wall;
        public AnimationClip wallCharged;

        [Header("4 Way Animation Clips")]
        public AnimationClip aerial4;
        public AnimationClip aerial4Down;
        public AnimationClip aerial4Up;
        public AnimationClip idle4;
        public AnimationClip idle4_2;
        public AnimationClip idle4_3;
        public AnimationClip idle4Up;

        [Header("4 Way Charged Animation Clips")]
        public AnimationClip idle4Charged;
        public AnimationClip aerial4Charged;


        [Header("8 Way Animation Clips")]
        public AnimationClip aerial8;
        public AnimationClip aerial8Down;
        public AnimationClip aerial8DownDiag;
        public AnimationClip aerial8Up;
        public AnimationClip aerial8UpDiag;
        public AnimationClip idle8;
        public AnimationClip idle8DownDiag;
        public AnimationClip idle8Up;
        public AnimationClip idle8UpDiag;

        [Header("8 Way Charged Animation Clips")]
        public AnimationClip aerial8Charged;
        public AnimationClip aerial8ChargedDown;
        public AnimationClip aerial8ChargedDownDiag;
        public AnimationClip aerial8ChargedUp;
        public AnimationClip aerial8ChargedUpDiag;
        public AnimationClip idle8Charged;
        public AnimationClip idle8ChargedDownDiag;
        public AnimationClip idle8ChargedUp;
        public AnimationClip idle8ChargedUpDiag;

        [Header("Stats")]
        public EquipmentType type;
        public EquipmentRange typeRange;
        public EquipmentType Ctype;
        public EquipmentRange CtypeRange;
        public float weaponOffset;
        public float damange;
        public float chargedDamage;

        public abstract void DoAttack(HitBox hitPoint);
        public abstract void DoChargedAttack(HitBox hitPoint);
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


