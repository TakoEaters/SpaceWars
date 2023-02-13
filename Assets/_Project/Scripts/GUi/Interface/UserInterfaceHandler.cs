using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.General.LevelHandlers;
using UnityEngine;

namespace _Project.Scripts.GUi.Interface
{
    public class UserInterfaceHandler : MonoBehaviour
    {
        [SerializeField] private HealthUI _healthUI;

        private void Awake()
        {
            _healthUI.Disable();
        }

        [Sub]
        private void OnStartLevel(StartLevel reference)
        {
            _healthUI.Enable();
        }

        [Sub]
        private void OnChangeHealthInterface(ChangeUIHealth reference)
        {
            if (reference.TotalHealth > 0) _healthUI.SetData(reference.TotalHealth, reference.TotalHealth);
            else _healthUI.ChangeUi(reference.CurrentHealth);
        }
    }
}
