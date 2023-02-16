using _Project.Scripts.General.DamageableCore;
using _Project.Scripts.General.Signals;
using UnityEngine;

namespace _Project.Scripts.AI
{
    public class Bot : AIController
    {
        public void Initialize(Team team)
        {
            _configs.Team = team;

            Animator = GetComponent<Animator>();
            
            FindServices();
            
        }
    }
}
