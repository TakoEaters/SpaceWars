using System;
using _Project.Scripts.General.Saves;
using _Project.Scripts.Player.WeaponsSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.GUi.MainMenu.Weapons
{
    public class WeaponPurchaseButton : MonoBehaviour
    {
        [SerializeField] private GameObject _purchaseInscription;
        [SerializeField] private GameObject _purchasedInscription;
        [SerializeField] private TextMeshProUGUI _price;
        [SerializeField] private Button _purchaseButton;

        private Action _onClick;
        
        public void Initialize(Action onPurchase)
        {
            _onClick = onPurchase;
            _purchaseButton.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            _onClick?.Invoke();
        }


        public void ShowData(WeaponEntity entity)
        {
            if (PlayerSaves.IsWeaponLocked(entity.ID))
            {
                _purchaseInscription.SetActive(true);
                _purchasedInscription.SetActive(false);
                _price.text = entity.WeaponPrice.ToString();
                return;
            }
            
            _purchaseInscription.SetActive(false);
            _purchasedInscription.SetActive(true);
        }
    }
}
