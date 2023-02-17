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
        protected CharacterController Controller;
        protected DamageableScanner Scanner;
        protected Animator Animator;
        protected SkinsChanger SkinsChanger;
        protected AIStates States;

        protected Action OnDeath;
        
        private ISpawnerSystem _spawnerSystem;
        private NavMeshAgent _agent;
        private bool _isDisabled = true;

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
            _isDisabled = false;
            Scanner.StartScanning(OnDetectEnemy, DamageableLayer, _configs.Team);
            States.Enable();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void UpdateBotData()
        {
            Controller.enabled = true;
            transform.position = _spawnerSystem.GetRandomSpawner(_configs.Team).SpawnPosition;
            Animator.Rebind();
            InitializeHealth();
            InitializeWeapon();
            SkinsChanger.EnableMesh();
        }

        public void DisableController()
        {
            _isDisabled = false;
            Scanner.StopScanning();
            States.Disable();
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
        private float _lastCoolingTime;
        private float _overheat;
        private bool _isOverheat;

        private void InitializeWeapon()
        {
            _weaponEntity = ServiceLocator.Current.Get<IWeaponHandler>().RandomWeapon;
            _currentWeapon = _views[_weaponEntity.ID];
            _currentWeapon.InitializeData(_configs.Team, _weaponEntity.Damage);
            _currentWeapon.Enable();
        }

        private void OnDetectEnemy(IDamageable target)
        {
            if (target == null)
            {
                _currentTarget = null;
                return;
            }
            if (_currentTarget != null && _currentTarget == target) return;
            States.SetTarget(target.Position);
            _currentTarget = target;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        protected void OnAttack()
        {
            if (_currentTarget != null && _currentTarget.IsAlive)
            {
                if (!_isOverheat)
                {
                    if (Time.time - _lastShootingTime >= _weaponEntity.FireRate)
                    {
                        _currentWeapon.ShootProjectile(_currentTarget);
                        _overheat = Mathf.Clamp(_overheat + _weaponEntity.OverheatAdditive, 0f, 100f);
                        _lastShootingTime = Time.time;
                    }
                }
            }
        }
        
        protected void UpdateOverheat()
        {
            if (_isDisabled) return;
            if (Time.time - _lastCoolingTime > 0.1f && Time.time - _lastShootingTime > _weaponEntity.FireRate + .1f)
            {
                _overheat = Mathf.Clamp(_overheat - _weaponEntity.CoolingPerSecond * .1f, 0f, 100f);
                _lastCoolingTime = Time.time;
            }

            if (_overheat >= 99) _isOverheat = true;
            else if (_overheat < _weaponEntity.MaxOverheat) _isOverheat = false;
            if (_isOverheat)
            {
              //  OnOverheat();
            }
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
            else {Animator.SetTrigger(AnimationHash.Hit);}
        }


        private void OnDie()
        {
            if (_healthRoutine != null) StopCoroutine(_healthRoutine);
            Signal.Current.Fire<ScoreChanger>(new ScoreChanger { Team = _configs.Team });
            States.Disable();
            Controller.enabled = false;
            Animator.enabled = false;
            OnDeath?.Invoke();
        }
        #endregion

        #region ANIMATOR

        protected void UpdateAnimator()
        {
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
