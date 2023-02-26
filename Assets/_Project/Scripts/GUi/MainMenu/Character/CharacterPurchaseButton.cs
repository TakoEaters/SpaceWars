using System;
using _Project.Scripts.Audio;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.General.Resources;
using _Project.Scripts.General.Saves;
using _Project.Scripts.GUi.MainMenu.NavigationSystem;
using _Project.Scripts.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.GUi.MainMenu.Character
{
    public class CharacterPurchaseButton : MonoBehaviour
    {
        [SerializeField] private Button _purchaseButton;
        [SerializeField] private TextMeshProUGUI _purchaseInscription;
        [SerializeField, Range(1000, 10000)] private int _price = 5000;
        [SerializeField] private CharacterPreviewButton _previewButton;

        private Action _onSuccess;
        private Action _onFailure;


        public void Initialize(Action onFailure, Action onSuccess)
        {
            _onSuccess = onSuccess;
            _onFailure = onFailure;
            
            if (PlayerSaves.IsSkinPurchased(_previewButton.CharacterID))
            {
                _purchaseButton.gameObject.SetActive(false);
            }

            else
            {
                _purchaseButton.onClick.AddListener(OnPurchase);
                _previewButton.ToggleInteraction(false);
                _purchaseInscription.text = _price.ToString();
            }
        }

        private void OnPurchase()
        {
            if (SaveManager.GetResourcesAmount(Resource.Bullets) < _price)
            {
                ServiceLocator.Current.Get<IFXEmitter>().PlayFailedPurchaseSound();
                _onFailure?.Invoke();
                return;
            }
            
            ServiceLocator.Current.Get<IFXEmitter>().PlaySuccessfulPurchaseSound();
            _purchaseButton.gameObject.SetActive(false);
            _previewButton.ToggleInteraction(true);
            PlayerSaves.PurchaseSkin(_previewButton.CharacterID);
            SaveManager.IncrementResourcesAmount(Resource.Bullets, -_price);
            _onSuccess?.Invoke();
        }
    }
}
