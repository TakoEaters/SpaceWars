using _Project.Scripts.General;
using UnityEngine;

namespace _Project.Scripts.AI
{
    public class AIBehaviour : MonoBehaviour, IDamageable
    {
        public bool IsAlive { get; }

        public void OnTakeDamage(int damage)
        {
            
        }
    }
}
