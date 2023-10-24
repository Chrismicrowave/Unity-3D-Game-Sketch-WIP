using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerWeaponControl : MonoBehaviour
{
    [SerializeField] public StringValue CurWeapon;
    [SerializeField] GameObject Weapons;
    [SerializeField] GameObject RightHand;

    [Header("Prefabs")]
    [SerializeField] GameObject BubbleTea;
    [SerializeField] GameObject WaterBottle;


    // Start is called before the first frame update
    void Awake()
    {
        foreach (Transform weapon in Weapons.transform)
        {
            //turn all off
            weapon.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CurWeaponSelect()
    {
         foreach (Transform weapon in Weapons.transform)
        {
            //turn all off
            weapon.gameObject.SetActive(false);

            //if matched cur weapon turn on
            //make sure there is gameobject with same name under Weapons gameobject
            if(weapon.gameObject.name == CurWeapon.RuntimeValue)
            {
                weapon.gameObject.SetActive(true);
                InstantiateModel(CurWeapon.RuntimeValue);
            }
        }

    }

    void InstantiateModel(string WeaponName)
    {
        GameObject prefab = null;

        switch (WeaponName)
        {
            case "BubbleTea":
                prefab = BubbleTea;
                break;

            case "WaterBottle":
                prefab = WaterBottle;
                break;
        }

        if (prefab != null)
        {
            Vector3 Ro = new Vector3(-90f, 0f, 0f);
            Quaternion Rotation = Quaternion.Euler(Ro);

            GameObject weapon = Instantiate(prefab, RightHand.transform.position, Rotation);
            
            weapon.transform.SetParent(RightHand.transform);
            weapon.transform.localRotation = Quaternion.identity;

            //disable coll
            if (weapon.TryGetComponent(out MeshCollider coll)) { coll.enabled = false; }
            if (weapon.TryGetComponent(out CapsuleCollider coll2)) { coll2.enabled = false; }

            //disable Highlight
            foreach (Transform transform in weapon.transform)
            {
                if (transform.gameObject.name == "Highlight")
                {
                    Highlight hl = weapon.GetComponent<Highlight>();
                    Destroy(hl);

                    Destroy(transform.gameObject);
                }
            }
        }
    }
}
