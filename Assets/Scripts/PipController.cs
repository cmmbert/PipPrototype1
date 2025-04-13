using UnityEngine;

public class PipController : MonoBehaviour, IPlayerControllable
{
    Rigidbody _rb;
    [SerializeField] float _moveSpeed = 100;
    [SerializeField] Transform _cameraTransform;
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
            Debug.DrawLine(new Vector3(pip.x, 0, pip.y), new Vector3(uitkomst.x, 0, uitkomst.y), Color.red, 2);
            transform.LookAt(transform.position + new Vector3(uitkomst.x, 0, uitkomst.y));
        }
        //_rb.AddForce(new Vector3(axis.x * _moveSpeed * Time.deltaTime, 0, axis.y * _moveSpeed * Time.deltaTime));
    }

    public void MoveCamera(Vector2 axis)
    {
        throw new System.NotImplementedException();
    }

}
