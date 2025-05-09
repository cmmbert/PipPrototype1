using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] float _yeet = 50;
    public void Die()
    {
        var rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.AddForce(Vector3.up * _yeet);
        rb.AddTorque(50, 0, 0);
    }
}
