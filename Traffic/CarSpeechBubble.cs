using UnityEngine;

public class CarSpeechBubble : MonoBehaviour
{
    private Camera mainCamera;
    private RectTransform canvasRectTransform;
    private Transform t;

    void Start()
    {
        // Get the main camera in the scene
        mainCamera = Camera.main;

        // Get the RectTransform component of the canvas object
        canvasRectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        t = mainCamera.transform;
        t.rotation.Set(t.rotation.x, t.rotation.y+180, t.rotation.z, t.rotation.w);

        // Make the canvas object face the camera
        canvasRectTransform.LookAt(mainCamera.transform);
        canvasRectTransform.rotation = t.rotation;
        
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(mainCamera.transform.position), 10f * Time.deltaTime);
    }
}