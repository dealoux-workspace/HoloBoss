using UnityEngine;

namespace DeaLoux.Entity
{
    using Equipment;

    [CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
    public class EntityData : ScriptableObject
    {
        [Header("Global")]
        public int facingDir = 1;
        public Vector3 LookDir;
        public float LookAngle;

        [Header("Inventory")]
        public EquipmentBase slot1;
        public EquipmentBase slot2;

        [Header("Move State")]
        public float movementSpeed = 10f;

        [Header("Hurt State")]
        public float hp;
        public float iframe = 1.4f;
        public float IframeDuration = 1.4f;
        public float flashDuration = .2f;
        public Color flashColour;
        public Color transparent;
        public Vector2 knockbackDir;
        public float knockbackDistance = 1.5f;

        [Header("Jump State")]
        public float dashJumpMultiplier = 1.24f;
        public float maxJumpHeight = 4;
        public float minJumpHeight = 1f;
        public float maxJumpVelocity = 20f;
        public float minJumpVelocity = 10f;
        public float timeToJumpApex = .4f;
        public int amountOfJumps = 1;

        [Header("Dash State")]
        public float dashTime = .3f;
        public float dashVelocity = 20f;
        public float afterImageDistance = 1.2f;

        [Header("Wall Action States")]
        public float wallSlideVelocity = 4.2f;
        public Vector2 wallJump = new Vector2(2.5f, 10f);
        public Vector2 wallLeap = new Vector2(7f, 10f);
        //public float wallJumpTime = .2f;
        public float wallJumpCDTime = .5f;
    }
}