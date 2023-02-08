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
    }
}
