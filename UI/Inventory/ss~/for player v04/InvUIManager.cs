using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InvUIManager : MonoBehaviour
{
    public static InvUIManager Instance;

    [Header("Detail Info Slot")]
    [SerializeField] TextMeshProUGUI DetailSlotDescr;
    [SerializeField] Image DetailSlotSprite;
    [SerializeField] TextMeshProUGUI DetailSlotName;



    private void Awake()
    {
        if (Instance == null)
        { Instance = this; }
        else
        {
            Destroy(gameObject);
            return;
        }
        //DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupDetailSlot(string Name, Sprite Sprite, string Descr)
    {
        DetailSlotName.text = Name;
        DetailSlotSprite.sprite = Sprite;
        DetailSlotDescr.text = Descr;
    }

    
}
