using _Project.Scripts.General.InputHandlers;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class Player : PlayerController
    {
        public void Initialize()
        {
            FindServices();
            PlayerAnimator = GetComponent<Animator>();
            Controller = GetComponent<CharacterController>();
            Inputs = GetComponent<PlayerInputs>();
            
            Camera = Camera.main;

            StartCoroutine(RestoreHealth());
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
