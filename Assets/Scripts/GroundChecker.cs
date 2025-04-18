using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public bool IsGrounded;
    [SerializeField] float _rayLength = 0.2f;
    private void Update()
    {
        IsGrounded = Physics.Raycast(transform.position, Vector3.down, _rayLength);
        Debug.DrawRay(transform.position, Vector3.down * _rayLength, Color.red);
        DebugManager.SetText(IsGrounded.ToString());
    }
}
