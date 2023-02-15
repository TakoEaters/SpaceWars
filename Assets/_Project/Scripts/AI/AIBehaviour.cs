using _Project.Scripts.General;
using _Project.Scripts.General.DamageableCore;
using UnityEngine;

namespace _Project.Scripts.AI
{
    public class AIBehaviour : MonoBehaviour, IDamageable
    {
        public DamageableLayer DamageableLayer { get; }
        public Vector3 Position { get; }
        public int Priority { get; }
        public bool IsInSafeZone { get; set; }
        public bool IsAlive { get; }

        public void OnTakeDamage(int damage)
        {
            
        }
    }
}
