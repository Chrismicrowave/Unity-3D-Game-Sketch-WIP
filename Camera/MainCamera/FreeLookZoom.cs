using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class FreeLookZoom : MonoBehaviour
{
    PlayerInput _playerInput;

    CinemachineFreeLook _freeLookCam;

    [SerializeField] float _zoomSenitivity = 1.0f;
    [SerializeField] float _startZoom = 25.0f;
    [SerializeField] float _minZoom = 2.0f;
    [SerializeField] float _maxZoom = 40.0f;
    private float _ctxZoomInput;



    void Awake()
    {
        _playerInput = new PlayerInput();
        _freeLookCam = GetComponent<CinemachineFreeLook>();

        _freeLookCam.m_CommonLens = true;
        _freeLookCam.m_Lens.FieldOfView = _startZoom;

        _playerInput.CharacterControls.Look2.started += OnLook2Input;
        _playerInput.CharacterControls.Look2.performed += OnLook2Input;
        _playerInput.CharacterControls.Look2.canceled += OnLook2Input;
    }

    void OnEnable()
    {
        _playerInput.CharacterControls.Enable();
    }

    void OnDisable()
    {
        _playerInput.CharacterControls.Disable();
    }


    private void Update()
    {
        
    }



    private void OnLook2Input(InputAction.CallbackContext context)
    {
        _ctxZoomInput = context.ReadValue<float>();

        float zoomCheck = 0f;
        zoomCheck = (_freeLookCam.m_Lens.FieldOfView + _ctxZoomInput * _zoomSenitivity);


        if (zoomCheck < _maxZoom && zoomCheck > _minZoom)
        {
            _freeLookCam.m_Lens.FieldOfView = zoomCheck; }

    }


   
}
