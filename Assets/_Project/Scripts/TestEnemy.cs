using _Project.Scripts.General.DamageableCore;
using _Project.Scripts.General.Utils;
using UnityEngine;

namespace _Project.Scripts
{
    public class TestEnemy : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable damageable) && damageable.IsAlive)
            {
                damageable.OnTakeDamage(500);
            }
        }
    }
}
