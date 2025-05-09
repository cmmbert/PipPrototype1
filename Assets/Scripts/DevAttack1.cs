using UnityEngine;

public class DevAttack1 : MonoBehaviour
{
    [HideInInspector]
    public bool IsAttacking;

    [SerializeField] float _spinSpeed = 360;
    float _degreesLeftToSpin;

    [SerializeField] SpinAttack _spinAttack;

    public void Attack()
    {
        if (IsAttacking) return;
        _spinAttack.StartTriggering(360 / _spinSpeed);
        IsAttacking = true;
        _degreesLeftToSpin = 360;
    }

    private void Update()
    {
        if (!IsAttacking) return;
        var rotation = _spinSpeed * Time.deltaTime;
        var currentRotation = transform.localRotation.eulerAngles.y;
        if (currentRotation + rotation > 360)
        {
            rotation = 360 - currentRotation;
            _degreesLeftToSpin = 0;
            IsAttacking = false;
        }
        else 
        {
            _degreesLeftToSpin -= rotation;
        }
        transform.Rotate(Vector3.up * rotation);
    }
}
