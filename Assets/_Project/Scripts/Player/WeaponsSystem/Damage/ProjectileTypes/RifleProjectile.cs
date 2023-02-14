using System;
using System.Collections;
using _Project.Scripts.Core.Pool;
using _Project.Scripts.General;
using UnityEngine;

namespace _Project.Scripts.Player.WeaponsSystem.Damage.ProjectileTypes
{
    public class RifleProjectile : Projectile
    {
        protected override IEnumerator FireRoutine(IDamageable target, float damage)
        {
            yield return ApplyDamage(target, damage);
            CorePool.Current.Return(this);
        }
        
        private IEnumerator ApplyDamage(IDamageable damageable, float damage)
        {
            // _hitSource.Play();
            // damageable.OnTakeDamage(damage);
            // _onApplyDamage?.Invoke();
            // DamageEffects.ForEach(x => x.ApplyEffect(transform.position, damage));
            yield return null;
        }

        private void OnCollisionEnter(Collision collision)
        {
            
        }
    }
}
