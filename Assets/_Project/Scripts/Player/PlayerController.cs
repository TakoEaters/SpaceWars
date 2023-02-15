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
using DG.Tweening;
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

        protected bool IsDisabled = true;

        private ISpawnerSystem _spawnerSystem;

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
	        transform.position = _spawnerSystem.GetRandomSpawner(Team.Blue).SpawnPosition;
	        IsDisabled = false;
	        ServiceLocator.Current.Get<ICameraManager>().EnableCameraInput();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void UpdatePlayerData()
        {
	        PlayerAnimator.Rebind();
	        transform.position = _spawnerSystem.GetRandomSpawner(Team.Blue).SpawnPosition;
	        InitializeHealth();
	        InitializeWeapon();
        }

        public void DisableController()
        {
	        IsDisabled = true;
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
        private float _inputX;
        private float _inputZ;
        private float _speed;
        private bool _isGrounded;

        protected void UpdateMovement()
        {
	        if (IsDisabled) return;
	        InputMagnitude();

	        _isGrounded = Controller.isGrounded;
	        if (_isGrounded) _verticalVel -= 0;
	        else _verticalVel -= 1;

	        _movementVector = new Vector3(0, _verticalVel * .2f * Time.deltaTime, 0);
	        Controller.Move(_movementVector);
        }
        
        private void PlayerMoveAndRotation()
		{
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
				Controller.Move(_desiredMoveDirection * Time.deltaTime * _configs.Velocity);
			}
			else
			{
				//Strafe
				Controller.Move((transform.forward * _inputZ + transform.right * _inputX) * Time.deltaTime * _configs.Velocity);
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

			//Change animation mode if rotation is blocked
		//	PlayerAnimator.SetBool(Shooting, _blockRotationPlayer);

			//Physically move player
			if (_speed > _allowPlayerRotation)
			{
				PlayerAnimator.SetFloat(Blend, _speed, StartAnimTime, Time.deltaTime);
				PlayerAnimator.SetFloat(X, _inputX, StartAnimTime / 3, Time.deltaTime);
				PlayerAnimator.SetFloat(Y, _inputZ, StartAnimTime / 3, Time.deltaTime);
				PlayerMoveAndRotation();
			}
			else if (_speed < _allowPlayerRotation)
			{
				PlayerAnimator.SetFloat(Blend, _speed, StopAnimTime, Time.deltaTime);
				PlayerAnimator.SetFloat(X, _inputX, StopAnimTime / 3, Time.deltaTime);
				PlayerAnimator.SetFloat(Y, _inputZ, StopAnimTime / 3, Time.deltaTime);
			}
		}

        #endregion
        
        #region SHOOTING

        private List<WeaponView> _views;
        private WeaponEntity _weaponEntity;
        private WeaponView _currentWeapon;
        private float _lastShootingTime;
        private float _lastCoolingTime;
        private float _overheat;
        private bool _sentOverheatEvent;
        private bool _isOverheat;

        private void InitializeWeapon()
        {
            _weaponEntity = ServiceLocator.Current.Get<IWeaponHandler>().CurrentWeapon;
            _currentWeapon = _views[_weaponEntity.ID];
            _currentWeapon.InitializeData(_weaponEntity.Damage);
            _currentWeapon.Enable();
        }


        // ReSharper disable Unity.PerformanceAnalysis
        protected void UpdateWeapon()
        {
	        if (IsDisabled) return;
	        PlayerAnimator.SetBool(Shooting, Inputs.IsShooting);
	        _blockRotationPlayer = Inputs.IsShooting;
	        
	        
            if (Inputs.IsShooting)
            {
                RotateToCamera(transform);
                if (!_isOverheat)
                {
                    if (Time.time - _lastShootingTime >= _weaponEntity.FireRate)
                    {
                        _currentWeapon.ShootProjectile();
                        VisualPolish();
                        ServiceLocator.Current.Get<ICameraManager>().ShakeCamera(0.1f);
                        _overheat = Mathf.Clamp(_overheat + _weaponEntity.OverheatAdditive, 0f, 100f);
                        MMVibrationManager.Haptic(HapticTypes.SoftImpact, false, true, this);
                     //   PlayerAnimator.SetBool(Shooting, false);
                        _lastShootingTime = Time.time;
                        Signal.Current.Fire<Modifier>(new Modifier { Percentage = _overheat / 100f });
                    }
                }
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        protected void UpdateOverheat()
        {
	        if (IsDisabled) return;

            if (Time.time - _lastCoolingTime > 0.1f && Time.time - _lastShootingTime > _weaponEntity.FireRate + .1f)
            {
                _overheat = Mathf.Clamp(_overheat - _weaponEntity.CoolingPerSecond * .1f, 0f, 100f);
                Signal.Current.Fire<Modifier>(new Modifier { Percentage = _overheat / 100f });
                _lastCoolingTime = Time.time;
            }

            if (_overheat >= 99) _isOverheat = true;
            else if (_overheat < _weaponEntity.MaxOverheat) _isOverheat = false;
            if (_isOverheat && _sentOverheatEvent == false)
            {
                MMVibrationManager.Haptic(HapticTypes.Failure, false, true, this);
                _sentOverheatEvent = true;
                OnOverheat();
                //     _onOverheat?.Invoke();
            }

            if (_overheat < 5f)
            {
                _sentOverheatEvent = false;
            }
        }

        private void VisualPolish()
        {
            if (!DOTween.IsTweening(_currentWeapon.Nozzle))
            {
                _currentWeapon.Nozzle.DOComplete();
                _currentWeapon.Nozzle.DOPunchScale(new Vector3(0, 1, 1) / 1.5f, .15f, 10, 1);
            }
        }

        private void OnOverheat()
        {
	        CorePoolAudio overHeat = CorePool.Current.Get(_overHeatClip);
	        overHeat.Play(); 
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
	        if (IsDisabled) return;
	        float lerpSpeedClamped = 1 - (float)Health / _configs.Health;
            _effectsModifier.UpdateVignette(lerpSpeedClamped);
        }

        [Button()]
        public override void OnTakeDamage(int damage)
        {
            Health -= damage;
            HealthViewer.UpdateView(0, Health);

            if (Health <= 0) OnDeath();
        }


        private void OnDeath()
        {
	        if (_healthRoutine != null) StopCoroutine(_healthRoutine);
	        PlayerAnimator.enabled = false;
	        Signal.Current.Fire<PlayerDeath>(new PlayerDeath());
	        _effectsModifier.UpdateVignette(0);
        }

        #endregion


        #region HASHES

        private static readonly int Shooting = Animator.StringToHash("shooting");
        private static readonly int Blend = Animator.StringToHash("Blend");
        private static readonly int Y = Animator.StringToHash("Y");
        private static readonly int X = Animator.StringToHash("X");

        #endregion
    }
}
