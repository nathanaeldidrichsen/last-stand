using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 5f;  // Speed at which the camera moves

    void Update()
    {
        // Get input from arrow keys
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate new camera position
        Vector3 newPosition = transform.position + new Vector3(moveHorizontal, moveVertical, 0) * moveSpeed * Time.deltaTime;

        // Set the camera's position to the new position
        transform.position = newPosition;
    }
}
