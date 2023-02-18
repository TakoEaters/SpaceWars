using System;
using _Project.Scripts.General.DamageableCore;
using _Project.Scripts.General.Signals;
using CustomAssets.Epic_Toon_FX.Demo.Scripts;
using UnityEngine;

namespace _Project.Scripts.Player.WeaponsSystem
{
    public class PlayerWeaponView : WeaponView
    {
        [SerializeField] private float _bulletSpeed = 1000f;
        [SerializeField] private LayerMask _necessaryLayer;
        [SerializeField] private ETFXProjectileScript _projectile;

        private Team _weaponTeam;
        
        public override void InitializeData(Team preferredTeam, int damage)
        {
            _weaponTeam = preferredTeam;
            MainCamera = Camera.main;
            Damage = damage;
        }

        private void Update()
        {
            LookAtPoint.Transform.position = Position();
        }

        public override void ShootProjectile(Action callback)
        {
            ETFXProjectileScript projectile = Instantiate(_projectile, Nozzle.position, Quaternion.identity); //Spawns the selected projectile
            projectile.OnKillTarget(callback);
            projectile.transform.position = Nozzle.position;
            projectile.transform.LookAt(Position());
            projectile.GetComponent<Projectile>().InitializeProjectileData(_weaponTeam);
            projectile.Rigidbody.AddForce(projectile.transform.forward * _bulletSpeed); 
        }

        public override void ShootProjectile(Action callback, IDamageable damageable)
        {
            
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
