using _Project.Scripts.General.Saves;
using _Project.Scripts.Player.WeaponsSystem;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.GUi.MainMenu.Weapons
{
    public class WeaponPurchaseButton : MonoBehaviour
    {
        [SerializeField] private GameObject _purchaseInscription;
        [SerializeField] private GameObject _purchasedInscription;
        [SerializeField] private TextMeshProUGUI _price;

        public void ShowData(WeaponEntity entity)
        {
            if (PlayerSaves.IsWeaponLocked(entity.ID))
            {
                _purchaseInscription.SetActive(true);
                _purchasedInscription.SetActive(false);
                _price.text = entity.Price.ToString();
                return;
            }
            
            _purchaseInscription.SetActive(false);
            _purchasedInscription.SetActive(true);
        }
    }
}
