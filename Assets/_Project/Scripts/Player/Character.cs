using System;
using System.Collections;
using _Project.Scripts.General.DamageableCore;
using _Project.Scripts.General.Signals;
using _Project.Scripts.GUi.Interface;
using Template.Scripts.Pool;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public abstract class Character : CorePoolElement, IDamageable, IAmmo
    {
        [SerializeField] private DamageableLayer _layer;
        [SerializeField] protected CharacterConfigs _configs;

        public Team Team => _configs.Team;
        public DamageableLayer DamageableLayer => _layer;

        public string Nickname => String.Empty;
        public int Priority => _configs.Priority;
        public bool IsAlive => Health > 0;
        public bool IsInSafeZone { get; set; }
        public Vector3 Position => transform.position;
        
        public abstract int Count { get; protected set; }
        public abstract int Remaining { get; protected set; }
        public abstract Action<float> IsReloading { get; protected set; }

        protected IHealthViewer HealthViewer;
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
                    HealthViewer.UpdateView(0, Health);
                }

                yield return wait;
            }
        }

        public abstract void OnTakeDamage(int damage);
    }
    
    [Serializable] public class CharacterConfigs
    {
        public Team Team;
        [Range(1.0f, 5.0f)] public float Velocity = 3.5f;
        [Range(1.0f, 7.0f)] public float RunningVelocity = 4.5f;

        public int Health;
        public float HealthRestorationDelay;
        public int HealthRestorationAmount;
        
        public int Priority; 
    }
}
