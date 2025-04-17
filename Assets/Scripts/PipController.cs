using UnityEngine;

public class PipController : MonoBehaviour, IPlayerControllable
{
    Rigidbody _rb;
    [SerializeField] float _moveSpeed = 100;
    [SerializeField] float _maxSpeed = 10;
    [SerializeField] float _rotationSpeed = 360;
    [SerializeField] Transform _cameraTransform;
    [SerializeField] Transform _rotationPivot;


    [SerializeField] float _jumpForceTotal = 500;
    [SerializeField] float _jumpForceIncrement = 100;
    bool _isJumping = false;

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

    public void Move(Vector2 axis)
    {
        if (axis.magnitude > 0)
        {
            var pip = new Vector2(transform.position.x, transform.position.z);
            var camera = new Vector2(_cameraTransform.position.x, _cameraTransform.position.z);
            var gevraagdeDir = axis;
            var alpha = Mathf.Atan2(pip.y - camera.y, pip.x - camera.x);
            var beta = Mathf.Atan2(gevraagdeDir.y, gevraagdeDir.x) - (90 * Mathf.Deg2Rad);
            var y = alpha + beta;
            //y *= Mathf.Rad2Deg;
            var dir = new Vector2(Mathf.Cos(y), Mathf.Sin(y)) + camera;
            var uitkomst = (dir - camera).normalized;
            _rotationPivot.rotation = Quaternion.RotateTowards(_rotationPivot.rotation, Quaternion.Euler(new Vector3(0, Mathf.Atan2(uitkomst.x, uitkomst.y) * Mathf.Rad2Deg, 0)), _rotationSpeed * Time.deltaTime);
            var force = new Vector3(uitkomst.x, 0, uitkomst.y) * _moveSpeed * Time.deltaTime;
            _rb.AddForce(force);
        }
    }

    void FixedUpdate()
    {
        if (_rb.linearVelocity.magnitude > _maxSpeed)
        {
            _rb.linearVelocity = _rb.linearVelocity.normalized * _maxSpeed;
        }
    }

    public void MoveCamera(Vector2 axis)
    {
        throw new System.NotImplementedException();
    }

    public void JumpStarted()
    {
        _rb.AddForce(0, _jumpForceTotal, 0);
    }

    public void JumpHeld()
    {

    }

    public void JumpReleased()
    {

    }
}
