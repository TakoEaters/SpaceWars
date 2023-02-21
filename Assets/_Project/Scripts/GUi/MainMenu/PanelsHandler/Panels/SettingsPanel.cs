using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.GUi.MainMenu.NavigationSystem;
using Template.Scripts.General;
using UnityEngine;

namespace _Project.Scripts.GUi.MainMenu.PanelsHandler.Panels
{
    public class SettingsPanel : PanelHandler
    {
        [SerializeField] private View _panelView;
        
        public override NavigationTab Navigation => NavigationTab.Settings;
        
        [Sub]
        protected override void OnNavigate(Navigate reference)
        {
            if (reference.Destination != NavigationTab.Settings)
            {
                _panelView.Disable();
            }
        }
    }
}
