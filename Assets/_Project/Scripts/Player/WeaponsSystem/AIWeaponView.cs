using UnityEngine;

namespace _Project.Scripts.Player.WeaponsSystem
{
    public class AIWeaponView : WeaponView
    {
        public override void InitializeData(int damage)
        {
            MainCamera = Camera.main;
            Damage = damage;
        }

        public override void ShootProjectile()
        {
            
        }
    }
}
