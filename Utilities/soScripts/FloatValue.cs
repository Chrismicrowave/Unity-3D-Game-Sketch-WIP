using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New FloatValue", menuName = "ValueSOs/FloatValue")]
public class FloatValue : ScriptableObject, ISerializationCallbackReceiver
{

    public float initialValue;
    public float RuntimeValue;

    public void OnAfterDeserialize()
    {
        RuntimeValue = initialValue;
    }

    public void OnBeforeSerialize() { }
}
