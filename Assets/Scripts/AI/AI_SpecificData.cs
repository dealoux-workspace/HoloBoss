using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "newAISpecificData", menuName = "Data/AI Specific Data/Base Data")]
    public class AI_SpecificData : ScriptableObject
    {
        [Header("Move State")]
        public float movementSpeed = 15f;

        [Header("Idle State")]
        public float minIdleTime = .1f;
        public float maxIdleTime = 2f;
    }
}