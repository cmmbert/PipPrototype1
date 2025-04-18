using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public bool IsGrounded;

    private void Update()
    {
        IsGrounded = Physics.Raycast(transform.position + new Vector3(0, 0.01f), Vector3.down, 0.10f);
        Debug.DrawRay(transform.position, Vector3.down * 0.10f, Color.red);
        DebugManager.SetText(IsGrounded.ToString());
    }
}
