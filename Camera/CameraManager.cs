using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    public CinemachineFreeLook StartCam;
    private CinemachineFreeLook _curCam;
    private CinemachineVirtualCamera _curCamVirtual;


    [Header("Freelook CMs")]
    public CinemachineFreeLook TopDown;
    public CinemachineFreeLook CloseUp;
    public CinemachineFreeLook BridgeThrow;

    [Header("Add Freelook to list")]
    public CinemachineFreeLook[] cameras;



    
    [Header("Virtual CMs")]
    public CinemachineVirtualCamera BridgeThrowV; //not used

    [Header("Add Vcam to list")]
    public CinemachineVirtualCamera[] camerasVirtual;




    void Awake()
    {
        if (Instance == null)
        { Instance = this; }
        else
        {
            Destroy(gameObject);
            return;
        }

    }

    void Start()
    {
        _curCam = StartCam;

       //freelook cam set all to 10 except curcam
        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i] == _curCam)
            {
                cameras[i].Priority = 20;
            }
            else
            {
                cameras[i].Priority = 10;
            }
        }

        // Vcam set all to 10 except curcam
        for (int i = 0; i < camerasVirtual.Length; i++)
        {
            if (camerasVirtual[i] == _curCam)
            {
                camerasVirtual[i].Priority = 20;
            }
            else
            {
                camerasVirtual[i].Priority = 10;
            }
        }
    }

    void Update()
    {
        
    }

    public void SwitchCamera(CinemachineFreeLook newCam = null, CinemachineVirtualCamera newCamV = null)
    {
        _curCam = newCam;
        _curCamVirtual = newCamV;

        //freelook cam
        if (_curCam != null)
        {
            _curCam.Priority = 20;

            for (int i = 0; i < cameras.Length; i++)
            {
                if (cameras[i] != _curCam)
                {
                    cameras[i].Priority = 10;
                }
            }
        }
        else
        {
            for (int i = 0; i < cameras.Length; i++)
            {
                cameras[i].Priority = 10;
            }
        }

        //virtual cam
        if (_curCamVirtual != null)
        {
            _curCamVirtual.Priority = 20;

            for (int i = 0; i < camerasVirtual.Length; i++)
            {
                if (camerasVirtual[i] != _curCamVirtual)
                {
                    camerasVirtual[i].Priority = 10;
                }
            }
        }
        else
        {
            for (int i = 0; i < camerasVirtual.Length; i++)
            {
                camerasVirtual[i].Priority = 10;
                
            }
        }


    }

}
