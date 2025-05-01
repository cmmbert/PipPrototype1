using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] float _overrideRotationSpeedMultiplier = 0.5f;
    [SerializeField] Transform _cameraRotationPoint;
    [SerializeField] Transform _anchor;
    [SerializeField] float _lateralRotationSpeed = 45;
    [SerializeField] float _maxDistance = 4;
    [SerializeField] Transform _desiredPosition;

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

    private void LateUpdate()
    {
        HandleClipping();
    }

    bool _isBeingOverriden = false;
    void CameraRotation()
    {
        if (!_isBeingOverriden)
            if (Mathf.Abs(_requestedRotation.x) > 0)
            {
                _cameraRotationPoint.Rotate(new Vector3(0, _requestedRotation.x * _lateralRotationSpeed * Time.deltaTime, 0));
            }
    }

    public void OverrideCameraAngle(float newAngle)
    {
        _cameraRotationPoint.rotation = Quaternion.RotateTowards(_cameraRotationPoint.rotation, Quaternion.Euler(new Vector3(0, newAngle, 0)), _lateralRotationSpeed * Time.deltaTime * _overrideRotationSpeedMultiplier);
        _isBeingOverriden = true;
    }

    public void StopOverride()
    {
        _isBeingOverriden = false;
    }

    void HandleClipping()
    {
        RaycastHit hit;
        Vector3 direction = _desiredPosition.position - _anchor.position;
        Ray ray = new Ray(_anchor.position, direction);
        Debug.DrawLine(_anchor.position, _anchor.position + direction.normalized * _maxDistance, Color.yellow);
        DebugManager.SetText(direction.ToString());
        if (Physics.Raycast(ray, out hit, _maxDistance))
        {
            transform.position = hit.point;
            DebugManager.SetText("hit");
        }
        else
        {
            transform.position = _desiredPosition.position;
            DebugManager.SetText("no hit");
        }
    }

}
