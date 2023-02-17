using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.General.DamageableCore;
using _Project.Scripts.General.Signals;
using UnityEngine;

namespace _Project.Scripts.General.Utils
{
    public static class DamageableUtils
    {
        public static IDamageable Scan(this DamageableLayer layer, Vector3 position, float range, Team team)
        {
            LayerMask layerMask = LayerMask.GetMask($"Damageable");
            var colliders = Physics.OverlapSphere(position, range, layerMask);
            var damageables = new List<IDamageable>();
            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out IDamageable damageable) && damageable.IsAlive &&
                    !damageable.IsInSafeZone && layer.ContainsLayer(damageable.DamageableLayer) && damageable.Team != team)
                {
                    damageables.Add(damageable);
                }
            }
            
            return damageables.Any() ? damageables.OrderByDescending(x => x.Priority).First() : null;
        }
    }
}