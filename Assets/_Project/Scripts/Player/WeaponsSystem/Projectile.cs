using _Project.Scripts.General.DamageableCore;
using _Project.Scripts.General.Signals;
using Template.Scripts.Pool;
using UnityEngine;

namespace _Project.Scripts.Player.WeaponsSystem
{
    public class Projectile : CorePoolElement
    {
        private Team _currentTeam;
        
        
        public void StartM(Team team)
        {
            _currentTeam = team;
        }

        public void DetectTarget(GameObject target, int damage)
        {
            if (target.TryGetComponent(out IDamageable damageable) && damageable.IsAlive && damageable.Team != _currentTeam)
            {
                damageable.OnTakeDamage(damage);
            }
        }
    }
}
