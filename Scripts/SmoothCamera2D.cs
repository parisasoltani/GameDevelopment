using UnityEngine;

public class SmoothCamera2D : MonoBehaviour
{
    public float dampTime = 0.6f; // Smaller value for quicker following
    public Transform target;
    public float xOffset = 0;
    public float yOffset = 0;

    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        if (target)
        {
            Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
            Vector3 delta = target.position + new Vector3(xOffset, yOffset, 0) - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }
    }
}
