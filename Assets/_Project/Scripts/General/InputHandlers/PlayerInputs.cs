using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    public Vector2 Movement => _input.Player.Joystick.ReadValue<Vector2>();
    public Vector2 Rotation => _input.Player.Rotation.ReadValue<Vector2>();
    
    public bool IsShootingPressed => _input.Player.Shooting.WasPressedThisFrame();
    public bool IsShootingReleased => _input.Player.Shooting.WasReleasedThisFrame();
    public bool IsShooting => _input.Player.Shooting.ReadValue<float>() > 0;
    
    private PlayerInput _input;

    private void Awake()
    {
        _input = new PlayerInput();
    }

    private void OnEnable()
    {
        _input.Enable();
    }
}
