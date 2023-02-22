using _Project.Scripts.General.Resources;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.GUi.MainMenu.Resources.Entities
{
    public class BulletsView : ResourceView
    {
        [SerializeField, Range(5.0f, 20.0f)] private float _lerpIntensity = 15.0f;
        [SerializeField] private TextMeshProUGUI _text;

        public override Resource ViewResource => Resource.Bullets;
        
        private int _currentAmount;
        private int _targetAmount;
        
        public override void Change(int value)
        {
            _targetAmount = value;
        }

        private void LateUpdate()
        {
            _currentAmount = (int)Mathf.Lerp(_currentAmount, _targetAmount, Time.deltaTime * _lerpIntensity);
            _text.text = _currentAmount.ToString();
        }
    }
}
