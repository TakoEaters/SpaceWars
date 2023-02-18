using _Project.Scripts.General.DamageableCore;
using _Project.Scripts.Player.WeaponsSystem;
using UnityEngine;

namespace CustomAssets.Epic_Toon_FX.Demo.Scripts
{
    public class ETFXProjectileScript : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private SphereCollider _sphereCollider;
      //  public GameObject impactParticle; // Effect spawned when projectile hits a collider
     //   public GameObject projectileParticle; // Effect attached to the gameobject as child
     //   public GameObject muzzleParticle; // Effect instantly spawned when gameobject is spawned
        [Header("Adjust if not using Sphere Collider")]
        public float colliderRadius = 1f;
        [Range(0f, 1f)] // This is an offset that moves the impact effect slightly away from the point of impact to reduce clipping of the impact effect
        public float collideOffset = 0.15f;

        [SerializeField] private DrawController _draw;

        private Projectile _projectile;
        
        void Start()
        {
            _projectile = GetComponent<Projectile>();
            _projectile.CurrentFX.TrailParticle = Instantiate(_projectile.CurrentFX.TrailParticle, transform.position, transform.rotation);
            _projectile.CurrentFX.TrailParticle.transform.parent = transform;
            if (_projectile.CurrentFX.MuzzleParticle)
            {
                _projectile.CurrentFX.MuzzleParticle = Instantiate(_projectile.CurrentFX.MuzzleParticle, transform.position, transform.rotation);
                Destroy(_projectile.CurrentFX.MuzzleParticle, 1.5f); // 2nd parameter is lifetime of effect in seconds
            }
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

            if (Physics.SphereCast(transform.position, radius, direction, out hit, detectionDistance)) // Checks if collision will happen
            {
                transform.position = hit.point + (hit.normal * collideOffset); // Move projectile to point of collision

                GameObject impactP = Instantiate(_projectile.CurrentFX.HitParticle.gameObject, transform.position, Quaternion.FromToRotation(Vector3.up, hit.normal)) as GameObject; // Spawns impact effect

                _draw.OnPaint(hit.collider.gameObject, hit.point);
                GetComponent<Projectile>().DetectTarget(hit.collider.gameObject, 25);

                Destroy(_projectile.CurrentFX.TrailParticle, 3f); // Removes particle effect after delay
                Destroy(impactP, 3.5f); // Removes impact effect after delay
                Destroy(gameObject); // Removes the projectile
            }
        }
    }
}