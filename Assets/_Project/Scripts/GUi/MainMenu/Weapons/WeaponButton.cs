using System;
using _Project.Scripts.Audio;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.General.Saves;
using _Project.Scripts.Player.WeaponsSystem;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.GUi.MainMenu.Weapons
{
    public class WeaponButton : MonoBehaviour
    {
        [SerializeField] private GameObject _lockView;
        [SerializeField] private WeaponEntity _entity;
        [SerializeField] private Image _selected;
        [SerializeField] private float _selectDuration = 0.25f;

        public WeaponEntity Entity => _entity;
        
        private Button _button;
        private Action<WeaponButton, bool> _onPreview;
        private bool _isSelected;
        
        public void Initialize(Action<WeaponButton, bool> onPreview)
        { 
            _lockView.SetActive(PlayerSaves.IsWeaponLocked(Entity.ID));
            Entity.UpdateData();
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnPreview);
            _onPreview = onPreview;
        }

        public void Select()
        {
            _isSelected = true;
            _onPreview?.Invoke(this, false);
            _selected.DOFade(1f, _selectDuration);
        }

        public void Unselect()
        {
            _isSelected = false;
            _selected.DOFade(0f, _selectDuration);
        }

        public void UpdateView()
        {
            _lockView.SetActive(false);
        }
        
        private void OnPreview()
        {
            if (_isSelected) return;
            _selected.DOFade(1f, _selectDuration);
            _onPreview?.Invoke(this, true);
            _isSelected = true;
        }
    }
}
