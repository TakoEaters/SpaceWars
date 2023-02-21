using System;

namespace _Project.Scripts.Audio
{
    public interface IAudio
    {
        public AudioType Audio { get; }
        public void SetVolume(float percentage);
    }

    [Serializable] public enum AudioType { FX, Music }
}