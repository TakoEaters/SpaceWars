using System;
using _Project.Scripts.General.Saves;
using _Project.Scripts.Player.WeaponsSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.GUi.MainMenu.Weapons
{
    public class WeaponUpgradeButton : MonoBehaviour
    {
        [SerializeField] private GameObject _lockedView;
        [SerializeField] private TextMeshProUGUI _text;

        private Button _button;
        private Action _onClick;
        
        public void Initialize(Action onClick, int amount, bool value)
        {
            gameObject.SetActive(!value);
            _onClick = onClick;
            _text.text = amount.ToString();
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            _onClick?.Invoke();
        }
        
        public void ShowPrice(WeaponEntity entity)
        {
            bool isLocked = PlayerSaves.IsWeaponLocked(entity.ID);
            _lockedView.SetActive(isLocked);
            _button.interactable = !isLocked;
            gameObject.SetActive(!entity.IsMaxLevel());
            _text.text = entity.Price.ToString();
        }
    }
}
