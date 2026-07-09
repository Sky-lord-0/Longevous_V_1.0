using UnityEngine;

public class CameraFollowClamp : MonoBehaviour
{
    public Transform target;
    public BoxCollider2D boundsCollider;
    public float smoothTime = 0.08f;

    private Vector3 velocity;

    void LateUpdate()
    {
        if (target == null || boundsCollider == null) return;

        Camera cam = Camera.main;
        if (cam == null) return;

        // posição desejada (segue o player)
        Vector3 desired = new Vector3(target.position.x, target.position.y, transform.position.z);

        // suaviza
        Vector3 smoothed = Vector3.SmoothDamp(transform.position, desired, ref velocity, smoothTime);

        // clamp dentro dos bounds
        Bounds b = boundsCollider.bounds;

        float camHalfHeight = cam.orthographicSize;
        float camHalfWidth = camHalfHeight * cam.aspect;

        float minX = b.min.x + camHalfWidth;
        float maxX = b.max.x - camHalfWidth;
        float minY = b.min.y + camHalfHeight;
        float maxY = b.max.y - camHalfHeight;

        // se o bounds for menor que a câmera, evita NaN
        float clampedX = Mathf.Clamp(smoothed.x, minX, maxX);
        float clampedY = Mathf.Clamp(smoothed.y, minY, maxY);

        transform.position = new Vector3(clampedX, clampedY, smoothed.z);
    }
}