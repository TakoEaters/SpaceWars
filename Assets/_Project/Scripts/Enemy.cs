using _Project.Scripts.General.DamageableCore;
using _Project.Scripts.General.Utils;
using UnityEngine;

namespace _Project.Scripts
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        private int _health = 100;
        public DamageableLayer DamageableLayer { get; }
        public Vector3 Position { get; }
        public int Priority { get; }
        public bool IsInSafeZone { get; set; }
        public bool IsAlive { get; }
        public void OnTakeDamage(int damage)
        {
            _health -= damage;

            if (_health <= 0)
            {
                GetComponent<Animator>().enabled = false;
                StartCoroutine(WaitUtils.WaitWithDelay(() => gameObject.SetActive(false), 2f));
            }
            
           else  GetComponent<Animator>().SetTrigger("Hit");
        }
    }
}
