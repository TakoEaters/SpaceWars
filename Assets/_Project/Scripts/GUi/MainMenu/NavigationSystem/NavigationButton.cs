using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.GUi.MainMenu.NavigationSystem
{
    public class NavigationButton : MonoBehaviour
    {
        [SerializeField] private NavigationTab _tab;

        private Action<NavigationTab> _onCall;
        
        public void Initialize(Action<NavigationTab> onCallButton)
        {
            Button targetButton = GetComponent<Button>();
            _onCall = onCallButton;
            targetButton.onClick.AddListener(OnClickOnButton);
        }

        private void OnClickOnButton()
        {
            _onCall?.Invoke(_tab);
        }
    }
}