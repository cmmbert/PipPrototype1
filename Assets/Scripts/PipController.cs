using UnityEngine;

public class PipController : MonoBehaviour, IPlayerControllable
{
    Rigidbody _rb;
    [SerializeField] float _moveSpeed = 100;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Attack1()
    {
        throw new System.NotImplementedException();
    }

    public void Attack2()
    {
        throw new System.NotImplementedException();
    }

    public void Jump()
    {
        Debug.Log("jump");
    }

    public void Move(Vector2 axis)
    {
        Debug.Log(axis);
        _rb.AddForce(new Vector3(axis.x * _moveSpeed, 0, axis.y * _moveSpeed));
    }

    public void MoveCamera(Vector2 axis)
    {
        throw new System.NotImplementedException();
    }

}
