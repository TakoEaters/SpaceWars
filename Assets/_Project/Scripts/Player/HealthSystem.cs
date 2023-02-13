using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.General;
using _Project.Scripts.GUi.Interface;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class HealthSystem : MonoBehaviour, IDamageable
    {
        [SerializeField, Range(10, 250)] private int _playerTotalHealth = 250;

        private int _currentHealth;

        public bool IsAlive => _currentHealth > 0;

        private void Awake()
        {
            _currentHealth = _playerTotalHealth;
        }

        private void Start()
        {
            Signal.Current.Fire<ChangeUIHealth>(new ChangeUIHealth {TotalHealth = _currentHealth});
        }

        [Button()]
        public void OnTakeDamage(int damage)
        {
            _currentHealth -= damage;
            Signal.Current.Fire<ChangeUIHealth>(new ChangeUIHealth {CurrentHealth = _currentHealth});

            if (_currentHealth <= 0)
            {
                Debug.Log("Player is dead");
            }
        }
    }
}
