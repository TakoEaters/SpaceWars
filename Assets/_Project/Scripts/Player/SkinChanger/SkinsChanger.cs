using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.General.Saves;
using MyBox;
using UnityEngine;

namespace _Project.Scripts.Player.SkinChanger
{
    public class SkinsChanger : MonoBehaviour
    {
        private List<Skin> _skins = new List<Skin>();
        private Skin _currentSkin;

        public void SetSkin()
        {
            _skins = GetComponentsInChildren<Skin>(true).ToList();
            _skins.ForEach(x => x.Disable());
            _currentSkin = _skins[PlayerSaves.GetSkinID()];
            _currentSkin.Enable();
        }

        public void EnableMesh()
        {
            InitializeSkin();
        }
        
        private void InitializeSkin()
        {
            if (_currentSkin)
            {
                _currentSkin.Enable();
                return;
            }
            _skins = GetComponentsInChildren<Skin>(true).ToList();
            _skins.ForEach(x => x.Disable());
            _currentSkin = _skins.GetRandom();
            _currentSkin.Enable();
        }
        
        public void DisableMesh()
        {
            _currentSkin.Disable();
        }
    }
}
