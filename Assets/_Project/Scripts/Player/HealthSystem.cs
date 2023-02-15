using System;
using _Project.Scripts.Common;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.General;
using _Project.Scripts.General.DamageableCore;
using _Project.Scripts.General.Signals;
using _Project.Scripts.GUi.Interface;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering;
using Vignette = UnityEngine.Rendering.Universal.Vignette;

namespace _Project.Scripts.Player
{
    public class HealthSystem : MonoBehaviour, IDamageable
    {
        [SerializeField] private Animator _animatorController;
        [SerializeField] private VolumeProfile _volumeProfile;
        [SerializeField, Range(0.1f, 0.5f)] private float _maxVignetteRadius = 0.35f;
        [SerializeField, Range(10, 250)] private int _playerTotalHealth = 250;

        private Vignette _vignette;
        private int _currentHealth;

        public DamageableLayer DamageableLayer { get; }
        public Vector3 Position { get; }
        public int Priority { get; }
        public bool IsInSafeZone { get; set; }
        public bool IsAlive => _currentHealth > 0;

        private void Awake()
        {
            _volumeProfile.TryGet(out _vignette);
            _vignette.intensity.value = 0;
            _currentHealth = _playerTotalHealth;
        }

        private void Start()
        {
            Signal.Current.Fire<ChangeUIHealth>(new ChangeUIHealth {TotalHealth = _currentHealth});
        }

        private void LateUpdate()
        {
            // ReSharper disable once PossibleLossOfFraction
            float lerpSpeedClamped =  1 - (float)_currentHealth / _playerTotalHealth;
            _vignette.intensity.value = Mathf.Lerp(0, _maxVignetteRadius, lerpSpeedClamped);
        }

        [Button()]
        public void OnTakeDamage(int damage)
        {
            _currentHealth -= damage;
            Signal.Current.Fire<ChangeUIHealth>(new ChangeUIHealth {CurrentHealth = _currentHealth});

            if (_currentHealth <= 0)
            {
                _maxVignetteRadius = 0;
                Signal.Current.Fire<PlayerDeath>(new PlayerDeath());
                _animatorController.enabled = false;
            }
        }

        private void OnDestroy()
        {
            _vignette.intensity.value = 0;
        }
    }
}
