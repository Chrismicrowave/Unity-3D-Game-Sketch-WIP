using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    //** attach script to gameobject, attach 'highlight' effect/obejct from child

    [SerializeField] private GameObject _highlight;




    //highlight when player nearby
    private void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Player"))
        {
            _highlight.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider coll)
    {
        if (coll.CompareTag("Player"))
        {
            _highlight.SetActive(false);
        }
    }
}
