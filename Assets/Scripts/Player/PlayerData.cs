using UnityEngine;

namespace DeaLoux.Entity
{
    using CoreSystems.ScriptableObjects;

    [CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
    public class PlayerData : ScriptableObject
    {
        [Header("Input Handler")]
        public float inputHoldTime = .2f;
        public float minChargeTime = .7f;

        [Header("In Air State")]
        public float coyoteTime = .08f;
    }
}