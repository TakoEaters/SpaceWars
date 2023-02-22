using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.GUi.MainMenu.NavigationSystem;
using Template.Scripts.General;
using UnityEngine;

namespace _Project.Scripts.GUi.MainMenu.PanelsHandler.Panels
{
    public class WeaponsPanel : PanelHandler
    {
        [SerializeField] private View _mainView;
        
        public override NavigationTab Navigation => NavigationTab.Weapons;
        
        [Sub]
        protected override void OnNavigate(Navigate reference)
        {
            if (reference.Destination == NavigationTab.Weapons)
            {
                _mainView.Enable();
            }
        }
    }
}
