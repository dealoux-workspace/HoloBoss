using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeaLoux.Entity
{
    [CreateAssetMenu(fileName = "newAIBaseData", menuName = "Data/AI Data/Base Data")]
    public class AI_Data : ScriptableObject
    {
        public AI_EventsChannelSO EventChannel;

        [Header("Idle State")]
        public float minIdleTime = .1f;
        public float maxIdleTime = .8f;

        public GameObject hitParticle;
        public LayerMask whatIsPlayer;
    }
}