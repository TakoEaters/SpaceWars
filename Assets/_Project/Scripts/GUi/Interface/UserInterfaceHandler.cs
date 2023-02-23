using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.General.Signals;
using Template.Scripts.General;
using UnityEngine;

namespace _Project.Scripts.GUi.Interface
{
    public class UserInterfaceHandler : MonoBehaviour, IHealthViewer
    {
        [SerializeField] private View _mainView;
        [SerializeField] private HealthUI _healthUI;
        
        public void Register()
        {
            ServiceLocator.Current.Register<IHealthViewer>(this);
           _healthUI.Disable();
        }
        
        public void UpdateView(int totalHealth, int currentHealth)
        {
            if (totalHealth > 0) _healthUI.SetData(totalHealth, totalHealth);
            else _healthUI.ChangeUi(currentHealth);
        }
        
        [Sub]
        private void OnToggleView(ToggleGameplayUI reference)
        {
            if (reference.Enable) _mainView.Enable();
            else _mainView.Disable();
        }

        [Sub]
        private void OnFinishLevel(FinishLevel reference)
        {
            _healthUI.Disable();
            _mainView.Disable();
        }
    }

    public interface IHealthViewer : IGameService
    {
        public void UpdateView(int totalHealth, int currentHealth);
    }
    
    public struct ToggleGameplayUI
    {
        public bool Enable;
    }
}
