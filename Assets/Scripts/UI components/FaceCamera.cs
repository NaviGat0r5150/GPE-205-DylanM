using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private Transform mainCameraTransform;

    void Start()
    {
        // Find the main camera in the scene
        mainCameraTransform = Camera.main.transform;

        // Ensure the canvas is initially facing the camera
        FaceTowardsCamera();
    }

    void Update()
    {
        // Update the canvas to face the camera every frame
        FaceTowardsCamera();
    }

    void FaceTowardsCamera()
    {
        // Calculate the direction from the canvas to the camera
        Vector3 directionToCamera = mainCameraTransform.position - transform.position;

        // Ensure the canvas is always facing the camera by using LookAt
        transform.rotation = Quaternion.LookRotation(directionToCamera);
    }
}