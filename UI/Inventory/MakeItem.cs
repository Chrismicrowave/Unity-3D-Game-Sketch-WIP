using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeItem : MonoBehaviour
{
    private Dictionary<string, string> recipes = new Dictionary<string, string>
    {
        {"LighterNewsPaper","Fireball"},
        {"","" }
    };

    [Header("Vars")]
    public StringValue MakeItemA;
    public StringValue MakeItemB;
    public StringValue MakeProduct;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
