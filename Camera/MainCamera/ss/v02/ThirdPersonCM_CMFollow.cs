using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCM_CMFollow : MonoBehaviour
{
    private Transform _transform;
    private PlayerInput _playerInput;
    private Vector2 _ctxCamInput;
    private Vector2 _ctxZoomInput;

    [SerializeField] private float _currentRotation = 220f; // Current rotation around target
    [SerializeField] private float _rotationSpeed = 2.0f;

    // Start is called before the first frame update
    void Awake()
    {
        _transform = GetComponent<Transform>();
        _playerInput = new PlayerInput();

        _playerInput.CharacterControls.Look1.started += OnLook1Input;
        _playerInput.CharacterControls.Look1.performed += OnLook1Input;
        _playerInput.CharacterControls.Look1.canceled += OnLook1Input;

        _playerInput.CharacterControls.Look2.started += OnLook2Input;
        _playerInput.CharacterControls.Look2.performed += OnLook2Input;
        _playerInput.CharacterControls.Look2.canceled += OnLook2Input;

        _currentRotation += _ctxCamInput.y * _rotationSpeed;
        Quaternion rot = Quaternion.Euler(45.0f, _currentRotation, 0.0f);
        _transform.rotation = rot;
    }

    // Update is called once per frame
    void Update()
    {
        
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
        Debug.Log(_ctxCamInput.x);

        _currentRotation += _ctxCamInput.x * _rotationSpeed;
        Quaternion rot = Quaternion.Euler(45.0f, _currentRotation , 0.0f);
        _transform.rotation = rot;

    }

    private void OnLook2Input(InputAction.CallbackContext context)
    {
        _ctxZoomInput = context.ReadValue<Vector2>();
        Debug.Log(_ctxZoomInput);
        //CamZoom();
    }
}
