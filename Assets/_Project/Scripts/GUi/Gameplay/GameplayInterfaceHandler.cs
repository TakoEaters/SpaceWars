using _Project.Scripts.Common;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.General.Signals;
using _Project.Scripts.General.Utils;
using _Project.Scripts.Player;
using Template.Scripts.General;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.GUi.Gameplay
{
    public class GameplayInterfaceHandler : MonoBehaviour
    {
        [SerializeField, Range(0.5f, 2.0f)] private float _enableDelay = 1.75f;
        [SerializeField] private View _mainView;
        [SerializeField] private Button _deployButton;
        [SerializeField] private Button _spawnReturningButton;


        private void Awake()
        {
            _mainView.Enable();
            _deployButton.onClick.AddListener(OnRevivePlayer);
            _spawnReturningButton.onClick.AddListener(OnReturnToSpawn);
        }

        private void OnRevivePlayer()
        {
            _deployButton.gameObject.SetActive(false);
            _spawnReturningButton.gameObject.SetActive(false);
            ServiceLocator.Current.Get<IPlayerSystem>().EnablePlayer();
        }

        private void OnReturnToSpawn()
        {
            _spawnReturningButton.gameObject.SetActive(false);
            StartCoroutine(WaitUtils.WaitWithDelay(() => _deployButton.gameObject.SetActive(true), _enableDelay));
            ServiceLocator.Current.Get<IPlayerSystem>().UpdatePlayer();
            ServiceLocator.Current.Get<ICameraManager>().EnableGameplayCamera();
        }

        [Sub]
        private void OnPlayerDeath(PlayerDeath reference)
        {
            _spawnReturningButton.gameObject.SetActive(true);
        }
    }
}
