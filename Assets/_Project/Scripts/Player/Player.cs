using _Project.Scripts.General.InputHandlers;
using _Project.Scripts.Player.SkinChanger;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class Player : PlayerController
    {
        public void Initialize()
        {
            GetComponentInChildren<SkinsChanger>().SetSkin();
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
            UpdateAiming();
        }

        private void LateUpdate()
        {
            LerpHealth();
        }
    }
}
