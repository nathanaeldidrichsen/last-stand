using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    // Define the boundaries for the camera
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        // Get the camera's current position
        Vector3 pos = transform.position;

        // Clamp the camera's position to ensure it stays within the bounds
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        // Set the camera's position to the clamped value
        transform.position = pos;
    }
}
