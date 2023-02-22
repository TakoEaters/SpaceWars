using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.General.Saves;
using _Project.Scripts.Player.SkinChanger;
using _Project.Scripts.Player.WeaponsSystem;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class CharacterViewer : MonoBehaviour
    {
        private List<WeaponView> _views = new List<WeaponView>();
        private WeaponView _currentView;
        private SkinsChanger _changer;
        private void Awake()
        {
            _changer = GetComponentInChildren<SkinsChanger>();
            _views = GetComponentsInChildren<WeaponView>(true).ToList();
            _currentView = _views[PlayerSaves.GetPlayerWeapon()];
            _changer.SetSkin();
            _currentView.Enable();
        }
    }
}
