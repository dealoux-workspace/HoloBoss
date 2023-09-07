using UnityEngine;

namespace DeaLoux.CoreSystems.Audio
{
    [CreateAssetMenu(fileName = "NewSoundEmitterFactory", menuName = "Factory/SoundEmitter Factory")]
    public class SoundEmitterFactorySO : Patterns.FactorySO<SoundEmitter>
    {
        public SoundEmitter prefab = default;

        public override SoundEmitter Create()
        {
            return Instantiate(prefab);
        }
    }
}