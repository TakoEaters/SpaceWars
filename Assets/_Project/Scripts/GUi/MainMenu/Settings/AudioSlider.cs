using System;
using _Project.Scripts.Audio;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.General.Saves;
using UnityEngine;
using UnityEngine.UI;
using AudioType = _Project.Scripts.Audio.AudioType;

namespace _Project.Scripts.GUi.MainMenu.Settings
{
    public class AudioSlider : MonoBehaviour
    {
        [SerializeField] private AudioType _type;
        [SerializeField] private Slider _slider;

        private IAudioManager _audioManager;

        private void Awake()
        {
            _slider.value = AudioSaveSystem.GetAudioVolume(_type);
            _slider.onValueChanged.AddListener(OnAudioVolumeChange);
        }

        private void OnAudioVolumeChange(Single value)
        {
            float percentage = value;
            ServiceLocator.Current.Get<IAudioManager>().ChangeVolume(_type, percentage);
        }
    }
}