//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Template/Scripts/InputManager/PlayerInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInput : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""91cf61f1-e7f4-4818-a8d8-2e7f4195457a"",
            ""actions"": [
                {
                    ""name"": ""Joystick"",
                    ""type"": ""Value"",
                    ""id"": ""6e7969da-9b29-4bac-916c-74b42c1d909e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Shooting"",
                    ""type"": ""Button"",
                    ""id"": ""feb9a027-db84-45d3-a314-aeb83e175677"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Rotation"",
                    ""type"": ""Value"",
                    ""id"": ""120ad6c9-d941-41b3-9773-c8273fc51af4"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Aiming"",
                    ""type"": ""Button"",
                    ""id"": ""8ef12ace-d60e-476b-855f-1c6c169e016e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Running"",
                    ""type"": ""Button"",
                    ""id"": ""c0ab8547-76fd-41bc-905b-d2e1ad6ec07d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Reloading"",
                    ""type"": ""Button"",
                    ""id"": ""e6ed10ee-f2c4-49f0-8773-72f3e5a6b05c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""IsJumping"",
                    ""type"": ""Button"",
                    ""id"": ""d8f8bba3-f32f-4703-ad66-546ff4eb56a9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""AndroidJoystick"",
                    ""id"": ""15839ab8-0c9c-4480-8102-28587c9fcd34"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Joystick"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""5493167b-1977-4f63-9545-6507b82e4d16"",
                    ""path"": ""<Joystick>/stick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Joystick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""14a8f825-f0d7-4953-b88c-2e3c4edcb879"",
                    ""path"": ""<Joystick>/stick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Joystick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""dece7d15-4b36-4bb1-8df9-4b8085d4d8c5"",
                    ""path"": ""<Joystick>/stick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Joystick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""3c2e5305-2e15-42f7-8ff3-2cd7f0a7c623"",
                    ""path"": ""<Joystick>/stick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Joystick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""MovementKeyboard"",
                    ""id"": ""e581da99-972e-4793-b780-31a139a393af"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Joystick"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""15ed913e-7a11-474b-b94d-775789edac59"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Joystick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""426a4522-311e-43a4-9cb3-a648b244ace7"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Joystick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""a0980cda-c0f5-4d02-b1db-df1f5636646c"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Joystick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""410dfd2a-3055-4658-9e7d-1f21500ea26f"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Joystick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""710ef9f2-aa23-42b0-8d26-0c24c0716493"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Joystick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4dac1a26-57b0-4f32-8f79-0bec7c4c2bff"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shooting"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""87371c05-be6a-420e-a6c6-64cfa4e0685f"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6e594ba9-b1e0-4a70-9e94-8010ee7c6241"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d13da101-8ff9-491f-a5f7-af3e70b8b418"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aiming"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""405cacf5-9a19-4b60-9610-53a50fc34e71"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Running"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""24674d19-258f-4d5e-821a-eaf95d238592"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reloading"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e0576ff6-d5a9-4bc4-8f93-ef090cb9aada"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""IsJumping"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Joystick = m_Player.FindAction("Joystick", throwIfNotFound: true);
        m_Player_Shooting = m_Player.FindAction("Shooting", throwIfNotFound: true);
        m_Player_Rotation = m_Player.FindAction("Rotation", throwIfNotFound: true);
        m_Player_Aiming = m_Player.FindAction("Aiming", throwIfNotFound: true);
        m_Player_Running = m_Player.FindAction("Running", throwIfNotFound: true);
        m_Player_Reloading = m_Player.FindAction("Reloading", throwIfNotFound: true);
        m_Player_IsJumping = m_Player.FindAction("IsJumping", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Joystick;
    private readonly InputAction m_Player_Shooting;
    private readonly InputAction m_Player_Rotation;
    private readonly InputAction m_Player_Aiming;
    private readonly InputAction m_Player_Running;
    private readonly InputAction m_Player_Reloading;
    private readonly InputAction m_Player_IsJumping;
    public struct PlayerActions
    {
        private @PlayerInput m_Wrapper;
        public PlayerActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Joystick => m_Wrapper.m_Player_Joystick;
        public InputAction @Shooting => m_Wrapper.m_Player_Shooting;
        public InputAction @Rotation => m_Wrapper.m_Player_Rotation;
        public InputAction @Aiming => m_Wrapper.m_Player_Aiming;
        public InputAction @Running => m_Wrapper.m_Player_Running;
        public InputAction @Reloading => m_Wrapper.m_Player_Reloading;
        public InputAction @IsJumping => m_Wrapper.m_Player_IsJumping;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Joystick.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJoystick;
                @Joystick.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJoystick;
                @Joystick.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJoystick;
                @Shooting.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShooting;
                @Shooting.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShooting;
                @Shooting.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShooting;
                @Rotation.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotation;
                @Rotation.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotation;
                @Rotation.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotation;
                @Aiming.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAiming;
                @Aiming.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAiming;
                @Aiming.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAiming;
                @Running.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRunning;
                @Running.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRunning;
                @Running.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRunning;
                @Reloading.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReloading;
                @Reloading.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReloading;
                @Reloading.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReloading;
                @IsJumping.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnIsJumping;
                @IsJumping.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnIsJumping;
                @IsJumping.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnIsJumping;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Joystick.started += instance.OnJoystick;
                @Joystick.performed += instance.OnJoystick;
                @Joystick.canceled += instance.OnJoystick;
                @Shooting.started += instance.OnShooting;
                @Shooting.performed += instance.OnShooting;
                @Shooting.canceled += instance.OnShooting;
                @Rotation.started += instance.OnRotation;
                @Rotation.performed += instance.OnRotation;
                @Rotation.canceled += instance.OnRotation;
                @Aiming.started += instance.OnAiming;
                @Aiming.performed += instance.OnAiming;
                @Aiming.canceled += instance.OnAiming;
                @Running.started += instance.OnRunning;
                @Running.performed += instance.OnRunning;
                @Running.canceled += instance.OnRunning;
                @Reloading.started += instance.OnReloading;
                @Reloading.performed += instance.OnReloading;
                @Reloading.canceled += instance.OnReloading;
                @IsJumping.started += instance.OnIsJumping;
                @IsJumping.performed += instance.OnIsJumping;
                @IsJumping.canceled += instance.OnIsJumping;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnJoystick(InputAction.CallbackContext context);
        void OnShooting(InputAction.CallbackContext context);
        void OnRotation(InputAction.CallbackContext context);
        void OnAiming(InputAction.CallbackContext context);
        void OnRunning(InputAction.CallbackContext context);
        void OnReloading(InputAction.CallbackContext context);
        void OnIsJumping(InputAction.CallbackContext context);
    }
}
