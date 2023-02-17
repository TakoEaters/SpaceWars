using System;
using System.Collections;
using _Project.Scripts.General.DamageableCore;
using _Project.Scripts.General.Signals;
using Template.Scripts.Pool;
using UnityEngine;

namespace _Project.Scripts.AI
{
    public abstract class AIBehaviour : CorePoolElement, IDamageable
    {
        [SerializeField] private DamageableLayer _layer;
        [SerializeField] protected AIConfigs _configs;

        public Team Team => _configs.Team;
        public DamageableLayer DamageableLayer => _layer;
        public int Priority => _configs.Priority;
        public bool IsAlive => Health > 0;
        public bool IsInSafeZone { get; set; }
        public Vector3 Position => transform.position;
        
        protected int Health;
        
        protected IEnumerator RestoreHealth()
        {
            WaitForSeconds wait = new WaitForSeconds(_configs.HealthRestorationDelay);
            while (IsAlive)
            {
                if (Health < _configs.Health)
                {
                    Health += _configs.HealthRestorationAmount;
                    Health = Mathf.Clamp(Health, 0, _configs.Health);
                }

                yield return wait;
            }
        }
        
        public abstract void OnTakeDamage(int damage);
    }
    
    [Serializable] public class AIConfigs
    {
        public Team Team;
        [Range(1.0f, 5.0f)] public float Velocity = 3.5f;
        public float RotationSpeed;

        public int Health;
        public float HealthRestorationDelay;
        public int HealthRestorationAmount;
        
        public int Priority; 
    }
}
