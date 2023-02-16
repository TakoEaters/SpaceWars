using _Project.Scripts.General.Signals;
using _Project.Scripts.General.Utils;
using _Project.Scripts.Player.SkinChanger;
using UnityEngine;

namespace _Project.Scripts.AI
{
    public class Bot : AIController
    {
        [SerializeField, Range(0.5f, 3.0f)] private float _waitRespawnDelay = 2f;
        public void Initialize(Team team)
        {
            OnDeath = OnAIDie;
            _configs.Team = team;

            Controller = GetComponent<CharacterController>();
            Animator = GetComponent<Animator>();
            SkinsChanger = GetComponentInChildren<SkinsChanger>();
            
            FindServices();
        }

        private void OnAIRevive()
        {
            SkinsChanger.DisableMesh();
            UpdateBotData();
            EnableController();
        }

        private void OnAIDie()
        {
            DisableController();
            StartCoroutine(WaitUtils.WaitWithDelay(OnAIRevive, _waitRespawnDelay));
        }
    }
}
