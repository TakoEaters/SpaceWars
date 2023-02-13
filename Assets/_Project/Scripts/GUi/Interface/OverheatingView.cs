using System;
using _Project.Scripts.Core.SignalBus;
using Template.Scripts.General;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.GUi.Interface
{
    public class OverheatingView : MonoBehaviour
    {
        [SerializeField] private View _view;
        [SerializeField] private Slider _slide; 
        [SerializeField] private Image _slideSprite;
        [SerializeField, Range(1.0f, 15.0f)] private float _lerpSpeed = 5f;
        [SerializeField] private Color _overheatedColor;
        [SerializeField] private Color _initialColor;

        private float _targetFill;
        
        private void LateUpdate()
        {
            float lerpSpeedClamped = _lerpSpeed * Time.deltaTime;
            _slide.value = Mathf.Lerp(_slide.value, _targetFill, lerpSpeedClamped);
            _slideSprite.color = Color.Lerp(_initialColor, _overheatedColor, _targetFill);

            if (Math.Abs(_slide.value - _targetFill) < .0001f) _view.Disable();
            else _view.Enable();
        }

        [Sub]
        private void OnModifyOverheatingView(Modifier reference)
        {
            _targetFill = reference.Percentage;
        }
    }

    public struct Modifier
    {
        public float Percentage;
    }
}
