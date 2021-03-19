using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
    public class PlayerData : ScriptableObject
    {
        [Header("Global")]
        public int FacingDir = 1;
        public Vector3 LookDir;
        public float LookAngle;

        [Header("Inventory")]
        public EquipmentBase slot1;
        public EquipmentBase slot2;

        [Header("Input Handler")]
        public float inputHoldTime = .2f;
        public float minChargeTime = .7f;

        [Header("Move State")]
        public float movementSpeed = 10f;

        [Header("Damaged State")]
        public float knockbackDistance = 1.5f;

        [Header("Jump State")]
        public float dashJumpMultiplier;
        public float maxJumpHeight = 4;
        public float minJumpHeight = 1;
        public float maxJumpVelocity;
        public float minJumpVelocity;
        public float timeToJumpApex = .4f;
        public float accelerationTimeAirborne = .2f;
        public float accelerationTimeGrounded = .1f;
        public int amountOfJumps = 1;

        [Header("In Air State")]
        public float coyoteTime = .08f;

        [Header("Dash State")]
        public float dashTime = .4f;
        public float dashVelocity = 20f;
        public float afterImageDistance = .8f;

        [Header("Wall Action States")]
        public float wallSlideVelocity = 4.2f;
        public Vector2 wallJumpClimb = new Vector2(2.5f, 10f);
        public Vector2 wallLeap = new Vector2(11f, 9f);
        public float wallJumpTime = .3f;
        public float wallJumpCDTime = .5f;
    }
}