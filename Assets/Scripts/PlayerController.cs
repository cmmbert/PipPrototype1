using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    IPlayerControllable _playerControllable;
    [SerializeField] PlayerCamera _playerCamera;

    Vector2 _moveVector;
    Vector2 _cameraMoveVector;
    private void Start()
    {
        _playerControllable = GetComponentInChildren<IPlayerControllable>();
    }

    private void Update()
    {
        _playerControllable.Move(_moveVector);
        _playerCamera.OnCameraMove(_cameraMoveVector);
    }

    public void OnMove(InputValue value)
    {
        _moveVector = value.Get<Vector2>();
    }

    public void OnJump()
    {
        _playerControllable.Jump();
    }

    public void OnLook(InputValue value)
    {
        _cameraMoveVector = value.Get<Vector2>();
    }
}
