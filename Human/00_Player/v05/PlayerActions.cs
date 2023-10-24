using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public static PlayerActions Instance;

    private void Awake()
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
        
    }


    public void Actions()
    {
        //Jump
        if (PlayerJumpHandler.Instance.IsReadyJump())
        {PlayerJumpHandler.Instance.Jump();}

    }

}
