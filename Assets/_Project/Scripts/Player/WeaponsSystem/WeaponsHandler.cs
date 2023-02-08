using System.Collections.Generic;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.General.Saves;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.Player.WeaponsSystem
{
    public class WeaponsHandler : MonoBehaviour, IWeaponHandler
    {
        [SerializeField] private List<WeaponEntity> _weapons = new List<WeaponEntity>();

        public WeaponEntity CurrentWeapon { get; protected set; }
        
        public void Register()
        {
            ServiceLocator.Current.Register<IWeaponHandler>(this);
            CurrentWeapon = _weapons.Find(x => x.ID == PlayerSaves.GetPlayerWeapon());
        }
        
        public void UpdateWeapon(int weaponID)
        {
            CurrentWeapon = _weapons.Find(x => x.ID == weaponID);
            PlayerSaves.SetPlayerWeapon(weaponID);
        }

        [Button]
        public void SetTestWeapon(int buttonID)
        {
            PlayerSaves.SetPlayerWeapon(buttonID);
        }
    }

    public interface IWeaponHandler : IGameService
    {
        public WeaponEntity CurrentWeapon { get; }

        public void UpdateWeapon(int weaponID);
    }
}