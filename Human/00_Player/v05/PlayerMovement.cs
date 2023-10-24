using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;
    public CharacterController _charController {get; private set;}

    public static Vector3 CurMove;
    

    private Vector3 _camRelativeMove;

    [SerializeField] private float _runSpeed = 3.0f;
    [SerializeField] private float _rotFactorPerFrame = 15.0f;
    [SerializeField] private float _walkSpeed = 1.0f;

    
    private void Awake()
    {
        if (Instance == null)
        { Instance = this; }
        else
        {
            Destroy(gameObject);
            return;
        }

        _charController = GetComponent<CharacterController>();
    }

    void Start()
    {
        _charController.Move(_camRelativeMove * Time.deltaTime);
    }

    
    public void Move()
    {
        _camRelativeMove = ConvertToCameraSpace(CurMove);
        _charController.Move(_camRelativeMove * Time.deltaTime);
    }

    //Speed
    public void SpeedHandle()
    {

        if (!PlayerInputManager.IsWalkPressed && PlayerInputManager.IsMovePressed)
        {
            CurMove.x = PlayerInputManager.CurMoveInput.x * _runSpeed;
            CurMove.z = PlayerInputManager.CurMoveInput.y * _runSpeed;
        }
        else if (PlayerInputManager.IsWalkPressed && PlayerInputManager.IsMovePressed)
        {
            CurMove.x = PlayerInputManager.CurMoveInput.x * _walkSpeed;
            CurMove.z = PlayerInputManager.CurMoveInput.y * _walkSpeed;
        }
        else
        {
            CurMove.x = 0;
            CurMove.z = 0;
        }

    }

    //roation
    public void RotHandle()
    {

        Vector3 posLookAt;

        posLookAt.x = _camRelativeMove.x;
        posLookAt.y = 0.0f;
        posLookAt.z = _camRelativeMove.z;



        Quaternion curRot = transform.rotation;

        if (PlayerInputManager.IsMovePressed && posLookAt != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(posLookAt);
            transform.rotation = Quaternion.Slerp(curRot, targetRot, _rotFactorPerFrame * Time.deltaTime);
        }

    }

    //move relative to camera
    private Vector3 ConvertToCameraSpace(Vector3 vectorToRotate)
    {
        float curY = vectorToRotate.y;

        Vector3 camF = Camera.main.transform.forward;
        Vector3 camR = Camera.main.transform.right;

        camF.y = 0;
        camR.y = 0;

        camF = camF.normalized;
        camR = camR.normalized;

        Vector3 camFZ = vectorToRotate.z * camF;
        Vector3 camRX = vectorToRotate.x * camR;

        Vector3 camFinalVector = camFZ + camRX;
        camFinalVector.y = curY;
        return camFinalVector;
    }

    public bool IsGrounded()
    {
        return _charController.isGrounded;
    }
}
