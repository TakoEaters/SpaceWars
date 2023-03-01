using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.General.Signals;
using UnityEngine;

namespace _Project.Scripts.General.InputHandlers
{
    public class PlayerInputs : MonoBehaviour, IPlayerInputs
    {
        public Vector2 Movement => _input.Player.Joystick.ReadValue<Vector2>();
        public bool IsAimingPressed => _input.Player.Aiming.WasPressedThisFrame();
        public bool IsReloadingPressed => _input.Player.Reloading.WasPressedThisFrame();
        public bool IsAimingReleased => _input.Player.Aiming.WasReleasedThisFrame();
        public bool JumpingPressed => _input.Player.IsJumping.WasPressedThisFrame();
        public bool IsShooting => _input.Player.Shooting.ReadValue<float>() > 0;
        public bool IsRunning => _input.Player.Running.ReadValue<float>() > 0;
    
        private PlayerInput _input;
        private bool _isFinishScreen;

        private void Awake()
        {
            _input = new PlayerInput();
        }

        [Sub]
        private void OnFinishLevel(FinishLevel reference)
        {
            _isFinishScreen = true;
            _input.Disable();
        }

        private void OnEnable()
        {
            if (_isFinishScreen) return;
            _input.Enable();
        }

        public void Register()
        {
            ServiceLocator.Current.Register<IPlayerInputs>(this);
        }

        public void EnableInputs()
        {
            _input.Enable();
        }

        public void DisableInputs()
        {
            _input.Disable();
        }
    }

    public interface IPlayerInputs : IGameService
    {
        public void EnableInputs();
        public void DisableInputs();
    }
}
