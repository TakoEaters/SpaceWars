using System.Collections.Generic;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.General.Saves;
using UnityEngine;

namespace _Project.Scripts.Audio
{
    public class AudioManager : MonoBehaviour, IAudioManager
    {
        private readonly List<IAudio> _audios = new List<IAudio>();
    
        public void Register()
        {
            ServiceLocator.Current.Register<IAudioManager>(this);
        }

        public void Subscribe(IAudio volume)
        {
            if (_audios.Contains(volume)) return;
            _audios.Add(volume);
            volume.SetVolume(AudioSaveSystem.GetAudioVolume(volume.Audio));
        }

        public void UnSubscribe(IAudio volume)
        {
            if (!_audios.Contains(volume)) return;
            _audios.Remove(volume);
        }

        public void ChangeVolume(AudioType type, float percentage)
        {
            AudioSaveSystem.SetAudioVolume(type, percentage);
        
            foreach (IAudio audioClip in _audios)
            {
                if (audioClip.Audio == type)
                {
                    audioClip.SetVolume(percentage);   
                }   
            }
        }
    }

    public interface IAudioManager : IGameService
    {
        public void Subscribe(IAudio volume);
        public void UnSubscribe(IAudio volume);
        public void ChangeVolume(AudioType type, float percentage);
    }
}