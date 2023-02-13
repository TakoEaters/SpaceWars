using _Project.Scripts.ColorIt;
using Template.Scripts.General;
using UnityEngine;

namespace _Project.Scripts.Player.WeaponsSystem
{
    public class WeaponView : View
    {
        [SerializeField] private Transform _nozzle;
        [SerializeField] private ParticleSystem _inkParticle;

        public Transform Nozzle => _nozzle;
        public Transform ParticleTransform => _inkParticle.transform;

        private ParticlesController _particlesController;

        public void InitializeData(int damage)
        {
            _particlesController = GetComponentInChildren<ParticlesController>();
            _particlesController.Initialize(damage);
        }

        public void ShootParticle()
        {
            _inkParticle.Play();
        }

        public void EmitParticle(int count)
        {
            _inkParticle.Emit(count);
        }

        public void StopParticle()
        {
            _inkParticle.Stop();
        }
    }
}
