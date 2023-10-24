using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePanel : MonoBehaviour
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
        if (PlayerState.IsPlayerInDialogue)
        {
            _anim.Play("Open");
        }
        else
        {
            _anim.Play("Close");
        }
    }
}
