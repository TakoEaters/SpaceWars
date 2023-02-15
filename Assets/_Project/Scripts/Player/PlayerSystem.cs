using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.General.Signals;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class PlayerSystem : MonoBehaviour, IPlayerSystem
    {
        [SerializeField] private Player _player;
        
        public void Register()
        {
            Application.targetFrameRate = 60;
            ServiceLocator.Current.Register<IPlayerSystem>(this);
        }

        public void InitializeSystem()
        {
            _player.Initialize();
            _player.EnableController();
        }

        public void EnablePlayer()
        {
            _player.EnableController();
        }

        public void DisablePlayer()
        {
            _player.DisableController();
        }

        [Sub]
        private void OnPlayerDeath(PlayerDeath reference)
        {
            DisablePlayer();
        }
    }

    public interface IPlayerSystem : IGameService
    {
        public void InitializeSystem();
        public void EnablePlayer();
        public void DisablePlayer();
    }
}
