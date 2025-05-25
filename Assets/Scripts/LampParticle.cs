using UnityEngine;

public class LampParticle : MonoBehaviour
{
    public Transform Target;
    public Vector3 CurrentTarget;
    [SerializeField] float _moveSpeed = 5;

    Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Target != null) CurrentTarget = Target.position;
        var moveVector = CurrentTarget - transform.position;
        _rb.linearVelocity = moveVector * _moveSpeed;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(CurrentTarget, 0.5f);

    }
}
