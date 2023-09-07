using System.Collections.Generic;
using UnityEngine;

namespace DeaLoux.Equipment
{
    [CreateAssetMenu(fileName = "newAnimationNames", menuName = "Data/Equipment/Animation Names")]
    [System.Serializable]
    public class AnimationNames : ScriptableObject
    {
        public List<string> list;
    }
}


