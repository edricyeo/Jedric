using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothFactor;
    private float originalY;
    private void Start()
    {
        originalY = transform.position.y;
    }

    private void FixedUpdate()
    {
        transform.position = target.position + offset;
        transform.position = new Vector3(target.position.x, originalY, 0) + offset;
    }

    //private void Follow()
    //{
    //    Vector3 targetPos = target.position + offset;
    //    Vector3 smoothPos = Vector3.Lerp(transform.position, targetPos, smoothFactor * Time.fixedDeltaTime);
    //    transform.position = smoothPos;
    //}
}
