using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Common;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.Core.Pool;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.General.InputHandlers;
using _Project.Scripts.General.Signals;
using _Project.Scripts.General.Spawners;
using _Project.Scripts.General.Utils;
using _Project.Scripts.General.Utils.Audio;
using _Project.Scripts.GUi.Interface;
using _Project.Scripts.Player.WeaponsSystem;
using MoreMountains.NiceVibrations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.Player
{
    [RequireComponent(typeof(CharacterController), typeof(PlayerInputs))]
    public class PlayerController : Character
    {
	    [SerializeField] private CorePoolAudio _overHeatClip;
	    
        protected CharacterController Controller;
        protected PlayerInputs Inputs;
        protected Animator PlayerAnimator;
        protected Camera Camera;

        private ISpawnerSystem _spawnerSystem;
        private bool _isDisabled = true;

        protected void FindServices()
        {
	        _spawnerSystem = ServiceLocator.Current.Get<ISpawnerSystem>();
	        HealthViewer = ServiceLocator.Current.Get<IHealthViewer>();
            _effectsModifier = ServiceLocator.Current.Get<IEffectsModifier>();
            _views = GetComponentsInChildren<WeaponView>(true).ToList();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void EnableController()
        {
	        _isDisabled = false;
	        ServiceLocator.Current.Get<ICameraManager>().EnableCameraInput();
	        ServiceLocator.Current.Get<IScoreSystem>().EnableScoreView();
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        public void UpdatePlayerData()
        {
	        transform.position = _spawnerSystem.GetRandomSpawner(_configs.Team).SpawnPosition;
	        InitializeHealth();
	        InitializeWeapon();
	        PlayerAnimator.Rebind();
        }

        public void DisableController()
        {
	        _isDisabled = true;
        }

        #region MOVEMENT
        
        [Range(0, 1f)] public float StartAnimTime = 0.3f;
        [Range(0, 1f)] public float StopAnimTime = 0.15f;
        
        [SerializeField] private float _desiredRotationSpeed = 0.3f;
        [SerializeField] private bool _blockRotationPlayer;
        [SerializeField] private float _allowPlayerRotation = 0.1f;
        [SerializeField] private float _verticalVel = -0.5f;
        
        private Vector3 _movementVector;
        private Vector3 _desiredMoveDirection;
        private float _movementVelocity;
        private float _inputX;
        private float _inputZ;
        private float _speed;
        private bool _isGrounded;

        protected void UpdateMovement()
        {
	        if (_isDisabled) return;
	        InputMagnitude();

	        _isGrounded = Controller.isGrounded;
	        if (_isGrounded) _verticalVel -= 0;
	        else _verticalVel -= 1;

	        _movementVector = new Vector3(0, _verticalVel * .2f * Time.deltaTime, 0);
	        Controller.Move(_movementVector);
        }
        
        private void PlayerMoveAndRotation()
        {
	        bool isRunning = Inputs.IsRunning;
	        PlayerAnimator.SetBool(AnimationHash.Running, isRunning);
	        _movementVelocity = isRunning ? _configs.RunningVelocity : _configs.Velocity;
			
			_inputX = Inputs.Movement.x;
			_inputZ = Inputs.Movement.y;
			
			Vector3 forward = Camera.transform.forward;
			Vector3 right = Camera.transform.right;

			forward.y = 0f;
			right.y = 0f;

			forward.Normalize();
			right.Normalize();

			_desiredMoveDirection = forward * _inputZ + right * _inputX;

			if (_blockRotationPlayer == false)
			{
				//Camera
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_desiredMoveDirection),
					_desiredRotationSpeed);
				Controller.Move(_desiredMoveDirection * Time.deltaTime * _movementVelocity);
			}
			else
			{
				//Strafe
				Controller.Move((transform.forward * _inputZ + transform.right * _inputX) * Time.deltaTime * _movementVelocity);
			}
		}

		private void RotateToCamera(Transform transform)
		{
			var forward = Camera.transform.forward;

			_desiredMoveDirection = forward;
			Quaternion lookAtRotation = Quaternion.LookRotation(_desiredMoveDirection);
			Quaternion lookAtRotationOnly_Y = Quaternion.Euler(transform.rotation.eulerAngles.x,
				lookAtRotation.eulerAngles.y, transform.rotation.eulerAngles.z);

			transform.rotation = Quaternion.Slerp(transform.rotation, lookAtRotationOnly_Y, _desiredRotationSpeed);
		}

		private void InputMagnitude()
		{
			//Calculate Input Vectors
			_inputX = Inputs.Movement.x;
			_inputZ = Inputs.Movement.y;

			//Calculate the Input Magnitude
			_speed = new Vector2(_inputX, _inputZ).sqrMagnitude;

			//Physically move player
			if (_speed > _allowPlayerRotation)
			{
				PlayerAnimator.SetFloat(AnimationHash.Blend, _speed, StartAnimTime, Time.deltaTime);
				PlayerAnimator.SetFloat(AnimationHash.X, _inputX, StartAnimTime / 3, Time.deltaTime);
				PlayerAnimator.SetFloat(AnimationHash.Y, _inputZ, StartAnimTime / 3, Time.deltaTime);
				PlayerMoveAndRotation();
			}
			else if (_speed < _allowPlayerRotation)
			{
				PlayerAnimator.SetFloat(AnimationHash.Blend, _speed, StopAnimTime, Time.deltaTime);
				PlayerAnimator.SetFloat(AnimationHash.X, _inputX, StopAnimTime / 3, Time.deltaTime);
				PlayerAnimator.SetFloat(AnimationHash.Y, _inputZ, StopAnimTime / 3, Time.deltaTime);
			}
		}

        #endregion
        
        #region SHOOTING
        
        public override int Count { get; protected set; }
        public override int Remaining { get; protected set; }
        public override Action<float> IsReloading { get; protected set; }

        private List<WeaponView> _views;
        private WeaponEntity _weaponEntity;
        private WeaponView _currentWeapon;
        private float _lastShootingTime;
        private bool _isReloading;

        private void InitializeWeapon()
        {
	        ServiceLocator.Current.Get<IAmmoViewer>().Subscribe(this);
	        ServiceLocator.Current.Get<ICameraManager>().ToggleDistance(false);
            _weaponEntity = ServiceLocator.Current.Get<IWeaponHandler>().CurrentWeapon;
            _currentWeapon = _views[_weaponEntity.ID];
            _currentWeapon.InitializeData(_configs.Team, _weaponEntity.Damage);
            _currentWeapon.Enable();
            Count = _weaponEntity.MagazineAmmo;
            Remaining = _weaponEntity.TotalAmmo - Count;
        }


        // ReSharper disable Unity.PerformanceAnalysis
        protected void UpdateWeapon()
        {
	        if (_isDisabled) return;
	        if (Inputs.IsReloadingPressed) OnReload();
	        PlayerAnimator.SetBool(AnimationHash.Shooting, Inputs.IsShooting);
	        _blockRotationPlayer = Inputs.IsShooting;

	        if (Inputs.IsShooting)
            {
                RotateToCamera(transform);
                if (_isReloading) return;
                if (Count > 0)
                {
                    if (Time.time - _lastShootingTime >= _weaponEntity.FireRate)
                    {
	                    Count--;
                        _currentWeapon.ShootProjectile();
                        ServiceLocator.Current.Get<ICameraManager>().ShakeCamera(0.1f);
                        _lastShootingTime = Time.time;
                    }
                }

                else OnReload();
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        protected void UpdateAiming()
        {
	        if (_isDisabled) return;

	        if (Inputs.IsAimingPressed)
	        {
		        ServiceLocator.Current.Get<ICameraManager>().ToggleDistance(true);
	        }
	        
	        else if (Inputs.IsAimingReleased)
	        {
		        ServiceLocator.Current.Get<ICameraManager>().ToggleDistance(false);
	        }
        }

        private void OnReload()
        {
	        if (Remaining <= 0)
	        {
		        Remaining = 0;
		        return;
	        }
	        _isReloading = true;
	        IsReloading?.Invoke(_weaponEntity.ReloadingDuration);
	        StartCoroutine(WaitUtils.WaitWithDelay(() =>
	        {
		        int necessaryAmmoCount = _weaponEntity.MagazineAmmo;
		        if (necessaryAmmoCount > Remaining)
		        {
			        Count = Remaining;
			        Remaining = 0;
		        }

		        else
		        {
			        Count = necessaryAmmoCount;
			        Remaining -= Count;
		        }
		    
		        _isReloading = false;
	        }, _weaponEntity.ReloadingDuration));
        }

        #endregion
        
        #region HEALTH/DAMAGE

        private IEffectsModifier _effectsModifier;
        private Coroutine _healthRoutine;

        private void InitializeHealth()
        {
	        PlayerAnimator.enabled = true; 
	        Health = _configs.Health;
	        HealthViewer.UpdateView(Health, Health);
	        _healthRoutine = StartCoroutine(RestoreHealth());
        }

        protected void LerpHealth()
        {
	        if (_isDisabled) return;
	        float lerpSpeedClamped = 1 - (float)Health / _configs.Health;
            _effectsModifier.UpdateVignette(lerpSpeedClamped);
        }

        [Button()]
        public override void OnTakeDamage(int damage)
        {
            Health -= damage;
            HealthViewer.UpdateView(0, Health);

            if (Health <= 0) OnDeath();
            else PlayerAnimator.SetTrigger(AnimationHash.Hit);
        }


        private void OnDeath()
        {
	        if (_healthRoutine != null) StopCoroutine(_healthRoutine);
	        PlayerAnimator.enabled = false;
	        Signal.Current.Fire<PlayerDeath>(new PlayerDeath());
	        _effectsModifier.UpdateVignette(0);
        }

        #endregion
    }
}
