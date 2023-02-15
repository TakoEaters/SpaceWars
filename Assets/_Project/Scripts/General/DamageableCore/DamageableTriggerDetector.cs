using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Scripts.General.DamageableCore
{
    public class DamageableTriggerDetector : MonoBehaviour
    {
        private DamageableLayer _damageableLayer;
        private List<IDamageable> _damageableInTrigger = new List<IDamageable>();
        public bool Any => _damageableInTrigger.Any();
        public List<IDamageable> DamageableInTrigger => _damageableInTrigger;
        public void Initialize(DamageableLayer layer)
        {
            _damageableLayer = layer;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable damageable) && damageable.IsAlive && !damageable.IsInSafeZone &&
                _damageableLayer.ContainsLayer(damageable.DamageableLayer) &&
                !_damageableInTrigger.Contains(damageable))
            {
                _damageableInTrigger.Add(damageable);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IDamageable damageable) && _damageableInTrigger.Contains(damageable))
            {
                _damageableInTrigger.Remove(damageable);
            }
        }
    }
}