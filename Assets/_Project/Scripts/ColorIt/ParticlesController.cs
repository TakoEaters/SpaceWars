using System;
using System.Collections.Generic;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.Core.Pool;
using _Project.Scripts.General;
using _Project.Scripts.General.DamageableCore;
using _Project.Scripts.General.Utils.Audio;
using _Project.Scripts.Player.WeaponsSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.ColorIt
{
    public class ParticlesController : MonoBehaviour
    {
        [SerializeField] private int _threshold = 6;
        [SerializeField] private CorePoolAudio _clip;
        public Color paintColor;

        public float minRadius = 0.05f;
        public float maxRadius = 0.2f;
        public float strength = 1;
        public float hardness = 1;
        [Space] ParticleSystem part;
        List<ParticleCollisionEvent> collisionEvents;


        private int _currentTime = 0;
        private int _damage;


        public void Initialize(int damage)
        {
            _damage = damage;
            _currentTime = _threshold;
            part = GetComponent<ParticleSystem>();
            collisionEvents = new List<ParticleCollisionEvent>();
            paintColor = ServiceLocator.Current.Get<IPaintColor>().CurrentColor;
        }

        private void OnParticleCollision(GameObject other)
        {
            Vector3 audioPos = Vector3.zero;
            int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
            Paintable p = other.GetComponent<Paintable>();
            OnShootEnemy(other);
            if (p != null)
            {
                for (int i = 0; i < numCollisionEvents; i++)
                {
                    Vector3 pos = collisionEvents[i].intersection;
                    float radius = Random.Range(minRadius, maxRadius);
                    audioPos = pos;
                    PaintManager.instance.paint(p, pos, radius, hardness, strength, paintColor);
                }

                _currentTime++;
                if (_currentTime >= _threshold)
                {
                    _currentTime = 0;
                    var clip = CorePool.Current.Get(_clip);
                    clip.Play(audioPos);
                }
            }
        }

        private void OnShootEnemy(GameObject other)
        {
            if (other.TryGetComponent(out IDamageable damageable) && damageable.IsAlive)
            {
                damageable.OnTakeDamage(_damage);
            }
        }
    }
}