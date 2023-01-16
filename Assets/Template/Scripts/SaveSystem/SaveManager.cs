using UnityEngine;

namespace _Project.Scripts.Core.SaveSystem
{
    public static class SaveManager
    {
        public static void SetValue(string saveID, int value)
        {
            PlayerPrefs.SetInt(saveID, value);
        }

        public static int GetValue(string saveID, int defaultValue)
        {
            return PlayerPrefs.GetInt(saveID, defaultValue);
        }
    }
}
