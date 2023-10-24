using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagCanvas : MonoBehaviour
{
    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    void Start()
    {

    }

    void Update()
    {
        if (PlayerState.IsPlayerInv)
        {
            _anim.Play("Open");
        }
        else
        {
            _anim.Play("Close");
        }
    }
}
