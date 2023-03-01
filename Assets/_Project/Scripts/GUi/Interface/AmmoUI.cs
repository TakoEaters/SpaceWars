using System;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.General.Signals;
using DG.Tweening;
using Template.Scripts.General;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.GUi.Interface
{
    public class AmmoUI : MonoBehaviour, IAmmoViewer
    {
        [SerializeField] private View _ammoView;
        [SerializeField] private TextMeshProUGUI _ammoText;

        private IAmmo _ammo;
        private bool _isReloading;
        
        public void Register()
        {
            ServiceLocator.Current.Register<IAmmoViewer>(this);
        }

        public void Subscribe(IAmmo ammoDisposer)
        {
            if (ammoDisposer != null && _ammo == ammoDisposer) return;
            _ammo = ammoDisposer;
            if (_ammo != null) _ammo.IsReloading = OnReload;
        }

        private void OnReload(float duration)
        {
            _isReloading = true;
            _ammoText.text = "RELOADING...";
            _ammoText.DOFade(0.85f, duration).SetLoops(2, LoopType.Yoyo).OnComplete(() =>
            {
                _isReloading = false;
            });
        }

        private void LateUpdate()
        {
            if (_isReloading) return;
            _ammoText.text = _ammo.Count + "/" + _ammo.Remaining;
        }

        [Sub]
        private void OnToggleView(ToggleGameplayUI reference)
        {
            if (reference.Enable) _ammoView.Enable();
            else _ammoView.Disable();
        }

        [Sub]
        private void OnFinishLevel(FinishLevel reference)
        {
            _ammoView.Disable();
        }
    }

    
    public interface IAmmoViewer : IGameService
    {
        public void Subscribe(IAmmo ammoDisposer);
    }
    
    public interface IAmmo
    {
        public int Count { get; }
        public int Remaining { get; }
        public Action<float> IsReloading { get; set; }
    }
}
