using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New IntValue", menuName = "ValueSOs/IntValue")]
public class IntValue : ScriptableObject, ISerializationCallbackReceiver
{

    public int initialValue;
    public int RuntimeValue;

    public void OnAfterDeserialize()
    {
        RuntimeValue = initialValue;
    }

    public void OnBeforeSerialize() { }
}
