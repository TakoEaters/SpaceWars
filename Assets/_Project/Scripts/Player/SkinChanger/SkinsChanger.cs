using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.General.Saves;
using MyBox;
using UnityEngine;

namespace _Project.Scripts.Player.SkinChanger
{
    public class SkinsChanger : MonoBehaviour
    {
        [SerializeField] private Material _headMaterial;
        [SerializeField] private Material _bodyMaterial;

        private List<Skin> _skins = new List<Skin>();
        private List<SkinnedMeshRenderer> _meshRenderers = new List<SkinnedMeshRenderer>();
        private Skin _currentSkin;
        private Material _initialBodyMaterial;
        private Material _initialHeadMaterial;

        public void SetSkin()
        {
            _skins = GetComponentsInChildren<Skin>(true).ToList();
            _skins.ForEach(x => x.Disable());
            _currentSkin = _skins[PlayerSaves.GetSkinID()];
            _currentSkin.Enable();
            _meshRenderers = _currentSkin.GetComponentsInChildren<SkinnedMeshRenderer>(true).ToList();
            Material[] bodyMaterials = _meshRenderers[0].materials;
            Material[] headMaterials = _meshRenderers[1].materials;
            _initialBodyMaterial = bodyMaterials[0];
            _initialHeadMaterial = headMaterials[0];
        }

        public void ToggleTransparency(bool value)
        {
            Material[] bodyMaterials = _meshRenderers[0].materials;
            Material[] headMaterials = _meshRenderers[1].materials;
            bodyMaterials[0] = value ? _bodyMaterial : _initialBodyMaterial;
            headMaterials[0] = value ? _headMaterial : _initialHeadMaterial;
            _meshRenderers[0].materials = bodyMaterials;
            _meshRenderers[1].materials = headMaterials;
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
