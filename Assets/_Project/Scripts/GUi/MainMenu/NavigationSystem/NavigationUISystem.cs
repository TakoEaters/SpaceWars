using System.Collections.Generic;
using _Project.Scripts.Core.SignalBus;
using Template.Scripts.General;
using UnityEngine;

namespace _Project.Scripts.GUi.MainMenu.NavigationSystem
{
    public class NavigationUISystem : MonoBehaviour
    {
        [SerializeField] private List<NavigationButton> _buttons = new List<NavigationButton>();
        [SerializeField] private View _navigationView;
    //    [SerializeField] private View _backNavigationView;

        private void Start()
        {
            _navigationView.Enable();
            _buttons.ForEach(x => x.Initialize(OnFireNavigationData));
      //      _backNavigationView.Disable();
        }

        [Sub]
        private void OnNavigateBack(Navigate reference)
        {
            if (reference.Destination != NavigationTab.Main)
            {
                _navigationView.Disable();
                return;
            }
            
            _navigationView.Enable();
      //      _backNavigationView.Disable();
        }

        // [Sub]
        // private void OnDisableBackButton(OnDisableBackButton reference)
        // {
        //     _backNavigationView.Disable();
        // }
        
        private void OnFireNavigationData(NavigationTab reference)
        {
      //      if (reference != NavigationTab.Race) _backNavigationView.Enable();
            Signal.Current.Fire<Navigate>(new Navigate {Destination = reference});
        }
    }
    
    public enum NavigationTab { Main, Start, Weapons, Character, Shop, Settings }

    public struct Navigate
    {
        public NavigationTab Destination;
    }
}