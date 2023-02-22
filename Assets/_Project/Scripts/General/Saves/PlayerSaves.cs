using UnityEngine;

namespace _Project.Scripts.General.Saves
{
    public static class PlayerSaves 
    {
        public static int GetPlayerWeapon()
        {
            string id = $"Player_Weapon";
            return PlayerPrefs.GetInt(id, 0);
        }

        public static void SetPlayerWeapon(int weaponID)
        {
            string id = $"Player_Weapon";
            PlayerPrefs.SetInt(id, weaponID);
        }

        public static int GetSkinID()
        {
            string id = $"Player_Skin";
            return PlayerPrefs.GetInt(id, 0);
        }

        public static void SetSkinID(int skinID)
        {
            string id = $"Player_Skin";
            PlayerPrefs.SetInt(id, skinID);
        }
    }
}
