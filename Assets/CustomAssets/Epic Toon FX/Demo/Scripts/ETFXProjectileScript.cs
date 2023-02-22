using System;
using _Project.Scripts.General.DamageableCore;
using _Project.Scripts.General.Signals;
using _Project.Scripts.Player.WeaponsSystem;
using UnityEngine;

namespace CustomAssets.Epic_Toon_FX.Demo.Scripts
{
    public class ETFXProjectileScript : MonoBehaviour
    {
        [SerializeField] private LayerMask _damageableLayer;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private SphereCollider _sphereCollider;
        [SerializeField] private Projectile _projectile;

        [Header("Adjust if not using Sphere Collider")]
        public float colliderRadius = 1f;
        [Range(0f, 1f)] // This is an offset that moves the impact effect slightly away from the point of impact to reduce clipping of the impact effect
        public float collideOffset = 0.15f;

        private Action<string, Team> _onKillTarget;
        private Action _onTakeDamageTarget;
        private GameObject _muzzleParticle;
        private GameObject _trailParticle;
        private GameObject _impactParticle;
        
        public Rigidbody Rigidbody => _rigidbody;
        
        private void Start()
        {
            Destroy(gameObject, 3.0f);
            _trailParticle = Instantiate(_projectile.CurrentFX.TrailParticle, transform.position, transform.rotation).gameObject;
            _trailParticle.transform.parent = transform;
            _muzzleParticle= Instantiate(_projectile.CurrentFX.MuzzleParticle, transform.position, transform.rotation).gameObject; 
            Destroy(_muzzleParticle.gameObject, 0.5f); // 2nd parameter is lifetime of effect in seconds
            Destroy(_trailParticle.gameObject, 3.0f);
        }

        public void OnKillTarget(Action<string, Team> onKill, Action onTakeDamage)
        {
            _onKillTarget = onKill;
            _onTakeDamageTarget = onTakeDamage;
        }
		
        void FixedUpdate()
        {
            if (_rigidbody.velocity.magnitude != 0)
			{
			    transform.rotation = Quaternion.LookRotation(_rigidbody.velocity); // Sets rotation to look at direction of movement
			}
			
            RaycastHit hit;
			
            float radius; // Sets the radius of the collision detection
            if (_sphereCollider) radius = _sphereCollider.radius;
            else radius = colliderRadius;

            Vector3 direction = _rigidbody.velocity; // Gets the direction of the projectile, used for collision detection
            if (_rigidbody.useGravity) direction += Physics.gravity * Time.deltaTime; // Accounts for gravity if enabled
            direction = direction.normalized;

            float detectionDistance = _rigidbody.velocity.magnitude * Time.deltaTime; // Distance of collision detection for this frame

            if (Physics.SphereCast(transform.position, radius, direction, out hit, detectionDistance, _damageableLayer)) // Checks if collision will happen
            {
                transform.position = hit.point + (hit.normal * collideOffset); // Move projectile to point of collision

                _impactParticle = Instantiate(_projectile.CurrentFX.HitParticle.gameObject, transform.position, Quaternion.FromToRotation(Vector3.up, hit.normal)); // Spawns impact effect
                _projectile.DetectTarget(hit.collider.gameObject, _onKillTarget, _onTakeDamageTarget);

                Destroy(_trailParticle, 1f); // Removes particle effect after delay
                Destroy(_impactParticle, 1.5f); // Removes impact effect after delay
                Destroy(gameObject); // Removes the projectile
            }
        }
    }
}