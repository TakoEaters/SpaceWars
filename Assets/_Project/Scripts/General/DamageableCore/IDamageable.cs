using _Project.Scripts.General.Signals;
using UnityEngine;

namespace _Project.Scripts.General.DamageableCore
{
    public interface IDamageable
    {
        public Team Team { get; }
        public DamageableLayer DamageableLayer { get; }
        public Vector3 Position { get; }
        public string Nickname { get; }
        public int Priority { get; }
        public bool IsInSafeZone { get; set; }
        public bool IsAlive { get; }
        public void OnTakeDamage(int damage);
    }
}
