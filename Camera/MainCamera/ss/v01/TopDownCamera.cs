using UnityEngine;

public class TopDownCamera2 : MonoBehaviour
{
    public Transform target; // The target to follow
    

    public float distance = 0f; // Distance from target
    public float height = 0f; // Height from target
    public float rotationSpeed = 5.0f; // Speed of rotation
    public float zoomSpeed = 1.0f; // Speed of zoom

    public float currentRotation = 220f; // Current rotation around target
    public float currentZoom = 6f; // Current distance from target

    void Start()
    {
        //currentRotation = transform.eulerAngles.y;
        //currentZoom = distance;
   
    }

    void LateUpdate()
    {
        // Follow the target
        transform.position = target.position + new Vector3(0, height, -distance);

        // Rotate the camera when middle mouse button is pressed
        if (Input.GetMouseButton(2))
        {
            currentRotation += Input.GetAxis("Mouse X") * rotationSpeed;
        }

        // Zoom the camera using mouse wheel scroll
        currentZoom -= Input.mouseScrollDelta.y * zoomSpeed;

        // Clamp the zoom distance
        currentZoom = Mathf.Clamp(currentZoom, 2.0f, 20.0f);

        // Set the camera rotation and distance
        Quaternion rotation = Quaternion.Euler(45.0f, currentRotation, 0.0f);
        transform.rotation = rotation;
        transform.position = target.position + rotation * new Vector3(0, height, -currentZoom);
    }
}
