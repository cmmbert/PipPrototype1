using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] float _overrideRotationSpeedMultiplier = 0.5f;
    [SerializeField] Transform _horizontalCameraRotationPoint;
    [SerializeField] Transform _verticalCameraRotationPoint;
    [SerializeField] Transform _anchor;
    [SerializeField] float _lateralRotationSpeed = 45;
    [SerializeField] float _maxDistance = 4;
    [SerializeField] Transform _desiredPosition;
    [SerializeField] float _maxVerticalAngle = 45;
    [SerializeField] float _verticalMinMovement = 0.1f;
    [SerializeField] float _clippingOffset = 0.8f;

    Vector2 _requestedRotation;


    public void OnCameraMove(Vector2 axis)
    {
        _requestedRotation = axis;
        if (_requestedRotation.x > 1) _requestedRotation.x = 1;
        if (_requestedRotation.x < -1) _requestedRotation.x = -1;

        if (_requestedRotation.y > 1) _requestedRotation.y = 1;
        if (_requestedRotation.y < -1) _requestedRotation.y = -1;
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
        {
            if (Mathf.Abs(_requestedRotation.x) > 0)
            {
                _horizontalCameraRotationPoint.Rotate(new Vector3(0, _requestedRotation.x * _lateralRotationSpeed * Time.deltaTime, 0));
            }
            if (Mathf.Abs(_requestedRotation.y) > _verticalMinMovement)
            {
                _requestedRotation -= new Vector2(0, _verticalMinMovement) * Mathf.Sign(_requestedRotation.y);
                _verticalCameraRotationPoint.Rotate(new Vector3(-_requestedRotation.y * _lateralRotationSpeed * Time.deltaTime, 0, 0));
                var xAngle = _verticalCameraRotationPoint.localRotation.eulerAngles.x;
                xAngle = Mathf.Clamp(NormalizeAngle(xAngle), -_maxVerticalAngle, _maxVerticalAngle);
                _verticalCameraRotationPoint.localRotation = Quaternion.Euler(xAngle, 0, 0);
            }
        }
    }

    //return angle in range -180 to 180
    float NormalizeAngle(float a)
    {
        if (a > 180)
        {
            return a - 360;
        }
        return a;
    }

    public void OverrideCameraAngle(float newAngle)
    {
        _horizontalCameraRotationPoint.rotation = Quaternion.RotateTowards(_horizontalCameraRotationPoint.rotation, Quaternion.Euler(new Vector3(0, newAngle, 0)), _lateralRotationSpeed * Time.deltaTime * _overrideRotationSpeedMultiplier);
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
        if (Physics.Raycast(ray, out hit, _maxDistance))
        {
            var hitVector = (hit.point - _anchor.position);
            var hitDistance = (hitVector.magnitude / _maxDistance);
            DebugManager.SetText(hitDistance.ToString());
            transform.position = (hitVector * (_clippingOffset * hitDistance)) + _anchor.position;
        }
        else
        {
            transform.position = _desiredPosition.position;
        }
    }

}
