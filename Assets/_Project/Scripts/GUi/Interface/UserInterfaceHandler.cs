using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.General.Signals;
using UnityEngine;

namespace _Project.Scripts.GUi.Interface
{
    public class UserInterfaceHandler : MonoBehaviour, IHealthViewer
    {
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
        private void OnPlayerRevive(PlayerRevive reference)
        {
            _healthUI.Enable();
        }

        [Sub]
        private void OnPlayerDeath(PlayerDeath reference)
        {
            _healthUI.Disable();
        }
    }

    public interface IHealthViewer : IGameService
    {
        public void UpdateView(int totalHealth, int currentHealth);
    }
}
