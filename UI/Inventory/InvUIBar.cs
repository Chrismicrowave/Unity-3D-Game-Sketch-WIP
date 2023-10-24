using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InvUIBar : MonoBehaviour
{
    [SerializeField] RectTransform Bar;
    [SerializeField] TextMeshProUGUI Num;
    [SerializeField] IntValue IntValue;

    void Start()
    {
        
    }


    void Update()
    {
       
    }

    public void UpdateBar()
    {
        Bar.localScale = new Vector3(IntValue.RuntimeValue / 100f, 1, 1);
        Num.text = IntValue.RuntimeValue.ToString() + " /100";
    }
}
