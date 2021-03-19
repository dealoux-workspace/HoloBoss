using UnityEngine;

namespace CoreSystem.Audio
{
    [CreateAssetMenu(fileName = "newAudioObject", menuName = "Audio/Clip")]
    [System.Serializable]
    public class AudioObject : ScriptableObject
    {
        public AudioID ID;
        public AudioClip clip;

        public void Init(AudioID _ID, AudioClip _clip)
        {
            ID = _ID;
            clip = _clip;
        }
    }
}




