using _Project.Scripts.Audio;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.GUi.MainMenu.NavigationSystem;
using _Project.Scripts.GUi.MainMenu.Settings;
using DG.Tweening;
using Template.Scripts.General;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.GUi.MainMenu.PanelsHandler.Panels
{
    public class SettingsPanel : PanelHandler
    {
        [SerializeField] private Button _onDisableSettings;
        [SerializeField] private View _panelView;
        [SerializeField] private Image _background;
        [SerializeField, Range(0.1f, 1.0f)] private float _animationDuration = 0.35f;
        
        public override NavigationTab Navigation => NavigationTab.Settings;

        private SettingsHandler _handler;
        private const float MaxScale = 0.75f;
        private const float MaxFade = 0.85f;

        private void Awake()
        {
            _handler = GetComponent<SettingsHandler>();
            _onDisableSettings.onClick.AddListener(DisableSettings);
        }

        [Sub]
        protected override void OnNavigate(Navigate reference)
        {
            if (reference.Destination == NavigationTab.Settings) EnableSettings();
        }

        private void EnableSettings()
        {
            _background.gameObject.SetActive(true);
            _background.DOFade(MaxFade, _animationDuration);
            _panelView.Enable();
            _panelView.transform.localScale = Vector3.zero;
            _panelView.transform.DOScale(MaxScale, _animationDuration);
            _handler.Enable();
        }

        private void DisableSettings()
        {
            ServiceLocator.Current.Get<IFXEmitter>().PlayButtonSound();
            _background.DOFade(0f, _animationDuration).OnComplete(() => _background.gameObject.SetActive(false));
            _panelView.transform.DOScale(0f, _animationDuration).OnComplete(() => _panelView.Disable());
            _handler.Disable();
            Signal.Current.Fire<Navigate>(new Navigate {Destination = NavigationTab.Main});
        }
    }
}
