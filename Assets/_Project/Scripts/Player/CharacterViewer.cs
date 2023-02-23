using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.General.Saves;
using _Project.Scripts.Player.SkinChanger;
using _Project.Scripts.Player.WeaponsSystem;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class CharacterViewer : MonoBehaviour, ICharacterViewer
    {
        private List<WeaponView> _views = new List<WeaponView>();
        private WeaponView _currentView;
        private SkinsChanger _changer;
        
        public void Register()
        {
            ServiceLocator.Current.Register<ICharacterViewer>(this);
            _changer = GetComponentInChildren<SkinsChanger>();
            _views = GetComponentsInChildren<WeaponView>(true).ToList();
            _currentView = _views[PlayerSaves.GetPlayerWeapon()];
            _changer.SetSkin();
            _currentView.Enable();
        }

        public void UpdateWeapon()
        {
            _currentView.Disable();
            _currentView = _views[PlayerSaves.GetPlayerWeapon()];
            _currentView.Enable();
        }
    }

    public interface ICharacterViewer : IGameService
    {
        public void UpdateWeapon();
    }
}
