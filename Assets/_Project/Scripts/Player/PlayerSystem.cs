using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.General.Signals;
using _Project.Scripts.General.Utils;
using _Project.Scripts.GUi.Interface;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class PlayerSystem : MonoBehaviour, IPlayerSystem
    {
        [SerializeField, Range(0.5f, 2.0f)] private float _enableDelay = 1.0f;
        [SerializeField, Range(10, 50)] private int _playerRewardPerEnemy = 25;
        [SerializeField, Range(2, 15)] private int _bulletRewardPerEnemy = 10;
        [SerializeField] private Player _player;

        public int TotalKills { get; protected set;}
        public int TotalMultiKills { get; protected set; }
        public int TotalHeadshots { get; protected set;}
        public int PlayerReward { get; protected set; }
        public int BulletReward { get; protected set; }

        private bool _isReboot = true;

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
            Signal.Current.Fire<PlayerDeploy>(new PlayerDeploy());
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
        private void OnKillEnemy(OnKillTarget reference)
        {
            TotalKills += 1;
            if (reference.Headshot) TotalHeadshots += 1;
            PlayerReward += _playerRewardPerEnemy;
            BulletReward += _bulletRewardPerEnemy;
            if (_isReboot)
            {
                TotalMultiKills += 1;
                _isReboot = false;
            }
        }

        [Sub]
        private void OnPlayerDeath(PlayerDeath reference)
        {
            _isReboot = true;
            DisablePlayer();
        }
    }

    public interface IPlayerSystem : IGameService
    {
        public int TotalKills { get; }
        public int TotalMultiKills { get; }
        public int TotalHeadshots { get; }
        public int PlayerReward { get; }
        public int BulletReward { get; }
        public void InitializeSystem();
        public void EnablePlayer();

        public void UpdatePlayer();
        public void DisablePlayer();
    }
}
