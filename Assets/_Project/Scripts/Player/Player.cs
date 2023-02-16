using _Project.Scripts.General.InputHandlers;
using _Project.Scripts.General.Signals;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class Player : PlayerController
    {
        public void Initialize(Team playerTeam)
        {
            _configs.Team = playerTeam;
            FindServices();
            PlayerAnimator = GetComponent<Animator>();
            Controller = GetComponent<CharacterController>();
            Inputs = GetComponent<PlayerInputs>();
            
            Camera = Camera.main;
        }


        private void Update()
        {
            UpdateMovement();
            UpdateWeapon();
            UpdateOverheat();
        }

        private void LateUpdate()
        {
            LerpHealth();
        }

        private void FixedUpdate()
        {
            
        }
    }
}
