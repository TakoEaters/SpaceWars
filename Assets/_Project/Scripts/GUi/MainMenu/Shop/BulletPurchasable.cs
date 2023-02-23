using _Project.Scripts.Audio;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.General.Resources;
using _Project.Scripts.General.Saves;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.GUi.MainMenu.Shop
{
    public class BulletPurchasable : MonoBehaviour
    {
        [SerializeField, Range(500, 15000)] private int _additiveAmount = 900;
        [SerializeField, Range(1, 10)] private int _requirementIndex = 3;
        [SerializeField] private TextMeshProUGUI _adsRemaining;
        [SerializeField] private string _placement;
        
        private Button _button;
        private int _currentIndex;
        
        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnPurchase);
            _currentIndex = AdvertisementSaveSystem.GetCurrentTapIndex(_placement);
            _adsRemaining.text = "ADS REMAINING: " + (_requirementIndex - _currentIndex);
        }

        private void OnWatchAd()
        {
            //SHOW OFFER
        }

        private void OnPurchase()
        {
            _currentIndex++;
            if (_currentIndex >= _requirementIndex)
            {
                _currentIndex = 0;
                AdvertisementSaveSystem.SetCurrentTapIndex(_placement, 0);
                SaveManager.IncrementResourcesAmount(Resource.Bullets, _additiveAmount);
                ServiceLocator.Current.Get<IFXEmitter>().PlaySuccessfulPurchaseSound();
            }

            else
            {
                AdvertisementSaveSystem.SetCurrentTapIndex(_placement, _currentIndex);
                ServiceLocator.Current.Get<IFXEmitter>().PlayButtonSound();
            }


            _adsRemaining.text = "ADS REMAINING: " + (_requirementIndex - _currentIndex);
        }
    }
}
