using System;
using _Project.Scripts.Audio;
using _Project.Scripts.Core.LocatorServices;
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
            ServiceLocator.Current.Get<IFXEmitter>().PlayButtonSound();
            _onCall?.Invoke(_tab);
        }
    }
}