using System;
using _Project.Scripts.Audio;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.GUi.MainMenu.NavigationSystem;
using Template.Scripts.General;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.GUi.MainMenu.PanelsHandler.Panels
{
    public class CharacterPanel : PanelHandler
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private View _mainView;
        
        public override NavigationTab Navigation => NavigationTab.Character;

        private void Awake()
        {
            _backButton.onClick.AddListener(OnExitPanel);
        }

        [Sub]
        protected override void OnNavigate(Navigate reference)
        {
            if (reference.Destination == NavigationTab.Character)
            {
                _mainView.Enable();
            }
        }
        
        private void OnExitPanel()
        {
            Signal.Current.Fire<Navigate>(new Navigate {Destination = NavigationTab.Main});
            ServiceLocator.Current.Get<IFXEmitter>().PlayButtonSound();
            _mainView.Disable();
        }
    }
}
