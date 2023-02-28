using System;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.General.Saves;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Common.Sensitivity
{
    public class SensitivitySlider : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        
        private void Awake()
        {
            _slider.value = SaveManager.GetSensitivityMultiplier();
            _slider.onValueChanged.AddListener(OnSliderValueChange);
        }

        private void OnSliderValueChange(Single value)
        {
            float percentage = value;
            ServiceLocator.Current.Get<ISensitivityManager>().ChangeSensitivity(percentage);
        }
    }
}
