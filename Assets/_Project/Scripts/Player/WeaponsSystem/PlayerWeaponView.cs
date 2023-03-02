using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.General.DamageableCore;
using _Project.Scripts.General.Signals;
using _Project.Scripts.GUi.Interface;
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
            Vector3 lookAtPoint = MainCamera.transform.forward * 20f + MainCamera.transform.position;
            LookAtPoint.Transform.position = lookAtPoint;
        }

        public override void ShootProjectile()
        {
            ETFXProjectileScript projectile = Instantiate(_projectile, Nozzle.position, Quaternion.identity); //Spawns the selected projectile
            projectile.OnKillTarget(OnKillTarget, () => Signal.Current.Fire<OnTakeDamageAtEnemy>(new OnTakeDamageAtEnemy {Damage = Damage}));
            projectile.transform.LookAt(Position());
            projectile.GetComponent<Projectile>().InitializeProjectileData(_weaponTeam, Damage);
            projectile.Rigidbody.AddForce(projectile.transform.forward * _bulletSpeed); 
        }

        public override void ShootProjectile(IDamageable damageable)
        {
            
        }

        private void OnKillTarget(string nickname, Team team)
        {
            Signal.Current.Fire<OnKillTarget>(new OnKillTarget {Name = nickname, Team = team});
        }

        private Vector3 Position()
        { 
            var point = MainCamera.transform.forward * 50f + MainCamera.transform.position;

            if (Physics.Raycast(MainCamera.transform.position, MainCamera.transform.forward, out RaycastHit hit, 1000f, _necessaryLayer)) //Finds the point where you click with the mouse
            {
                point = hit.point;
            }

            return point;
        }
    }
}
