using _Project.Scripts.Common;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.GUi.MainMenu.NavigationSystem;

namespace _Project.Scripts.GUi.MainMenu.PanelsHandler.Panels
{
    public class MatchPanel : PanelHandler
    {
        public override NavigationTab Navigation => NavigationTab.Start;
        
        [Sub]
        protected override void OnNavigate(Navigate reference)
        {
            if (reference.Destination == NavigationTab.Start)
            {
                LevelsManager.LoadGame();
            }
        }
    }
}
