using UnityEngine;

namespace _Project.Scripts.General.Saves
{
    public static class AdvertisementSaveSystem
    {
        public static int GetCurrentTapIndex(string placement)
        {
            string id = $"Ads_Rewarded_{placement}";
            return PlayerPrefs.GetInt(id, 0);
        }

        public static void SetCurrentTapIndex(string placement, int index)
        {
            string id = $"Ads_Rewarded_{placement}";
            PlayerPrefs.SetInt(id, index);
        }
    }
}