using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Vector2Value", menuName = "ValueSOs/Vector2Value")]
public class VectorValue : ScriptableObject, ISerializationCallbackReceiver
{

    [Header("Value running in game")]
    public Vector2 initialValue;
    [Header("Value by default when starting")]
    public Vector2 defaultValue;

    public void OnAfterDeserialize() { initialValue = defaultValue; }

    public void OnBeforeSerialize() { }

}