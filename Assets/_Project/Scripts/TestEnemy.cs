using _Project.Scripts.General;
using UnityEngine;

namespace _Project.Scripts
{
    public class TestEnemy : MonoBehaviour, IDamageable
    {
        [SerializeField] private int _totalHealth;

        private int _currentHealth;

        private void Awake()
        {
            _currentHealth = _totalHealth;
        }

        public bool IsAlive => _currentHealth > 0;
        
        
        public void OnTakeDamage(int damage)
        {
            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                Debug.Log("Is dead");
            }
        }
    }
}
