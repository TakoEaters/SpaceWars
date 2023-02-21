using _Project.Scripts.GUi.MainMenu.NavigationSystem;
using UnityEngine;

namespace _Project.Scripts.GUi.MainMenu.PanelsHandler
{
    public abstract class PanelHandler : MonoBehaviour
    {
        public abstract NavigationTab Navigation { get; }
        
        protected abstract void OnNavigate(Navigate reference);
    }
}