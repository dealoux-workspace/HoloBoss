using UnityEngine;

namespace DeaLoux.Equipment
{
    [CreateAssetMenu(fileName = "newEquipmentAnimation", menuName = "Data/Equipment/Equipment Animation")]
    [System.Serializable]
    public class EquipmentAnimation : ScriptableObject
    {
        public int id;
        public AnimationClip clip;
    }
}


