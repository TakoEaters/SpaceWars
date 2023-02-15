using _Project.Scripts.ColorIt;
using Template.Scripts.General;
using UnityEngine;

namespace _Project.Scripts.Player.WeaponsSystem
{
    public class WeaponView : View
    {
        [SerializeField] private float _bulletSpeed = 1000f;
        [SerializeField] private LayerMask _necessaryLayer;
        [SerializeField] private GameObject _projectile;

        [SerializeField] private Transform _nozzle;

        public Transform Nozzle => _nozzle;

        private Camera _mainCamera;
        private int _damage;

        public void InitializeData(int damage)
        {
            _mainCamera = Camera.main;
            _damage = damage;
        }

        public void ShootProjectile()
        {
            Ray ray = _mainCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            if (Physics.Raycast(ray, out RaycastHit hit, 1000f, _necessaryLayer)) //Finds the point where you click with the mouse
            {
                GameObject projectile = Instantiate(_projectile, _nozzle.position, Quaternion.identity); //Spawns the selected projectile
                projectile.transform.LookAt(hit.point); //Sets the projectiles rotation to look at the point clicked
                projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * _bulletSpeed); //Set the speed of the projectile by applying force to the rigidbody
            }
        }
    }
}
