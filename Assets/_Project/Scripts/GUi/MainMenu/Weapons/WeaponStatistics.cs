using System.Collections.Generic;
using _Project.Scripts.Player.WeaponsSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.GUi.MainMenu.Weapons
{
    public class WeaponStatistics : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _damageText;
        [SerializeField] private TextMeshProUGUI _fireRateText;
        [SerializeField] private TextMeshProUGUI _overheatingText;
        [SerializeField] private TextMeshProUGUI _weaponLevel;
        [SerializeField] private Slider _upgradeSlider;
        [SerializeField] private List<float> _values = new List<float>();
        

        public void Show(WeaponEntity entity)
        {
            _damageText.text = entity.Damage.ToString();
            _fireRateText.text = entity.FireRate + "s";
            _overheatingText.text = entity.OverheatAdditive + "s";
            int level = Mathf.Clamp(entity.Level - 1, 0, _values.Count - 1);
            _upgradeSlider.value = _values[level];
            _weaponLevel.text = "LV <size=50>" + entity.Level;
        }
    }
}
