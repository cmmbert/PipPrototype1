using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] Transform _toFollow;
    void Update()
    {
        transform.LookAt(_toFollow);
    }

    public void OnCameraMove(Vector2 axis)
    {
        if (Mathf.Abs(axis.x) > 0)
        {
            transform.RotateAround(_toFollow.position, Vector3.up, Mathf.Sign(axis.x) * (axis.x / axis.x));
        }
    }
}
