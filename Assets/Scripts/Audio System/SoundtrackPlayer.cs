using UnityEngine;

namespace DeaLoux.CoreSystems.Audio
{
    using ScriptableObjects;

    public class SoundtrackPlayer : MonoBehaviour
    {
        [SerializeField] private VoidEventChannelSO _onSceneReady = default;
        [SerializeField] private AudioCueEventChannelSO _soundtrackChannel = default;
        [SerializeField] private GameSceneSO _thisSceneSO = default;
        [SerializeField] private AudioConfigurationSO _audioConfig = default;

        private void OnEnable()
        {
            _onSceneReady.OnEventRaised += PlayIntro;
        }

        private void OnDisable()
        {
            _onSceneReady.OnEventRaised -= PlayIntro;
        }

        private void PlayIntro()
        {
            _soundtrackChannel.RaisePlayEvent(_thisSceneSO.soundtrack, _audioConfig, 0, true);
            _soundtrackChannel.OnAudioCueFinishRequested += PlayLoop;
        }

        private bool PlayLoop(AudioCueKey key)
        {
            _soundtrackChannel.RaisePlayEvent(_thisSceneSO.soundtrack, _audioConfig, 1);
            return true;
        }

        private bool PlayOutro(AudioCueKey key)
        {
            _soundtrackChannel.RaisePlayEvent(_thisSceneSO.soundtrack, _audioConfig, 2, true);
            return true;
        }
    }
}