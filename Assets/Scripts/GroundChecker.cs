using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public bool IsGrounded;
    [SerializeField] float _rayLength = 0.2f;
    [SerializeField] Rigidbody _rb;

    private void Update()
    {
        var raySizeModifier = 1 + Mathf.Abs(_rb.linearVelocity.y) * Time.deltaTime; //account for high falls
        IsGrounded = Physics.Raycast(transform.position, Vector3.down, _rayLength * raySizeModifier);
        Debug.DrawRay(transform.position, Vector3.down * _rayLength * raySizeModifier, Color.red, Time.deltaTime);
        DebugManager.SetText(_rb.linearVelocity.y.ToString() + ", " + raySizeModifier + "= " + IsGrounded);
    }
}
