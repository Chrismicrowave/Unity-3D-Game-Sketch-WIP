using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New BoolValue", menuName = "ValueSOs/BoolValue")]
public class BoolValue : ScriptableObject, ISerializationCallbackReceiver
{
    public bool initialValue;
    public bool RuntimeValue;

    public void OnAfterDeserialize()
    {
        RuntimeValue = initialValue;
    }

    public void OnBeforeSerialize() { }
}
