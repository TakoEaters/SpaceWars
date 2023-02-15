using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Common;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.Core.Pool;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.General.Utils.Audio;
using _Project.Scripts.GUi.Interface;
using _Project.Scripts.Player.WeaponsSystem;
using CustomAssets.Jammo_Character.Scripts;
using DG.Tweening;
using MoreMountains.NiceVibrations;
using UnityEngine;

namespace _Project.Scripts.ColorIt
{
    public class ShootingSystem : MonoBehaviour
    {
        [SerializeField] private CorePoolAudio _overHeatClip;

        MovementInput input;
        
        private static readonly int Shooting = Animator.StringToHash("shooting");

        private WeaponEntity _currentWeapon;
        private WeaponView _currentView;
        private Animator _animator;
        private PlayerInputs _inputs;
        private float _lastShootingTime;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            input = GetComponent<MovementInput>();
            _inputs = GetComponent<PlayerInputs>(); 
        }

        private void Start()
        {
            _currentWeapon = ServiceLocator.Current.Get<IWeaponHandler>().CurrentWeapon;
            List<WeaponView> views = GetComponentsInChildren<WeaponView>(true).ToList();
            _currentView = views[_currentWeapon.ID];
            _currentView.InitializeData(_currentWeapon.Damage);
            _currentView.Enable();
        }

        private void Update()
        {
            input.blockRotationPlayer = _inputs.IsShooting;
            Fire();
            OverheatUpdate();
        }

        private void VisualPolish()
        {

            if (!DOTween.IsTweening(_currentView.Nozzle))
            {
                _currentView.Nozzle.DOComplete();     
                _currentView.Nozzle.DOPunchScale(new Vector3(0, 1, 1) / 1.5f, .15f, 10, 1);
            }
        }

        #region Overheat

        [SerializeField] private float _intensity = 2f;
        
        public float LastCoolingTime;
        public float Overheat;
        public bool IsOverheat;
        
        // ReSharper disable Unity.PerformanceAnalysis
        private void Fire()
        {
            if (_inputs.IsShooting)
            {
                input.RotateToCamera(transform);
                if (!IsOverheat)
                {
                    if (Time.time - _lastShootingTime >= _currentWeapon.FireRate)
                    {
                        _currentView.ShootProjectile();
                        VisualPolish();
                        ServiceLocator.Current.Get<ICameraManager>().ShakeCamera(_intensity, 0.1f);
                        Overheat = Mathf.Clamp(Overheat + _currentWeapon.OverheatAdditive, 0f, 100f);
                        MMVibrationManager.Haptic(HapticTypes.SoftImpact, false, true, this); 
                        _animator.SetBool(Shooting, false);
                        _lastShootingTime = Time.time;
                        Signal.Current.Fire<Modifier>(new Modifier { Percentage = Overheat / 100f });
                    }
                }
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void OverheatUpdate()
        {
            if (Time.time - LastCoolingTime > 0.1f && Time.time - _lastShootingTime > _currentWeapon.FireRate + .1f)
            {
                Overheat = Mathf.Clamp(Overheat - _currentWeapon.CoolingPerSecond * .1f, 0f, 100f);
                Signal.Current.Fire<Modifier>(new Modifier {Percentage = Overheat / 100f});
                LastCoolingTime = Time.time;
            }

            if (Overheat >= 99) IsOverheat = true;
            else if (Overheat < _currentWeapon.MaxOverheat) IsOverheat = false;
            if (IsOverheat && _sentOverheatEvent == false)
            {
                MMVibrationManager.Haptic(HapticTypes.Failure, false, true, this);
                _sentOverheatEvent = true;
                OnOverheat();
           //     _onOverheat?.Invoke();
            }

            if (Overheat < 5f)
            {
                _sentOverheatEvent = false;
            }
            // if (_isPlayer)
            // {
            //     _overheatIndicator.UpdateValue(Overheat / 100f);
            //     _overheatIndicator.SetOverheat(IsOverheat);
            // }
        }

        private void OnOverheat()
        {
            CorePoolAudio overHeat = CorePool.Current.Get(_overHeatClip);
            overHeat.Play(); 
        }

        private bool _sentOverheatEvent;

        public void Cool()
        {
            Overheat = 0f;
            _sentOverheatEvent = false;
         //   _onCool?.Invoke();
        }

        #endregion
    }
}
