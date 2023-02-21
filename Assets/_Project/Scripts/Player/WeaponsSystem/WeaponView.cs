using System;
using _Project.Scripts.General.DamageableCore;
using _Project.Scripts.General.Signals;
using _Project.Scripts.General.Utils;
using Template.Scripts.General;
using UnityEngine;

namespace _Project.Scripts.Player.WeaponsSystem
{
    public abstract class WeaponView : View
    {
        [SerializeField] private Transform _nozzle;

        public Transform Nozzle => _nozzle;

        protected Camera MainCamera;
        protected LookAtPoint LookAtPoint;
        protected int Damage;

        protected override void OnAwake()
        {
            LookAtPoint = FindObjectOfType<LookAtPoint>();
        }

        public abstract void InitializeData(Team preferredTeam, int damage);
        public abstract void ShootProjectile();
        public abstract void ShootProjectile(IDamageable damageable);
    }
}
