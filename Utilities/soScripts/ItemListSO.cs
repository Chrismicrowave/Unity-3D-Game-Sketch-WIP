using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ItemListSO", menuName = "ValueSOs/ItemListSO")]
public class ItemListSO: ScriptableObject, ISerializationCallbackReceiver
{
    public List<Item> initialValue = new List<Item>();

    public List<Item> RuntimeValue = new List<Item>();

    public void OnAfterDeserialize()
    {
        RuntimeValue = initialValue;
    }

    public void OnBeforeSerialize() { }

}