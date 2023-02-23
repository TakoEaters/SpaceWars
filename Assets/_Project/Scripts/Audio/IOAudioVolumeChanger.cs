using System;
using _Project.Scripts.Core.LocatorServices;
using UnityEngine;

namespace _Project.Scripts.Audio
{
    public class IOAudioVolumeChanger : MonoBehaviour, IAudio
    {
        [SerializeField] private AudioType _audioType;
        public AudioType Audio => _audioType;
    
        private AudioSource _audioSource;
        private float _maxValue;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _maxValue = _audioSource.volume;
            ServiceLocator.Current.Get<IAudioManager>().Subscribe(this);
        }

        public void SetVolume(float percentage)
        {
            if (!_audioSource) _audioSource = GetComponent<AudioSource>();
            var percentageVolume = Mathf.Lerp(0f, _maxValue, percentage);
            _audioSource.volume = percentageVolume;
        }

        private void OnDestroy()
        {
            ServiceLocator.Current.Get<IAudioManager>().UnSubscribe(this);
        }
    }
}