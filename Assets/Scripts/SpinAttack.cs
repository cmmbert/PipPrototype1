using UnityEngine;

public class SpinAttack : MonoBehaviour
{
    float _durationLeft;
    public void StartTriggering(float duration)
    {
        gameObject.SetActive(true);
        _durationLeft = duration;
    }

    private void Update()
    {
        _durationLeft -= Time.deltaTime;
        if (_durationLeft < 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit");
        if (other.TryGetComponent<EnemyDeath>(out var enemyDeath))
        {
            Debug.Log("Death");
            enemyDeath.Die();
        }
    }
}
