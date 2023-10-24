using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ListValue", menuName = "ValueSOs/ListValue")]
public class ListValue: ScriptableObject, ISerializationCallbackReceiver
{
    public List<string> initialValue = new List<string>();

    public List<string> RuntimeValue = new List<string>();

    public void OnAfterDeserialize()
    {
        RuntimeValue = initialValue;
    }

    public void OnBeforeSerialize() { }

}