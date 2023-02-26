using System.Collections.Generic;
using _Project.Scripts.Audio;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.General.Saves;
using _Project.Scripts.GUi.MainMenu.Character;
using _Project.Scripts.GUi.MainMenu.NavigationSystem;
using _Project.Scripts.Player;
using Template.Scripts.General;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.GUi.MainMenu.PanelsHandler.Panels
{
    public class CharacterPanel : PanelHandler
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private View _mainView;
        [SerializeField] private List<CharacterPreviewButton> _previews = new List<CharacterPreviewButton>();
        [SerializeField] private List<CharacterPurchaseButton> _purchaseButtons = new List<CharacterPurchaseButton>();
        [SerializeField] private CharacterSelectButton _characterSelect;
        
        public override NavigationTab Navigation => NavigationTab.Character;


        private int _currentPreviewedCharacter;
        private int _currentSelectedCharacter;

        private void Awake()
        {
            _backButton.onClick.AddListener(OnExitPanel);
            _characterSelect.Initialize(OnSelectCharacter);
            _previews.ForEach(x => x.Initialize(OnPreviewCharacter));
            _purchaseButtons.ForEach(x => x.Initialize(OnFailure, OnPurchase));
            _currentSelectedCharacter = PlayerSaves.GetSkinID();
            _characterSelect.gameObject.SetActive(false);
        }

        [Sub]
        protected override void OnNavigate(Navigate reference)
        {
            if (reference.Destination == NavigationTab.Character)
            {
                _mainView.Enable();
            }
        }

        private void OnPreviewCharacter(int characterID)
        {
            _characterSelect.gameObject.SetActive(_currentSelectedCharacter != characterID);
            ServiceLocator.Current.Get<IFXEmitter>().PlaySwitchSound();
            _currentPreviewedCharacter = characterID;
        }

        private void OnSelectCharacter()
        {
            _currentSelectedCharacter = _currentPreviewedCharacter;
            ServiceLocator.Current.Get<IFXEmitter>().PlayButtonSound();
            PlayerSaves.SetSkinID(_currentPreviewedCharacter);
            ServiceLocator.Current.Get<ICharacterViewer>().UpdateSkin();
            _characterSelect.gameObject.SetActive(false);
        }

        private void OnPurchase()
        {
            ServiceLocator.Current.Get<ICharacterViewer>().UpdateSkin();
            _currentSelectedCharacter = PlayerSaves.GetSkinID();
            _characterSelect.gameObject.SetActive(false);
        }

        private void OnFailure()
        {
            Signal.Current.Fire<Navigate>(new Navigate {Destination = NavigationTab.Shop});
        }
        
        private void OnExitPanel()
        {
            Signal.Current.Fire<Navigate>(new Navigate {Destination = NavigationTab.Main});
            ServiceLocator.Current.Get<IFXEmitter>().PlayButtonSound();
            _mainView.Disable();
        }
    }
}
