using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Audio;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.General.Resources;
using _Project.Scripts.General.Saves;
using _Project.Scripts.GUi.MainMenu.NavigationSystem;
using _Project.Scripts.GUi.MainMenu.Weapons;
using _Project.Scripts.Player;
using Template.Scripts.General;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.GUi.MainMenu.PanelsHandler.Panels
{
    public class WeaponsPanel : PanelHandler
    {
        [SerializeField] private List<WeaponButton> _weaponButtons = new List<WeaponButton>();
        [SerializeField] private WeaponUpgradeButton _upgradeButton;
        [SerializeField] private Button _backButton;
        [SerializeField] private View _mainView;
        
        public override NavigationTab Navigation => NavigationTab.Weapons;

        private WeaponStatistics _statistics;
        private WeaponButton _currentWeapon;
        
        private void Start()
        {
            _statistics = GetComponent<WeaponStatistics>();
            _backButton.onClick.AddListener(OnExitPanel);
            _weaponButtons.ForEach(x => x.Initialize(OnSelectWeapon));
            _currentWeapon = _weaponButtons.Find(x => x.Entity.ID == PlayerSaves.GetPlayerWeapon());
            _upgradeButton.Initialize(OnUpgrade, _currentWeapon.Entity.Price, _currentWeapon.Entity.IsMaxLevel());
            _currentWeapon.Select();
        }

        [Sub]
        protected override void OnNavigate(Navigate reference)
        {
            if (reference.Destination == NavigationTab.Weapons)
            {
                _mainView.Enable();
            }
            
            else _mainView.Disable();
        }

        private void OnUpgrade()
        {
            int price = _currentWeapon.Entity.Price;
            if (price > SaveManager.GetResourcesAmount(Resource.Coins))
            {
                ServiceLocator.Current.Get<IFXEmitter>().PlayFailedPurchaseSound();
                Signal.Current.Fire<Navigate>(new Navigate { Destination = NavigationTab.Shop });
                return;
            }
            
            SaveManager.IncrementResourcesAmount(Resource.Coins, -price);
            ServiceLocator.Current.Get<IFXEmitter>().PlaySuccessfulPurchaseSound();
            _currentWeapon.Entity.Upgrade();
            _upgradeButton.ShowPrice(_currentWeapon.Entity);
            _statistics.Show(_currentWeapon.Entity);
        }

        private void OnSelectWeapon(WeaponButton entity)
        {
            _currentWeapon.Unselect();
            _currentWeapon = entity;
            _upgradeButton.ShowPrice(_currentWeapon.Entity);
            _statistics.Show(_currentWeapon.Entity);
            PlayerSaves.SetPlayerWeapon(_currentWeapon.Entity.ID);
            ServiceLocator.Current.Get<ICharacterViewer>().UpdateWeapon();
        }
        
        private void OnExitPanel()
        {
            Signal.Current.Fire<Navigate>(new Navigate {Destination = NavigationTab.Main});
            ServiceLocator.Current.Get<IFXEmitter>().PlayButtonSound();
            _mainView.Disable();
        }
    }
}
