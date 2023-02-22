using System;
using _Project.Scripts.Audio;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.General.Resources;
using _Project.Scripts.General.Saves;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.GUi.MainMenu.Shop
{
    public class CoinPurchasable : MonoBehaviour
    {
        [SerializeField, Range(500, 15000)] private int _additiveAmount = 1000;
        [SerializeField, Range(50, 1000)] private int _removableAmount = 100;
        
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnPurchase);
        }

        private void OnPurchase()
        {
            int bulletsAmount = SaveManager.GetResourcesAmount(Resource.Bullets);
            if (bulletsAmount >= _removableAmount)
            {
                SaveManager.IncrementResourcesAmount(Resource.Coins, _additiveAmount);
                SaveManager.IncrementResourcesAmount(Resource.Bullets, -_removableAmount);
                ServiceLocator.Current.Get<IFXEmitter>().PlaySuccessfulPurchaseSound();
                return;
            }
            
            ServiceLocator.Current.Get<IFXEmitter>().PlayFailedPurchaseSound();
        }
    }
}
