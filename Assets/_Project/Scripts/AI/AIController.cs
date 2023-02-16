using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.General.Spawners;
using UnityEngine;

namespace _Project.Scripts.AI
{
    public class AIController : AIBehaviour
    {
        protected Animator Animator;

        
        private ISpawnerSystem _spawnerSystem;
        private bool _isDisabled = true;
        
        protected void FindServices()
        {
            _spawnerSystem = ServiceLocator.Current.Get<ISpawnerSystem>();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void EnableController()
        {
            _isDisabled = false;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void UpdateBotData()
        {
            transform.position = _spawnerSystem.GetRandomSpawner(_configs.Team).SpawnPosition;
            Animator.Rebind();
            InitializeHealth();
        }

        public void DisableController()
        {
            _isDisabled = false;
        }


        #region HEALTH/DAMAGE

        private Coroutine _healthRoutine;

        private void InitializeHealth()
        {
            Animator.enabled = true;
            Health = _configs.Health;
            _healthRoutine = StartCoroutine(RestoreHealth());
        }
        
        public override void OnTakeDamage(int damage)
        {
            Health -= damage;

            if (Health <= 0) OnDeath();
            else Animator.SetTrigger(Hit);
        }


        private void OnDeath()
        {
            if (_healthRoutine != null) StopCoroutine(_healthRoutine);
            Animator.enabled = false;
        }
        #endregion
        
        #region HASHES

        private static readonly int Shooting = Animator.StringToHash("shooting");
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Blend = Animator.StringToHash("Blend");
        private static readonly int Y = Animator.StringToHash("Y");
        private static readonly int X = Animator.StringToHash("X");

        #endregion
    }
}
