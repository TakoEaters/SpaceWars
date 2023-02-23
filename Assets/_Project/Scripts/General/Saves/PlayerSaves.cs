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

        public static int GetWeaponLevel(int weaponID)
        {
            string id = $"Weapon_Level_{weaponID}";
            return PlayerPrefs.GetInt(id, 1);
        }

        public static void IncrementWeaponLevel(int weaponID)
        {
            string id = $"Weapon_Level_{weaponID}";
            PlayerPrefs.SetInt(id, GetWeaponLevel(weaponID) + 1);
        }

        public static bool IsWeaponLocked(int weaponID)
        {
            string id = $"Weapon_Lock_{weaponID}";
            int defaultLock = weaponID == 0 ? 1 : 0;
            return PlayerPrefs.GetInt(id, defaultLock) == 0;
        }
        
        public static void UnlockWeapon(int weaponID)
        {
            string id = $"Weapon_Lock_{weaponID}"; 
            PlayerPrefs.SetInt(id, 1);
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
