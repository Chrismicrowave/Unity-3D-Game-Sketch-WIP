using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    [SerializeField] private GameObject Green;
    [SerializeField] private GameObject Yellow;
    [SerializeField] private GameObject Red;

    [SerializeField] private int RedTime = 3;
    [SerializeField] private int YellowTime = 1;
    [SerializeField] private int GreenTime = 6;

    public bool IsGreen;
    public bool IsYellow;
    public bool IsRed;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Lights());
        //CurLight(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Lights()
    {
        while(true)
        {
            CurLight(3);
            yield return new WaitForSeconds(GreenTime);

            CurLight(2);
            yield return new WaitForSeconds(YellowTime);

            CurLight(1);
            yield return new WaitForSeconds(RedTime);
        }

    }

    private void CurLight(int i)
    {
        Red.SetActive(false);
        Yellow.SetActive(false);
        Green.SetActive(false);

        IsRed = false;
        IsYellow = false;
        IsGreen = false;

        if (i == 1)
        {
            Red.SetActive(true);
            IsRed = true;
        }

        if (i == 2)
        {
            Yellow.SetActive(true);
            IsYellow = true;
        }

        if (i == 3)
        {
            Green.SetActive(true);
            IsGreen = true;
        }
    }

}
