using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSat : MonoBehaviour
{
    public IntValue PlayerHunger;
    public IntValue PlayerThirst;



    void Start()
    {
        PlayerHunger.RuntimeValue = 50;
        PlayerThirst.RuntimeValue = 50;
        
    }

    void Update()
    {
        
    }
}
