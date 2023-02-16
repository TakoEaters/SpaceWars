using System.Collections;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.General.Signals;
using _Project.Scripts.General.Utils;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class PlayerSystem : MonoBehaviour, IPlayerSystem
    {
        [SerializeField, Range(0.5f, 2.0f)] private float _enableDelay = 1.0f;
        [SerializeField] private Player _player;
        
        public void Register()
        {
            ServiceLocator.Current.Register<IPlayerSystem>(this);
        }

        public void InitializeSystem()
        {
            _player.Initialize();
            _player.UpdatePlayerData();
        }

        public void EnablePlayer()
        { 
            Signal.Current.Fire<PlayerRevive>(new PlayerRevive());
            StartCoroutine(WaitUtils.WaitWithDelay(_player.EnableController, _enableDelay));
        }

        public void UpdatePlayer()
        {
            StartCoroutine(WaitUtils.WaitWithDelay(_player.UpdatePlayerData, _enableDelay));
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

        public void UpdatePlayer();
        public void DisablePlayer();
    }
}
