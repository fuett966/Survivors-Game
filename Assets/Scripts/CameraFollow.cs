using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float followSpeed = 2f; 

    [SerializeField] private Vector2 minBounds;
    [SerializeField] private Vector2 maxBounds;

    private void LateUpdate()
    {
        Vector3 targetPosition = target.position;

        targetPosition.x = Mathf.Clamp(targetPosition.x, minBounds.x, maxBounds.x);
        targetPosition.z = Mathf.Clamp(targetPosition.z, minBounds.y, maxBounds.y);

        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        transform.position = new Vector3(smoothPosition.x, transform.position.y, smoothPosition.z);
    }
}
