using UnityEngine;

public class PipController : MonoBehaviour, IPlayerControllable
{
    Rigidbody _rb;
    [SerializeField] float _moveSpeed = 100;
    [SerializeField] float _maxSpeed = 10;
    [SerializeField] float _rotationSpeed = 360;
    [SerializeField] Transform _cameraTransform;
    [SerializeField] Transform _rotationPivot;
    [SerializeField] GroundChecker _groundChecker;
    [SerializeField] float _facingForceCorrection = 3;
    [SerializeField] float _brakeValue = 0.9f;

    [SerializeField] float _jumpRotationModifier = 0.35f;
    [SerializeField] float _jumpVelocity = 6;
    bool _isHoldingJumpButton = false;

    [SerializeField] float _extraGravity = 2;
    [SerializeField] float _bounceMovementMultiplier = 1.5f;
    [SerializeField] Tail _tail;

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
            //Definitions
            var pip = new Vector2(transform.position.x, transform.position.z);
            var camera = new Vector2(_cameraTransform.position.x, _cameraTransform.position.z);
            var gevraagdeDir = axis;
            var alpha = Mathf.Atan2(pip.y - camera.y, pip.x - camera.x);
            var beta = Mathf.Atan2(gevraagdeDir.y, gevraagdeDir.x) - (90 * Mathf.Deg2Rad);

            //MATH: https://www.geogebra.org/calculator/bwqwwpax
            var y = alpha + beta;
            var dir = new Vector2(Mathf.Cos(y), Mathf.Sin(y)) + camera;
            var uitkomst = (dir - camera).normalized;

            //ROTATION
            //Make sure the rotation is slower when bouncing
            var jumpRotationMultiplier = _isBouncing ? _jumpRotationModifier * Mathf.Abs(axis.x): 1;

            _rotationPivot.rotation = Quaternion.RotateTowards(
                _rotationPivot.rotation, 
                Quaternion.Euler(new Vector3(0, Mathf.Atan2(uitkomst.x, uitkomst.y) * Mathf.Rad2Deg, 0)), 
                _rotationSpeed * Time.deltaTime * jumpRotationMultiplier);

            //MOVEMENT
            //Makes sure movement only starts when the char is looking at the requested distance
            var forward2d = new Vector2(_rotationPivot.forward.x, _rotationPivot.forward.z);
            //DebugManager.SetText(uitkomst.ToString() + ", " + forward2d.ToString() + "= " + Vector2.Angle(uitkomst, forward2d).ToString());
            var angleBetweenRequestedAndForward = Vector2.Angle(uitkomst, forward2d);
            float angleForceModifier = 1;
            if (angleBetweenRequestedAndForward > 0) // prevents dividing by 0
            {
                angleForceModifier -= angleBetweenRequestedAndForward / 180; //0 when facing away, 1 when facing towards requested angle
                angleForceModifier = Mathf.Pow(angleForceModifier, _facingForceCorrection);
            }
            var force = _rotationPivot.forward * _moveSpeed * Time.deltaTime * angleForceModifier * axis.magnitude;
            _rb.AddForce(force);
        }
        //brake
        if (_groundChecker.IsGrounded)
        {
            var velocity = _rb.linearVelocity;
            velocity.x *= _brakeValue;
            velocity.z *= _brakeValue;
            _rb.linearVelocity = velocity;
        }
    }

    void FixedUpdate()
    {
        //cap horizontalmovement
        var horizontalMovement = _rb.linearVelocity;
        horizontalMovement.y = 0;
        var bounceMovementMultiplier = _isBouncing ? _bounceMovementMultiplier : 1;
        if (horizontalMovement.magnitude > _maxSpeed * bounceMovementMultiplier)
        {
            var cappedHorizontalMovement = horizontalMovement.normalized * _maxSpeed * bounceMovementMultiplier;
            cappedHorizontalMovement.y = _rb.linearVelocity.y;
            _rb.linearVelocity = cappedHorizontalMovement;
        }

        if (!_groundChecker.IsGrounded && !_isHoldingJumpButton)
            _rb.AddForce(Vector3.down * _extraGravity, ForceMode.Acceleration);

        
    }

    public void MoveCamera(Vector2 axis)
    {
    }

    public void JumpStarted()
    {
        _isHoldingJumpButton = true;
        if (_groundChecker.IsGrounded)
        {
            var newVelocity = _rb.linearVelocity;
            newVelocity.y = _jumpVelocity;
            _rb.linearVelocity = newVelocity;
        }
        else
        {
            _isBouncing = true;
            _tail.PointDown = true;
        }
    }

    bool _jumpHasLeftTheGround = false;
    bool _isBouncing = false;
    public void JumpHeld()
    {
        if (!_groundChecker.IsGrounded)
            _jumpHasLeftTheGround = true;
        if (!_jumpHasLeftTheGround)
            return;

        if (_isBouncing)
        {
            _cameraTransform.GetComponent<PlayerCamera>().OverrideCameraAngle(_rotationPivot.eulerAngles.y);
            if (_groundChecker.IsGrounded)
            {
                DoBounce();
            }
        }
    }

    public void JumpReleased()
    {
        _isHoldingJumpButton = false;
        _jumpHasLeftTheGround = false;
        _cameraTransform.GetComponent<PlayerCamera>().StopOverride();
        _isBouncing = false;
        _tail.PointDown = false;
    }

    void DoBounce()
    {
        var newVelocity = _rb.linearVelocity;
        newVelocity.y = Mathf.Max(Mathf.Abs(newVelocity.y), _jumpVelocity);
        _rb.linearVelocity = newVelocity;
        _jumpHasLeftTheGround = false;
    }

}
