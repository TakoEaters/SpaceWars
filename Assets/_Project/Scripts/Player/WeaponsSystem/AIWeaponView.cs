using _Project.Scripts.General.DamageableCore;
using _Project.Scripts.General.Extensions;
using _Project.Scripts.General.Signals;
using UnityEngine;

namespace _Project.Scripts.Player.WeaponsSystem
{
    public class AIWeaponView : WeaponView
    {
        [SerializeField] private LayerMask _rayCastLayer;
        [SerializeField] private float _bulletSpeed = 1000f;
        
        
        [SerializeField] private GameObject _projectile;
        
        private Team _weaponTeam;
        private string _nickName;
        private int _damage;
        
        public override void InitializeData(Team preferredTeam, int damage, string nick)
        {
            _nickName = nick;
            _damage = damage;
            _weaponTeam = preferredTeam;
            MainCamera = Camera.main;
            Damage = damage;
        }

        public override void ShootProjectile() { }
        
        
        public override void ShootProjectile(IDamageable damageable)
        {
            GameObject projectile = Instantiate(_projectile, Nozzle.position, Quaternion.identity); //Spawns the selected projectile
            projectile.transform.LookAt(damageable.Position.AddY(0.75f));
            projectile.GetComponent<Projectile>().InitializeProjectileData(_weaponTeam, _damage, _nickName);
            projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * _bulletSpeed); //Set the speed of the projectile by applying force to the rigidbody   
        }
    }
}
