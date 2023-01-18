using System;
using _Project.Scripts.Core.SignalBus;
using Template.Scripts.General;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.GUi.Interface
{
    public class OverheatingView : MonoBehaviour
    {
        [SerializeField] private Image _fillBorder;
        [SerializeField] private View _view;
        [SerializeField, Range(0.5f, 3.0f)] private float _activeTime = 3f;
        [SerializeField, Range(1.0f, 15.0f)] private float _lerpSpeed = 5f;
        [SerializeField] private Color _overheatedColor;
        [SerializeField] private Color _initialColor;

        private float _targetFill;
        private float _remainingActiveTime;
        private bool _isActive;
        
        
        private void LateUpdate()
        {
            float lerpSpeedClamped = _lerpSpeed * Time.deltaTime;
            _fillBorder.fillAmount = Mathf.Lerp(_fillBorder.fillAmount, _targetFill, lerpSpeedClamped);
            _fillBorder.color = Color.Lerp(_initialColor, _overheatedColor, _targetFill);
            
            if (Math.Abs(_fillBorder.fillAmount - _targetFill) < .0001f)
            {
                _isActive = false;
                _view.Disable();
            }
            
            else _view.Enable();
        }

        [Sub]
        private void OnModifyOverheatingView(Modifier reference)
        {
            _remainingActiveTime = _activeTime;
            _isActive = true;
          //  _view.Enable();
            _targetFill = reference.Percentage;
        }
    }

    public struct Modifier
    {
        public float Percentage;
    }
}
