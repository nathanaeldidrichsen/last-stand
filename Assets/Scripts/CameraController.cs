using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 5f;  // Speed at which the camera moves
    public float zoomSpeed = 10f; // Speed at which the camera zooms
    public float minZoom = 5f;    // Minimum zoom level
    public float maxZoom = 20f;   // Maximum zoom level
    public float zoomSmoothTime = 0.2f; // Smooth time for zooming

    private Camera cam;
    private float targetZoom;
    private float zoomVelocity;

    void Start()
    {
        cam = GetComponent<Camera>();
        targetZoom = cam.orthographicSize;
    }

    void Update()
    {
        // Move the camera with arrow keys
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 newPosition = transform.position + new Vector3(moveHorizontal, moveVertical, 0) * moveSpeed * Time.deltaTime;
        transform.position = newPosition;

        // Zoom the camera with the mouse scroll wheel
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0f)
        {
            targetZoom -= scrollInput * zoomSpeed;
            targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
        }

        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, targetZoom, ref zoomVelocity, zoomSmoothTime);
    }
}
