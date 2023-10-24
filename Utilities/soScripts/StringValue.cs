using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New StringValue", menuName = "ValueSOs/StringValue")]
public class StringValue : ScriptableObject, ISerializationCallbackReceiver
{

    public string initialValue;

    public string RuntimeValue;

    public void OnAfterDeserialize()
    {
        RuntimeValue = initialValue;
    }

    public void OnBeforeSerialize() { }
}
