using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class CanvasSetup : MonoBehaviour
{
    private Canvas canvas;

    void Awake()
    {
        // Get the Canvas component attached to this GameObject
        canvas = GetComponent<Canvas>();

        // Find the Main Camera in the scene
        Camera mainCamera = Camera.main;

        if (mainCamera != null)
        {
            // Set the Main Camera as the event camera for the Canvas
            canvas.worldCamera = mainCamera;
        }
        else
        {
            Debug.LogError("No Main Camera found in the scene. Please ensure there is a Camera tagged as 'MainCamera'.");
        }
    }
}
