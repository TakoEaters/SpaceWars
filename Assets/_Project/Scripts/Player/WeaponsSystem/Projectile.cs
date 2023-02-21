using System;
using System.Collections.Generic;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.General.DamageableCore;
using _Project.Scripts.General.Signals;
using _Project.Scripts.GUi.Interface;
using Template.Scripts.Pool;
using UnityEngine;

namespace _Project.Scripts.Player.WeaponsSystem
{
    public class Projectile : CorePoolElement
    {
        [SerializeField] private List<ProjectileFX> _fx = new List<ProjectileFX>();

        private Team _currentTeam;
        public ProjectileFX CurrentFX { get; private set; }
        
        
        public void InitializeProjectileData(Team team)
        {
            _currentTeam = team;
            CurrentFX = _fx.Find(x => x.Team == team);
        }

        public void DetectTarget(GameObject target, int damage, Action<string, Team> killedTarget, Action takeDamage)
        {
            if (target.TryGetComponent(out IDamageable damageable) && damageable.IsAlive && damageable.Team != _currentTeam)
            {
                damageable.OnTakeDamage(damage);
                takeDamage?.Invoke();
                if (damageable.IsAlive == false) killedTarget?.Invoke(damageable.Nickname, damageable.Team);
            }
        }
    }

    [Serializable]
    public class ProjectileFX
    {
        public Team Team;
        public ParticleSystem MuzzleParticle;
        public ParticleSystem HitParticle;
        public ParticleSystem TrailParticle;
    }
}
