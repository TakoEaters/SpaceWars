using UnityEngine;

namespace _Project.Scripts.General.Saves
{
    public static class AudioSaveSystem 
    {
        public static void SetAudioVolume(Audio.AudioType audio, float percentage)
        {
            string id = $"Audio_Type_{audio}";
            PlayerPrefs.SetFloat(id, percentage);
        }

        public static float GetAudioVolume(Audio.AudioType audio)
        {
            string id = $"Audio_Type_{audio}";
            return PlayerPrefs.GetFloat(id, 1);
        }
    }
}