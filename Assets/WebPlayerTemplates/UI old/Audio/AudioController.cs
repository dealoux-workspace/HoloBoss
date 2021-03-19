using System.Collections;
using UnityEngine;

namespace CoreSystem.Audio
{
    public class AudioController : MonoBehaviour
    {
        public static AudioController instance;

        public bool debug;
        public AudioTrack[] tracks;

        private Hashtable m_AudioTable; // relationship of audio types (key) and tracks (value)
        private Hashtable m_JobTable;   // relationship between audio types (key) and jobs (value)

        private enum AudioAction
        {
            START,
            STOP,
            RESTART
        }

        [System.Serializable]
        public class AudioTrack
        {
            public AudioSource source;
            public AudioObject[] audio;
        }

        private class AudioJob
        {
            public AudioAction action;
            public AudioID ID;
            public bool fade;
            public WaitForSeconds delay;

            public AudioJob(AudioAction _action, AudioID _ID, bool _fade, float _delay)
            {
                action = _action;
                ID = _ID;
                fade = _fade;
                delay = _delay > 0f ? new WaitForSeconds(_delay) : null;
            }
        }

        #region Unity Functions
        private void Awake()
        {
            if (!instance)
            {
                Configure();
            }
        }

        private void OnDisable()
        {
            Dispose();
        }
        #endregion

        #region Public Functions
        public void PlayAudio(AudioID _ID, bool _fade = false, float _delay = 0.0F)
        {
            AddJob(new AudioJob(AudioAction.START, _ID, _fade, _delay));
        }

        public void StopAudio(AudioID _ID, bool _fade = false, float _delay = 0.0F)
        {
            AddJob(new AudioJob(AudioAction.STOP, _ID, _fade, _delay));
        }

        public void RestartAudio(AudioID _ID, bool _fade = false, float _delay = 0.0F)
        {
            AddJob(new AudioJob(AudioAction.RESTART, _ID, _fade, _delay));
        }
        #endregion

        #region Private Functions
        private void Configure()
        {
            instance = this;
            m_AudioTable = new Hashtable();
            m_JobTable = new Hashtable();
            GenerateAudioTable();
        }

        private void Dispose()
        {
            // cancel all jobs in progress
            foreach (DictionaryEntry _kvp in m_JobTable)
            {
                Coroutine _job = (Coroutine)_kvp.Value;
                StopCoroutine(_job);
            }
        }

        private void AddJob(AudioJob _job)
        {
            // cancel any job that might be using this job's audio source
            RemoveConflictingJobs(_job.ID);

            Coroutine _jobRunner = StartCoroutine(RunAudioJob(_job));
            m_JobTable.Add(_job.ID, _jobRunner);
            Log("Starting job on [" + _job.ID + "] with operation: " + _job.action);
        }

        private void RemoveJob(AudioID _name)
        {
            if (!m_JobTable.ContainsKey(_name))
            {
                Log("Trying to stop a job [" + _name + "] that is not running.");
                return;
            }
            Coroutine _runningJob = (Coroutine)m_JobTable[_name];
            StopCoroutine(_runningJob);
            m_JobTable.Remove(_name);
        }

        private void RemoveConflictingJobs(AudioID _name)
        {
            // cancel the job if one exists with the same type
            if (m_JobTable.ContainsKey(_name))
            {
                RemoveJob(_name);
            }

            // cancel jobs that share the same audio track
            AudioID _conflictAudio = AudioID.None;
            AudioTrack _audioTrackNeeded = GetAudioTrack(_name, "Get Audio Track Needed");
            foreach (DictionaryEntry _entry in m_JobTable)
            {
                AudioID _audioName = (AudioID)_entry.Key;
                AudioTrack _audioTrackInUse = GetAudioTrack(_audioName, "Get Audio Track In Use");
                if (_audioTrackInUse.source == _audioTrackNeeded.source)
                {
                    _conflictAudio = _audioName;
                    break;
                }
            }
            if (!_conflictAudio.Equals(AudioID.None))
            {
                RemoveJob(_conflictAudio);
            }
        }

        private IEnumerator RunAudioJob(AudioJob _job)
        {
            if (_job.delay != null) yield return _job.delay;

            AudioTrack _track = GetAudioTrack(_job.ID); // track existence should be verified by now
            _track.source.clip = GetAudioClipFromAudioTrack(_job.ID, _track);

            float _initial = 0f;
            float _target = 1f;
            switch (_job.action)
            {
                case AudioAction.START:
                    _track.source.Play();
                    break;
                case AudioAction.STOP when !_job.fade:
                    _track.source.Stop();
                    break;
                case AudioAction.STOP:
                    _initial = 1f;
                    _target = 0f;
                    break;
                case AudioAction.RESTART:
                    _track.source.Stop();
                    _track.source.Play();
                    break;
            }

            // fade volume
            if (_job.fade)
            {
                float _duration = 1.0f;
                float _timer = 0.0f;

                while (_timer <= _duration)
                {
                    _track.source.volume = Mathf.Lerp(_initial, _target, _timer / _duration);
                    _timer += Time.deltaTime;
                    yield return null;
                }

                // if _timer was 0.9999 and Time.deltaTime was 0.01 we would not have reached the target
                // make sure the volume is set to the value we want
                _track.source.volume = _target;

                if (_job.action == AudioAction.STOP)
                {
                    _track.source.Stop();
                }
            }

            m_JobTable.Remove(_job.ID);
            Log("Job count: " + m_JobTable.Count);
        }

        private void GenerateAudioTable()
        {
            foreach (AudioTrack _track in tracks)
            {
                foreach (AudioObject _obj in _track.audio)
                {
                    // do not duplicate keys
                    if (m_AudioTable.ContainsKey(_obj.ID))
                    {
                        LogWarning("You are trying to register audio [" + _obj.ID + "] that has already been registered.");
                    }
                    else
                    {
                        m_AudioTable.Add(_obj.ID, _track);
                        Log("Registering audio [" + _obj.ID + "]");
                    }
                }
            }
        }

        private AudioTrack GetAudioTrack(AudioID _ID, string _job = "")
        {
            if (!m_AudioTable.ContainsKey(_ID))
            {
                LogWarning("You are trying to <color=#fff>" + _job + "</color> for [" + _ID + "] but no track was found supporting this audio type.");
                return null;
            }
            return (AudioTrack)m_AudioTable[_ID];
        }

        private AudioClip GetAudioClipFromAudioTrack(AudioID _ID, AudioTrack _track)
        {
            foreach (AudioObject _obj in _track.audio)
            {
                if (_obj.ID.Equals(_ID))
                {
                    return _obj.clip;
                }
            }
            return null;
        }

        private void Log(string _msg)
        {
            if (!debug) return;
            Debug.Log("[Audio Controller]: " + _msg);
        }

        private void LogWarning(string _msg)
        {
            if (!debug) return;
            Debug.LogWarning("[Audio Controller]: " + _msg);
        }
        #endregion
    }
}
