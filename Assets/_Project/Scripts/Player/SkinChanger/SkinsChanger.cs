using System.Collections.Generic;
using System.Linq;
using MyBox;
using UnityEngine;

namespace _Project.Scripts.Player.SkinChanger
{
    public class SkinsChanger : MonoBehaviour
    {
        private List<Skin> _skins = new List<Skin>();

        private void Awake()
        {
            _skins = GetComponentsInChildren<Skin>(true).ToList();
            _skins.ForEach(x => x.Disable());
            Skin necessary = _skins.GetRandom();
            necessary.Enable();
        }
    }
}
