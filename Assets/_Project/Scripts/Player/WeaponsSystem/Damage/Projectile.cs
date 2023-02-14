using System.Collections;
using Template.Scripts.Pool;
using UnityEngine;

namespace _Project.Scripts.Player.WeaponsSystem.Damage
{
    public abstract class Projectile : CorePoolElement
    {
        public abstract void Initialize(Vector3 spawnPosition, Quaternion spawnRotation);
        protected abstract IEnumerator FireRoutine(int damage);

        public void Fire(int damage)
        {
            gameObject.SetActive(true);
            StartCoroutine(FireRoutine(damage));
        }
    }
}
