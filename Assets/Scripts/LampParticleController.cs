using UnityEngine;

public class LampParticleController : MonoBehaviour
{
    [Range(1,10)]
    [SerializeField] int _amount = 3;
    [SerializeField] Transform _targetParent;
    [SerializeField] Transform _particleParent;
    [SerializeField] GameObject _particlePrefab;
    [SerializeField] GameObject _targetPrefab;

    private void Start()
    {
        for (int i = 0; i < _amount; i++)
        {
            var target = Instantiate(_targetPrefab, _targetParent);
            var particle = Instantiate(_particlePrefab, _particleParent);
            particle.GetComponent<LampParticle>().Target = target.transform;
        }
    }
}
