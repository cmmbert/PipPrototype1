using UnityEngine;

public class LampParticleTarget : MonoBehaviour
{
    [SerializeField] float _bounceHeight = 1;
    [SerializeField] float _bounceSpeed = 0.5f;
    [SerializeField] float _rotationRadius = 2;
    [SerializeField] float _rotationSpeedMultiplier = 2;
    [SerializeField] float _rotationPingPong = 0.25f;
    float _randomizer;
    float _current;

    private void Start()
    {
        _current = Random.value * 200;
        _randomizer = Random.value / 2;
        _bounceSpeed += _randomizer;
        _rotationRadius += _randomizer / 10;
        _rotationSpeedMultiplier += _randomizer;
    }

    void Update()
    {
        _current += Time.deltaTime * _rotationSpeedMultiplier;
        var currentRadius = _rotationRadius + Mathf.PingPong(_current / _rotationSpeedMultiplier * _bounceSpeed, _rotationPingPong);
        transform.localPosition = new Vector3(
            Mathf.Cos(_current) * currentRadius, 
            Mathf.PingPong(_current / _rotationSpeedMultiplier * _bounceSpeed, _bounceHeight) - _bounceHeight/2, 
            Mathf.Sin(_current) * currentRadius);
    }


}
