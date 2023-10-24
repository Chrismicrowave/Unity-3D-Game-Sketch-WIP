using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnim : MonoBehaviour
{
    Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.Play("Idle");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
