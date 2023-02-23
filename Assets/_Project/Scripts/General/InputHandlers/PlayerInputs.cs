using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.General.Signals;
using UnityEngine;

namespace _Project.Scripts.General.InputHandlers
{
    public class PlayerInputs : MonoBehaviour, IPlayerInputs
    {
        public Vector2 Movement => _input.Player.Joystick.ReadValue<Vector2>();
        public Vector2 Rotation => _input.Player.Rotation.ReadValue<Vector2>();
    
        public bool IsShootingPressed => _input.Player.Shooting.WasPressedThisFrame();
        public bool IsShootingReleased => _input.Player.Shooting.WasReleasedThisFrame();
        public bool IsShooting => _input.Player.Shooting.ReadValue<float>() > 0;
    
        private PlayerInput _input;

        private void Awake()
        {
            _input = new PlayerInput();
        }

        [Sub]
        private void OnFinishLevel(FinishLevel reference)
        {
            _input.Disable();
        }

        private void OnEnable()
        {
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
