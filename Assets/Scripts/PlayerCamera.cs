using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] Transform _cameraRotationPoint;
    [SerializeField] Transform _toFollow;
    [SerializeField] float _lateralRotationSpeed = 45;


    Vector2 _requestedRotation;
    

    public void OnCameraMove(Vector2 axis)
    {
        _requestedRotation = axis;
        if (_requestedRotation.x > 1) _requestedRotation.x = 1;
        if (_requestedRotation.x < -1) _requestedRotation.x = -1;
    }

    private void FixedUpdate()
    {
        CameraRotation();
    }

    void CameraRotation()
    {
        if (Mathf.Abs(_requestedRotation.x) > 0)
        {
            _cameraRotationPoint.Rotate(new Vector3(0, _requestedRotation.x * _lateralRotationSpeed * Time.deltaTime, 0));
        }
    }

}
