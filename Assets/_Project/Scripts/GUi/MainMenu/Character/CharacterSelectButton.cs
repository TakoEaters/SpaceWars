using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.GUi.MainMenu.Character
{
    public class CharacterSelectButton : MonoBehaviour
    {
        private Button _button;
        private Action _onInteract;

        public void Initialize(Action onSelectCharacter)
        {
            _onInteract = onSelectCharacter;
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnSelect);
        }

        private void OnSelect()
        {
            _onInteract?.Invoke();
        }
    }
}
