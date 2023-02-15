using UnityEngine;

namespace _Project.Scripts.General.DamageableCore
{
    public interface IDamageable
    {
        public DamageableLayer DamageableLayer { get; }
        public Vector3 Position { get; }
        public int Priority { get; }
        public bool IsInSafeZone { get; set; }
        public bool IsAlive { get; }
        public void OnTakeDamage(int damage);
    }
}
