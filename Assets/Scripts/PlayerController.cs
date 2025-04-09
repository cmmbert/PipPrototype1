using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    IPlayerControllable _playerControllable;


    Vector2 _moveVector;
    private void Start()
    {
        _playerControllable = GetComponentInChildren<IPlayerControllable>();
    }

    private void Update()
    {
        _playerControllable.Move(_moveVector);

    }

    public void OnMove(InputValue value)
    {
        _moveVector = value.Get<Vector2>();
    }

    public void OnJump()
    {
        _playerControllable.Jump();
    }
}
