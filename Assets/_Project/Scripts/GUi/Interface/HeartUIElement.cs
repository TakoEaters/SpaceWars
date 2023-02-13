using Template.Scripts.General;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.GUi.Interface
{
    public class HeartUIElement : View
    {
        [SerializeField] private Image _image;
        
        private Vector2 _interval;
        private readonly float _lerpDuration = 10.0f;
        private float _endValue;

        public void Initialize(Vector2 interval)
        {
            _interval = interval;
        }
        
        public void Fill(float percentage)
        {
            if (_interval.x > percentage)
            {
                _endValue = 0f;
                return;
            }

            if (_interval.y < percentage)
            {
                _endValue = 1f;
                return;
            }
            
            _endValue = Mathf.InverseLerp(_interval.x, _interval.y, percentage);
        }
        
        public void FillInactive(float percentage)
        { 
            _image.fillAmount = Mathf.InverseLerp(_interval.x, _interval.y, percentage);
            _endValue = _image.fillAmount;
        }


        private void LateUpdate()
        {
            float fillValue = Mathf.Lerp(_image.fillAmount, _endValue, Time.deltaTime * _lerpDuration);
            _image.fillAmount = fillValue;
        }
    }
}
