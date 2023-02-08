using System.Collections.Generic;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.General.Saves;
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
    }

    public interface IWeaponHandler : IGameService
    {
        public WeaponEntity CurrentWeapon { get; }
    }
}