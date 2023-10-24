using UnityEngine;
using UnityEngine.InputSystem;

public class TopDownCamera : MonoBehaviour
{
    public Transform target; // The target to follow

    private PlayerInput _playerInput;
    private Vector2 _ctxCamInput;
    private float _ctxZoomInput;


    public float _distance = 0f; // Distance from target
    public float _height = 0.5f; // Height from target
    public float _rotationSpeed = 2.0f; // Speed of rotation
    public float _zoomSpeed = 1f; // Speed of zoom

    public float _currentRotation = 220f; // Current rotation around target
    public float _currentZoom = 10f; // Current distance from target


    private void Awake()
    {
        _playerInput = new PlayerInput();

        _playerInput.CharacterControls.Look1.started += OnLook1Input;
        _playerInput.CharacterControls.Look1.performed += OnLook1Input;
        _playerInput.CharacterControls.Look1.canceled += OnLook1Input;

        _playerInput.CharacterControls.Look2.started += OnLook2Input;
        _playerInput.CharacterControls.Look2.performed += OnLook2Input;
        _playerInput.CharacterControls.Look2.canceled += OnLook2Input;

    }
    void Start()
    {
        

    }

    void LateUpdate()
    {
        SpinAndZoom();
    }

    void OnEnable()
    {
        _playerInput.CharacterControls.Enable();
    }

    void OnDisable()
    {
        _playerInput.CharacterControls.Disable();
    }


    private void OnLook1Input(InputAction.CallbackContext context)
    {
        _ctxCamInput = context.ReadValue<Vector2>();
    }

    private void OnLook2Input(InputAction.CallbackContext context)
    {
        _ctxZoomInput = context.ReadValue<float>();
        
    }

    private void SpinAndZoom()
    {
        transform.position = target.position + new Vector3(0, _height, -_distance);

        _currentRotation += _ctxCamInput.x * _rotationSpeed;

        // Zoom the camera using mouse wheel scroll
        _currentZoom -= _ctxZoomInput * _zoomSpeed;

        // Clamp the zoom distance
        _currentZoom = Mathf.Clamp(_currentZoom, 2.0f, 20.0f);

        // Set the camera rotation and distance
        Quaternion rotation = Quaternion.Euler(45.0f, _currentRotation, 0.0f);
        transform.rotation = rotation;
        transform.position = target.position + rotation * new Vector3(0, _height, -_currentZoom);
    }
}
