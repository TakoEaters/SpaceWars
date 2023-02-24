using _Project.Scripts.General.Saves;
using UnityEngine;

namespace _Project.Scripts.Player.WeaponsSystem
{
    [CreateAssetMenu(fileName = "WeaponEntity", menuName = "Configs/Weapons/WeaponData")]
    public class WeaponEntity : ScriptableObject
    {
        [Range(0, 5)] public int ID;
        [Range(1.0f, 100.0f)] public float MaxOverheat;
        [Range(1.0f, 50.0f)] public float CoolingPerSecond;
        [Range(0.01f, 2.0f)] public float FireRate;
        [Range(1.0f, 20.0f)] public float OverheatAdditive = 1.25f;
        [Range(10, 100)] public int Damage;

        public int WeaponPrice;
        
        [HideInInspector] public int Level; 
        [HideInInspector] public int UpgradePrice;
        
        public int InitialUpgradePrice; 
        public int AdditiveUpgradePrice;
        
        public int InitialDamage; 
        public int AdditiveDamage;
        
        public float InitialOverheat; 
        public float AdditiveOverheat;
        
        public float InitialFireRate; 
        public float AdditiveFireRate;

        private readonly int _maxLevel = 10;

        public void UpdateData()
        {
            Level = PlayerSaves.GetWeaponLevel(ID);
            UpgradePrice = InitialUpgradePrice + (AdditiveUpgradePrice * (Level - 1));
            Damage = InitialDamage + (AdditiveDamage * (Level - 1));
            FireRate = InitialFireRate - (AdditiveFireRate * (Level - 1));
            OverheatAdditive = InitialOverheat - (AdditiveOverheat * (Level - 1));
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
