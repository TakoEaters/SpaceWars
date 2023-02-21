using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.GUi.MainMenu.Settings
{
    [RequireComponent(typeof(Button))]
    public class GraphicsButton : MonoBehaviour
    {
        [SerializeField] private Image _isOn;
        [SerializeField, Range(0.01f, 0.1f)] private float _fadeDuration = 0.2f;

        private Action<int> _onClick;
        private Button _button;


        public void Initialize(Action<int> onEnable, int index)
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() => onEnable?.Invoke(index));
        }
        
        public void Enable()
        {
            _isOn.DOFade(1f, _fadeDuration);
        }

        public void Disable()
        {
            _isOn.DOFade(0f, _fadeDuration);
        }
    }
}