using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerInput _input;
    IPlayerControllable _playerControllable;
    [SerializeField] PlayerCamera _playerCamera;

    Vector2 _moveVector;
    Vector2 _cameraMoveVector;


    InputAction _jumpAction;

    private void Start()
    {
        _input = GetComponent<PlayerInput>();
        _playerControllable = GetComponentInChildren<IPlayerControllable>();
        _jumpAction = _input.actions["Jump"];
    }

    private void Update()
    {
        _playerControllable.Move(_moveVector);
        _playerCamera.OnCameraMove(_cameraMoveVector);
        HandleJump();
    }

    public void OnMove(InputValue value)
    {
        _moveVector = value.Get<Vector2>();
    }

    public void OnJumpHold()
    {
        Debug.Log("JumpHold");
    }

    public void OnLook(InputValue value)
    {
        _cameraMoveVector = value.Get<Vector2>();
    }

    void HandleJump()
    {
        if (_jumpAction.IsPressed())
            _playerControllable.JumpHeld();
        if (_jumpAction.WasPressedThisFrame())
            _playerControllable.JumpStarted();
        if (_jumpAction.WasReleasedThisFrame())
            _playerControllable.JumpReleased();
    }
}
