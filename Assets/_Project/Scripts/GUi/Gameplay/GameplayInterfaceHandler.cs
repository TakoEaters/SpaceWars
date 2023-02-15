using _Project.Scripts.Common;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.General.Signals;
using _Project.Scripts.General.Utils;
using _Project.Scripts.Player;
using DG.Tweening;
using Template.Scripts.General;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.GUi.Gameplay
{
    public class GameplayInterfaceHandler : MonoBehaviour
    {
        [SerializeField, Range(0.5f, 2.0f)] private float _enableDelay = 1.0f;
        [SerializeField, Range(0.05f, 1.0f)] private float _duration = 0.35f;
         
        
        [SerializeField] private View _mainView;
        [SerializeField] private Image _background;
        [SerializeField] private Button _deployButton;
        [SerializeField] private Button _spawnReturningButton;
        [SerializeField] private TextMeshProUGUI _fontValue;

        private const float AlphaValue = 1f;
        private const float TransparentValue = 0f;
        

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
            _background.DOFade(TransparentValue, _duration);
        }

        private void OnReturnToSpawn()
        {
            EnableGameplayUI();
            ServiceLocator.Current.Get<IPlayerSystem>().UpdatePlayer();
            ServiceLocator.Current.Get<ICameraManager>().EnableGameplayCamera();
        }

        private void EnableGameplayUI()
        {
            _spawnReturningButton.gameObject.SetActive(false);
            _spawnReturningButton.interactable = false;
            Color buttonColor = _deployButton.image.color;
            buttonColor.a = TransparentValue;
            Color textColor = _fontValue.color;
            textColor.a = TransparentValue;
            _fontValue.color = textColor;
            _deployButton.image.color = buttonColor;
            _deployButton.gameObject.SetActive(true);
            _deployButton.image.DOFade(AlphaValue, _duration);
            _fontValue.DOFade(AlphaValue, _duration);
            _background.DOFade(AlphaValue, _duration);
            StartCoroutine(WaitUtils.WaitWithDelay(() => _spawnReturningButton.interactable = true, _enableDelay));
        }

        [Sub]
        private void OnPlayerDeath(PlayerDeath reference)
        {
            _spawnReturningButton.gameObject.SetActive(true);
        }
    }
}
