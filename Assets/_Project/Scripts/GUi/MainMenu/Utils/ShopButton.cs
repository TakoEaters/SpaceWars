using System;
using _Project.Scripts.Audio;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.GUi.MainMenu.NavigationSystem;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.GUi.MainMenu.Utils
{
    public class ShopButton : MonoBehaviour
    {
        private Button _button;
        
        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnEnableShop);
        }

        private void OnEnableShop()
        {
            ServiceLocator.Current.Get<IFXEmitter>().PlayButtonSound();
            Signal.Current.Fire<Navigate>(new Navigate {Destination = NavigationTab.Shop});
        }
    }
}
