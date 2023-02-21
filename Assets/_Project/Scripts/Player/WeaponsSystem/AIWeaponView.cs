using System;
using _Project.Scripts.General.DamageableCore;
using _Project.Scripts.General.Extensions;
using _Project.Scripts.General.Signals;
using Unity.VisualScripting;
using UnityEngine;

namespace _Project.Scripts.Player.WeaponsSystem
{
    public class AIWeaponView : WeaponView
    {
        [SerializeField] private float _bulletSpeed = 1000f;
        
        
        [SerializeField] private GameObject _projectile;
        
        private Team _weaponTeam;
        
        public override void InitializeData(Team preferredTeam, int damage)
        {
            _weaponTeam = preferredTeam;
            MainCamera = Camera.main;
            Damage = damage;
        }

        public override void ShootProjectile()
        {
        }

        public override void ShootProjectile(IDamageable damageable)
        {
            GameObject projectile = Instantiate(_projectile, transform.position, Quaternion.identity); //Spawns the selected projectile
            projectile.transform.LookAt(damageable.Position.AddY(0.75f));
            projectile.GetComponent<Projectile>().InitializeProjectileData(_weaponTeam);
            projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * _bulletSpeed); //Set the speed of the projectile by applying force to the rigidbody   
        }
    }
}
