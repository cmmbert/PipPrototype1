using UnityEngine;

public class Tail : MonoBehaviour
{
    public bool PointDown = false;

    [SerializeField] float _rotationSpeed = 180;
    float endRotation = -160;

    void Update()
    {
        Vector3 requestedRotation = new Vector3(0, 0, 0);
        if (PointDown)
            requestedRotation.x = endRotation;
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(requestedRotation), _rotationSpeed * Time.deltaTime);
    }
}
