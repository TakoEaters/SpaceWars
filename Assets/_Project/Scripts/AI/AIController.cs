using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.General.Spawners;
using _Project.Scripts.Player.SkinChanger;
using _Project.Scripts.Player.WeaponsSystem;
using UnityEngine;

namespace _Project.Scripts.AI
{
    public class AIController : AIBehaviour
    {
        protected CharacterController Controller;
        protected Animator Animator;
        protected SkinsChanger SkinsChanger;
        protected Action OnDeath;


        private ISpawnerSystem _spawnerSystem;
        private bool _isDisabled = true;

        protected void FindServices()
        {
            _spawnerSystem = ServiceLocator.Current.Get<ISpawnerSystem>();
            _views = GetComponentsInChildren<WeaponView>(true).ToList();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void EnableController()
        {
            _isDisabled = false;
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
        }

        #region SHOOTING

        private List<WeaponView> _views;
        private WeaponEntity _weaponEntity;
        private WeaponView _currentWeapon;
        
        private void InitializeWeapon()
        {
            _weaponEntity = ServiceLocator.Current.Get<IWeaponHandler>().RandomWeapon;
            _currentWeapon = _views[_weaponEntity.ID];
            _currentWeapon.InitializeData(_weaponEntity.Damage);
            _currentWeapon.Enable();
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
            else Animator.SetTrigger(Hit);
        }


        private void OnDie()
        {
            if (_healthRoutine != null) StopCoroutine(_healthRoutine);
            Controller.enabled = false;
            Animator.enabled = false;
            OnDeath?.Invoke();
        }
        #endregion
        
        #region HASHES

        private static readonly int Shooting = Animator.StringToHash("shooting");
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Blend = Animator.StringToHash("Blend");
        private static readonly int Y = Animator.StringToHash("Y");
        private static readonly int X = Animator.StringToHash("X");

        #endregion
    }
}
