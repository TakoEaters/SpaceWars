using System.Collections;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.General.LevelHandlers;
using _Project.Scripts.General.Signals;
using UnityEngine;

//This script requires you to have setup your animator with 3 parameters, "InputMagnitude", "InputX", "InputZ"
//With a blend tree to control the inputmagnitude and allow blending between animations.
namespace CustomAssets.Jammo_Character.Scripts
{
	[RequireComponent(typeof(CharacterController), typeof(PlayerInputs))]
	public class MovementInput : MonoBehaviour
	{

		public float Velocity;
		[Space] private PlayerInputs _inputs;
		private Animator anim;
		private Camera cam;
		private CharacterController controller;
		private bool isGrounded;
		private Vector3 desiredMoveDirection;
		private float InputX;
		private float InputZ;

		public bool blockRotationPlayer;
		public float desiredRotationSpeed = 0.1f;

		public float Speed;
		public float allowPlayerRotation = 0.1f;

		[Range(0, 1f)] public float StartAnimTime = 0.3f;
		[Range(0, 1f)] public float StopAnimTime = 0.15f;

		public float verticalVel;
		private Vector3 moveVector;
		private static readonly int Shooting = Animator.StringToHash("shooting");
		private static readonly int Blend = Animator.StringToHash("Blend");
		private static readonly int Y = Animator.StringToHash("Y");
		private static readonly int X = Animator.StringToHash("X");

		private Coroutine _inputRoutine;

		private void Awake()
		{
			Application.targetFrameRate = 60;
			_inputs = GetComponent<PlayerInputs>();
			anim = this.GetComponent<Animator>();
			cam = Camera.main;
			controller = this.GetComponent<CharacterController>();
		}

		[Sub]
		private void OnStartLevel(StartLevel reference)
		{
			_inputRoutine = StartCoroutine(UpdateInputs());
		}

		[Sub]
		private void OnPlayerDeath(PlayerDeath reference)
		{
			if (_inputRoutine != null) StopCoroutine(_inputRoutine);
		}

		private IEnumerator UpdateInputs()
		{
			while (true)
			{
				InputMagnitude();

				isGrounded = controller.isGrounded;
				if (isGrounded) verticalVel -= 0;
				else verticalVel -= 1;

				moveVector = new Vector3(0, verticalVel * .2f * Time.deltaTime, 0);
				controller.Move(moveVector);
				yield return null;
			}
			// ReSharper disable once IteratorNeverReturns
		}

		void PlayerMoveAndRotation()
		{
			InputX = _inputs.Movement.x;
			InputZ = _inputs.Movement.y;
			
			var forward = cam.transform.forward;
			var right = cam.transform.right;

			forward.y = 0f;
			right.y = 0f;

			forward.Normalize();
			right.Normalize();

			desiredMoveDirection = forward * InputZ + right * InputX;

			if (blockRotationPlayer == false)
			{
				//Camera
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection),
					desiredRotationSpeed);
				controller.Move(desiredMoveDirection * Time.deltaTime * Velocity);
			}
			else
			{
				//Strafe
				controller.Move((transform.forward * InputZ + transform.right * InputX) * Time.deltaTime * Velocity);
			}
		}

		public void RotateToCamera(Transform t)
		{
			var forward = cam.transform.forward;

			desiredMoveDirection = forward;
			Quaternion lookAtRotation = Quaternion.LookRotation(desiredMoveDirection);
			Quaternion lookAtRotationOnly_Y = Quaternion.Euler(transform.rotation.eulerAngles.x,
				lookAtRotation.eulerAngles.y, transform.rotation.eulerAngles.z);

			t.rotation = Quaternion.Slerp(transform.rotation, lookAtRotationOnly_Y, desiredRotationSpeed);
		}

		private void InputMagnitude()
		{
			//Calculate Input Vectors
			InputX = _inputs.Movement.x;
			InputZ = _inputs.Movement.y;

			//Calculate the Input Magnitude
			Speed = new Vector2(InputX, InputZ).sqrMagnitude;

			//Change animation mode if rotation is blocked
			anim.SetBool(Shooting, blockRotationPlayer);

			//Physically move player
			if (Speed > allowPlayerRotation)
			{
				anim.SetFloat(Blend, Speed, StartAnimTime, Time.deltaTime);
				anim.SetFloat(X, InputX, StartAnimTime / 3, Time.deltaTime);
				anim.SetFloat(Y, InputZ, StartAnimTime / 3, Time.deltaTime);
				PlayerMoveAndRotation();
			}
			else if (Speed < allowPlayerRotation)
			{
				anim.SetFloat(Blend, Speed, StopAnimTime, Time.deltaTime);
				anim.SetFloat(X, InputX, StopAnimTime / 3, Time.deltaTime);
				anim.SetFloat(Y, InputZ, StopAnimTime / 3, Time.deltaTime);
			}
		}
	}
}
