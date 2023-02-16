using System.Collections.Generic;
using System.Linq;
using MyBox;
using UnityEngine;

namespace _Project.Scripts.Player.SkinChanger
{
    public class SkinsChanger : MonoBehaviour
    {
        private List<Skin> _skins = new List<Skin>();
        private Skin _currentSkin;

        private void Awake()
        {
            _skins = GetComponentsInChildren<Skin>(true).ToList();
            _skins.ForEach(x => x.Disable());
            _currentSkin = _skins.GetRandom();
            _currentSkin.Enable();
        }

        public void EnableMesh()
        {
            _currentSkin.Enable();
        }
        
        public void DisableMesh()
        {
            _currentSkin.Disable();
        }
    }
}
