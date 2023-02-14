using System.Collections;
using _Project.Scripts.General;
using Template.Scripts.Pool;

namespace _Project.Scripts.Player.WeaponsSystem.Damage
{
    public abstract class Projectile : CorePoolElement
    {
        protected abstract IEnumerator FireRoutine(IDamageable target, float damage);

        public void Fire(IDamageable target, float damage)
        {
            gameObject.SetActive(true);
            StartCoroutine(FireRoutine(target, damage));
        }
    }
}
