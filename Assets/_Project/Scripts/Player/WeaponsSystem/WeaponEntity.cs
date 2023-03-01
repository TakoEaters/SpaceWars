using System;
using _Project.Scripts.General.Saves;
using UnityEngine;

namespace _Project.Scripts.Player.WeaponsSystem
{
    [CreateAssetMenu(fileName = "WeaponEntity", menuName = "Configs/Weapons/WeaponData")]
    public class WeaponEntity : ScriptableObject
    {
        [Range(0, 5)] public int ID;
        public string Name;
        public string Description;
        [Range(0.01f, 2.0f)] public float FireRate;
        [Range(10, 100)] public int Damage;

        public int WeaponPrice;
        
        [HideInInspector] public int Level; 
        [HideInInspector] public int UpgradePrice;
        
        public int InitialUpgradePrice; 
        public int AdditiveUpgradePrice;
        
        public int InitialDamage; 
        public int AdditiveDamage;

        public float InitialFireRate; 
        public float AdditiveFireRate;

        [Header("Ammo")] 
        public int MagazineAmmo;
        public int TotalAmmo;
        public float InitialReloadingDuration;
        [HideInInspector] public float ReloadingDuration;
        public float ReloadingAdditive;

        private readonly int _maxLevel = 10;

        public void UpdateData()
        {
            Level = PlayerSaves.GetWeaponLevel(ID);
            UpgradePrice = InitialUpgradePrice + (AdditiveUpgradePrice * (Level - 1));
            Damage = InitialDamage + (AdditiveDamage * (Level - 1));
            FireRate = (float)Math.Round(InitialFireRate - (AdditiveFireRate * (Level - 1)), 2);
            ReloadingDuration = (float)Math.Round(InitialReloadingDuration - (ReloadingAdditive * (Level - 1)), 2);
        }

        public void Upgrade()
        {
            if (Level == _maxLevel) return;
            PlayerSaves.IncrementWeaponLevel(ID);
            UpdateData();
        }

        public bool IsMaxLevel()
        {
            return Level >= _maxLevel;
        }
    }
}
