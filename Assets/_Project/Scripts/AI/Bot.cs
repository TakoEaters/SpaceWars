using _Project.Scripts.General.DamageableCore;
using _Project.Scripts.General.Signals;
using _Project.Scripts.General.Utils;
using _Project.Scripts.Player.SkinChanger;
using UnityEngine;

namespace _Project.Scripts.AI
{
    public class Bot : AIController
    {
        [SerializeField, Range(0.5f, 3.0f)] private float _waitRespawnDelay = 2f;

        private NicknameViewer _viewer;
        
        public void Initialize(Team team)
        {
            OnDeath = OnAIDie;
            _configs.Team = team;

            Collider = GetComponent<CapsuleCollider>();
            Scanner = GetComponentInChildren<DamageableScanner>();
            _viewer = GetComponentInChildren<NicknameViewer>();
            States = GetComponent<AIStates>();
            Animator = GetComponent<Animator>();
            SkinsChanger = GetComponentInChildren<SkinsChanger>();

            FindServices();
            _viewer.Initialize(team);
            Nickname = _viewer.Nick;
        }

        private void OnAIRevive()
        { 
            SkinsChanger.DisableMesh();
            UpdateBotData();
            EnableController();
            _viewer.EnableView();
        }

        private void Update()
        {
            OnAttack();
            UpdateAnimator();
            UpdateStates();
        }

        private void OnAIDie()
        {
            _viewer.DisableView();
            DisableController();
            StartCoroutine(WaitUtils.WaitWithDelay(OnAIRevive, _waitRespawnDelay));
        }
    }
}
