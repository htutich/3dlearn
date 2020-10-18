using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace learn3d
{
    public class AudioMenager : MonoBehaviour
    {
        #region Fields

        [SerializeField] private AudioClip[] _menuMusic;
        [SerializeField] private AudioClip[] _gameMusic;
        [SerializeField] private AudioMixerGroup _audioMixerMasterGroup;

        [SerializeField] private Slider _masterMusicSlider;
        [SerializeField] private Slider _backgroundMusicSlider;
        [SerializeField] private Slider _enviromentMusicSlider;

        private AudioSource _audioSource;

        private float _masterMusicVolume;
        private float _backgroundMusicVolume;
        private float _enviromentMusicVolume;

        private bool _isMenu = true;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            var audiosMenagers = FindObjectsOfType<AudioMenager>();
            if (audiosMenagers.Length > 1)
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            if (PlayerPrefs.HasKey("MasterMusicVolume"))
            {
                _masterMusicVolume = PlayerPrefs.GetFloat("MasterMusicVolume");
            }
            else
            {
                _masterMusicVolume = 1.0f;
                PlayerPrefs.SetFloat("MasterMusicVolume", _masterMusicVolume);
            }

            if (PlayerPrefs.HasKey("BackgroundMusicVolume"))
            {
                _backgroundMusicVolume = PlayerPrefs.GetFloat("BackgroundMusicVolume");
            }
            else
            {
                _backgroundMusicVolume = 1.0f;
                PlayerPrefs.SetFloat("BackgroundMusicVolume", _backgroundMusicVolume);
            }

            if (PlayerPrefs.HasKey("EnviromentMusicVolume"))
            {
                _enviromentMusicVolume = PlayerPrefs.GetFloat("EnviromentMusicVolume");
            }
            else
            {
                _enviromentMusicVolume = 1.0f;
                PlayerPrefs.SetFloat("EnviromentMusicVolume", _enviromentMusicVolume);
            }

            _audioSource = GetComponent<AudioSource>();

            _audioMixerMasterGroup.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-80, 0, _masterMusicVolume));
            _audioMixerMasterGroup.audioMixer.SetFloat("BackgroundVolume", Mathf.Lerp(-80, 0, _backgroundMusicVolume));
            _audioMixerMasterGroup.audioMixer.SetFloat("EnviromentVolume", Mathf.Lerp(-80, 0, _enviromentMusicVolume));

            _masterMusicSlider.value = _masterMusicVolume;
            _backgroundMusicSlider.value = _backgroundMusicVolume;
            _enviromentMusicSlider.value = _enviromentMusicVolume;

            StartRandomMusic();
            DontDestroyOnLoad(gameObject);
        }

        private void FixedUpdate()
        {
            if (!_audioSource.isPlaying)
            {
                StartRandomMusic();
            }
        }

        #endregion


        #region Methods

        private void StartRandomMusic()
        {
            _audioSource.Stop();

            if (_isMenu)
            {
                int randomValue = Random.Range(0, _menuMusic.Length);
                _audioSource.clip = _menuMusic[randomValue];
                _audioSource.Play();
            }
            else
            {
                int randomValue = Random.Range(0, _gameMusic.Length);
                _audioSource.clip = _gameMusic[randomValue];
                _audioSource.Play();
            }
        }

        public void SetMasterVolume(float valueVolume)
        {
            _masterMusicVolume = valueVolume;
            _audioMixerMasterGroup.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-80, 0, _masterMusicVolume));
            PlayerPrefs.SetFloat("MasterMusicVolume", _masterMusicVolume);
        }

        public void SetBackgroundVolume(float valueVolume)
        {
            _backgroundMusicVolume = valueVolume;
            _audioMixerMasterGroup.audioMixer.SetFloat("BackgroundVolume", Mathf.Lerp(-80, 0, _backgroundMusicVolume));
            PlayerPrefs.SetFloat("BackgroundMusicVolume", _backgroundMusicVolume);
        }

        public void SetEnviromentVolume(float valueVolume)
        {
            _enviromentMusicVolume = valueVolume;
            _audioMixerMasterGroup.audioMixer.SetFloat("EnviromentVolume", Mathf.Lerp(-80, 0, _enviromentMusicVolume));
            PlayerPrefs.SetFloat("EnviromentMusicVolume", _enviromentMusicVolume);
        }

        public void SetIsMenuValue(bool value)
        {
            _isMenu = value;
            StartRandomMusic();
        }

        #endregion
    }
}