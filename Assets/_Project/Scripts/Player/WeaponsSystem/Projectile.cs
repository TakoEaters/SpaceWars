using System;
using System.Collections.Generic;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.General.DamageableCore;
using _Project.Scripts.General.Signals;
using _Project.Scripts.GUi.Gameplay;
using _Project.Scripts.GUi.Interface;
using Template.Scripts.Pool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Player.WeaponsSystem
{
    public class Projectile : CorePoolElement
    {
        [SerializeField] private List<ProjectileFX> _fx = new List<ProjectileFX>();

        private Team _currentTeam;
        private string _nickName;
        private int _damage;
        public ProjectileFX CurrentFX { get; private set; }
        
        
        public void InitializeProjectileData(Team team, int damage, string nick)
        {
            _nickName = nick;
            _damage = damage;
            _currentTeam = team;
            CurrentFX = _fx.Find(x => x.Team == team);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void DetectTarget(GameObject target, Action<string, Team> killedTarget, Action takeDamage)
        {
            if (target.TryGetComponent(out IDamageable damageable) && damageable.IsAlive && damageable.Team != _currentTeam)
            {
                damageable.OnTakeDamage(_damage);
                takeDamage?.Invoke();
                if (damageable.IsAlive == false)
                {
                    ServiceLocator.Current.Get<ILeaderboardView>().UpdateData(_currentTeam, _nickName);
                    killedTarget?.Invoke(damageable.Nickname, damageable.Team);
                }
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
