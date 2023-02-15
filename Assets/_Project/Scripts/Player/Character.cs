using System;
using System.Collections;
using _Project.Scripts.General.DamageableCore;
using Template.Scripts.Pool;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public abstract class Character : CorePoolElement, IDamageable
    {
        [SerializeField] private DamageableLayer _layer;
        [SerializeField] protected CharacterConfigs _configs;
        
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
    
    [Serializable] public class CharacterConfigs 
    {
        public float MaxSpeed;
        public float RotationSpeed;

        public int Health;
        public float HealthRestorationDelay;
        public int HealthRestorationAmount;
        
        public int Priority; 
    }
}
