using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.GUi.MainMenu.Character
{
    public class CharacterPreviewButton : MonoBehaviour
    {
        [SerializeField, Range(0, 20)] private int _characterID;
        
        public int CharacterID => _characterID;
        
        private Button _button;

        public void Initialize(Action<int> onPreview)
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() => onPreview?.Invoke(_characterID));
        }

        public void ToggleInteraction(bool value)
        {
            _button.interactable = value;
        }
    }
}
