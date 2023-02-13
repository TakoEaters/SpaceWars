using _Project.Scripts.General;
using UnityEngine;

namespace _Project.Scripts.AI
{
    public abstract class AIBehaviour : MonoBehaviour, IDamageable
    {
        public abstract bool IsAlive { get; }
        public abstract void OnTakeDamage(int damage);
    }
}
