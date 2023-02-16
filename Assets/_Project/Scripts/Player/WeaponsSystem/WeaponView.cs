using System;
using _Project.Scripts.ColorIt;
using Template.Scripts.General;
using UnityEngine;

namespace _Project.Scripts.Player.WeaponsSystem
{
    public class WeaponView : View
    {
        [SerializeField] private Transform _lookAt;
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

        private void Update()
        {
            _lookAt.transform.position = Position();
        }

        private Vector3 Position()
        {
            var point = _mainCamera.transform.forward * 20f + _mainCamera.transform.position;

            if (Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward, out RaycastHit hit, 1000f, _necessaryLayer)) //Finds the point where you click with the mouse
            {
                point = hit.point;
            }

            return point;
        }
        
        public void ShootProjectile()
        {
            GameObject projectile = Instantiate(_projectile, _nozzle.position, Quaternion.identity); //Spawns the selected projectile
            projectile.transform.position = _nozzle.position;
            projectile.transform.LookAt(Position());
            projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * _bulletSpeed); //Set the speed of the projectile by applying force to the rigidbody
        }
    }
}
