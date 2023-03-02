using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.General.DamageableCore;
using _Project.Scripts.General.Signals;
using _Project.Scripts.General.Spawners;
using _Project.Scripts.General.Utils;
using _Project.Scripts.Player.SkinChanger;
using _Project.Scripts.Player.WeaponsSystem;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.AI
{
    public class AIController : AIBehaviour
    {
        protected CapsuleCollider Collider;
        protected DamageableScanner Scanner;
        protected Animator Animator;
        protected SkinsChanger SkinsChanger;
        protected AIStates States;

        protected Action OnDeath;
        
        private ISpawnerSystem _spawnerSystem;
        private NavMeshAgent _agent;
        private bool _isDisabled = true;
        private bool _isFinish;

        protected void FindServices()
        {
            _agent = GetComponent<NavMeshAgent>();
            _spawnerSystem = ServiceLocator.Current.Get<ISpawnerSystem>();
            _views = GetComponentsInChildren<WeaponView>(true).ToList();
            States.Initialize(_configs, _agent);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void EnableController()
        {
            if (_isFinish) return;
            _isDisabled = false;
            Scanner.StartScanning(OnDetectEnemy, DamageableLayer, _configs.Team);
            States.Enable();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void UpdateBotData()
        {
            if (_isFinish) return;
            Collider.enabled = true;
            transform.position = _spawnerSystem.GetRandomSpawner(_configs.Team).SpawnPosition;
            Animator.Rebind();
            InitializeHealth();
            InitializeWeapon();
            SkinsChanger.EnableMesh();
        }

        public void DisableAtFinish()
        {
            _isFinish = true;
            DisableController();
        }

        public void DisableController()
        {
            _isDisabled = false;
            Scanner.StopScanning();
            States.Disable();
            _currentTarget = null;
        }

        #region STATES_MANAGER
        
        private Vector3 _velocity;

        private readonly float _allowRotation = 0.1f;
        private readonly float _startAnimationTime = 0.3f;
        private readonly float _stopAnimationTime = 0.15f;
        private float _speed;

        protected void UpdateStates()
        {
            if (_isDisabled) return;
            States.UpdateStates();
        }


        #endregion

        #region SHOOTING

        private IDamageable _currentTarget;
        private List<WeaponView> _views;
        private WeaponEntity _weaponEntity;
        private WeaponView _currentWeapon;

        private float _lastShootingTime;
        private int _ammoCount;
        private bool _isReloading;
        private bool _isShooting;

        private void InitializeWeapon()
        {
            _weaponEntity = ServiceLocator.Current.Get<IWeaponHandler>().RandomWeapon;
            _currentWeapon = _views[_weaponEntity.ID];
            _currentWeapon.InitializeData(_configs.Team, _weaponEntity.Damage);
            _currentWeapon.Enable();
            _ammoCount = _weaponEntity.MagazineAmmo;
        }

        private void OnDetectEnemy(IDamageable target)
        {
            if (target == null) return;
            _currentTarget = target;
            States.SetTarget(target.Position);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        protected void OnAttack()
        {
            if (_isDisabled) return;
            _isShooting = false;
            if (_currentTarget is { IsAlive: false })
            {
                _currentTarget = null;
                if (_isDisabled) return;
                States.MoveRandom();
            }
            if (_currentTarget is { IsAlive: true })
            {
                if (!_isReloading)
                {
                    if (Time.time - _lastShootingTime >= _weaponEntity.FireRate)
                    {
                        _isShooting = true;
                        _currentWeapon.ShootProjectile(_currentTarget);
                        _lastShootingTime = Time.time;
                        _ammoCount--;
                        if (_ammoCount <= 0) OnReload();
                    }
                }
            }
        }

        private void OnReload()
        {
            _isReloading = true;
            StartCoroutine(WaitUtils.WaitWithDelay(() =>
            {
                _ammoCount = _weaponEntity.MagazineAmmo;
            }, _weaponEntity.ReloadingDuration));
        }

        #endregion

        #region HEALTH/DAMAGE

        private Coroutine _healthRoutine;

        private void InitializeHealth()
        {
            Animator.enabled = true;
            Health = _configs.Health;
            _healthRoutine = StartCoroutine(RestoreHealth());
        }
        
        public override void OnTakeDamage(int damage)
        {
            Health -= damage;

            if (Health <= 0) OnDie();
            else Animator.SetTrigger(AnimationHash.Hit);
        }


        private void OnDie()
        {
            if (_healthRoutine != null) StopCoroutine(_healthRoutine);
            Signal.Current.Fire<ScoreChanger>(new ScoreChanger { Team = _configs.Team });
            States.Disable();
            Collider.enabled = false;
            Animator.enabled = false;
            OnDeath?.Invoke();
        }
        #endregion

        #region ANIMATOR

        protected void UpdateAnimator()
        {
            Animator.SetBool(AnimationHash.Shooting, _isShooting);
            _velocity = _agent.velocity;
            _speed = _velocity.sqrMagnitude;
            
            if (_speed > _allowRotation)
            {
                Animator.SetFloat(AnimationHash.Blend, _speed, _startAnimationTime, Time.deltaTime); 
                Animator.SetFloat(AnimationHash.X, _velocity.x, _startAnimationTime / 3, Time.deltaTime); 
                Animator.SetFloat(AnimationHash.Y, _velocity.z, _startAnimationTime / 3, Time.deltaTime);
            }
            else if (_speed < _allowRotation)
            {
                Animator.SetFloat(AnimationHash.Blend, _speed, _stopAnimationTime, Time.deltaTime); 
                Animator.SetFloat(AnimationHash.X, _velocity.x, _stopAnimationTime / 3, Time.deltaTime); 
                Animator.SetFloat(AnimationHash.Y, _velocity.z, _stopAnimationTime / 3, Time.deltaTime);
            }
        }

        #endregion
    }
}
