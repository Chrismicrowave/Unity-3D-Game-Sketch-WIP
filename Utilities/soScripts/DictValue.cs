using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DictValue", menuName = "ValueSOs/DictValue")]
public class DictValue : ScriptableObject, ISerializationCallbackReceiver
{

    public Dictionary<string, string> initialValue = new Dictionary<string, string>() { };

    public Dictionary<string, string> RuntimeValue = new Dictionary<string, string>() { };

    public void OnAfterDeserialize()
    {
        RuntimeValue = initialValue;
    }

    public void OnBeforeSerialize() { }
}
