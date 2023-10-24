using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeThrowZoneTrigger : MonoBehaviour
{
    [SerializeField] public StringValue CurWeapon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent(out Player player)
            && CurWeapon.RuntimeValue == "WaterBottle")
        {
            CameraManager.Instance.SwitchCamera(CameraManager.Instance.BridgeThrow);
            //CameraManager.Instance.SwitchCamera(null,CameraManager.Instance.BridgeThrowV);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            CameraManager.Instance.SwitchCamera(CameraManager.Instance.TopDown);
        }
    }
}
