using UnityEngine;

namespace _Project.Scripts.Player.WeaponsSystem
{
    public class PlayerWeaponView : WeaponView
    {
        [SerializeField] private float _bulletSpeed = 1000f;
        [SerializeField] private LayerMask _necessaryLayer;
        [SerializeField] private GameObject _projectile;

        public override void InitializeData(int damage)
        {
            MainCamera = Camera.main;
            Damage = damage;
        }

        private void Update()
        {
            LookAtPoint.Transform.position = Position();
        }

        public override void ShootProjectile()
        {
            GameObject projectile = Instantiate(_projectile, Nozzle.position, Quaternion.identity); //Spawns the selected projectile
            projectile.transform.position = Nozzle.position;
            projectile.transform.LookAt(Position());
            projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * _bulletSpeed); //Set the speed of the projectile by applying force to the rigidbody
        }
        
        private Vector3 Position()
        {
            var point = MainCamera.transform.forward * 20f + MainCamera.transform.position;

            if (Physics.Raycast(MainCamera.transform.position, MainCamera.transform.forward, out RaycastHit hit, 1000f, _necessaryLayer)) //Finds the point where you click with the mouse
            {
                point = hit.point;
            }

            return point;
        }
    }
}
