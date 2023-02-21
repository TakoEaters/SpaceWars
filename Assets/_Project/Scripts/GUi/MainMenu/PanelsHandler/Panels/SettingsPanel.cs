using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.GUi.MainMenu.NavigationSystem;
using DG.Tweening;
using Template.Scripts.General;
using UnityEngine;

namespace _Project.Scripts.GUi.MainMenu.PanelsHandler.Panels
{
    public class SettingsPanel : PanelHandler
    {
        [SerializeField] private View _panelView;
        [SerializeField, Range(0.1f, 1.0f)] private float _animationDuration = 0.35f;
        
        public override NavigationTab Navigation => NavigationTab.Settings;

        private const float MaxScale = 1f;
        
        [Sub]
        protected override void OnNavigate(Navigate reference)
        {
            if (reference.Destination != NavigationTab.Settings)
            {
                _panelView.Disable();
                return;
            }
            
            _panelView.Enable();
            _panelView.transform.localScale = Vector3.zero;
            _panelView.transform.DOScale(MaxScale, _animationDuration);
        }
    }
}
