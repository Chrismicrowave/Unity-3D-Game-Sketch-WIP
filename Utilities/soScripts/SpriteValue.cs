using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SpriteValue", menuName = "ValueSOs/SpriteValue")]
public class SpriteValue : ScriptableObject, ISerializationCallbackReceiver
{

    public Sprite initialValue;
    public Sprite RuntimeValue;

    public void OnAfterDeserialize()
    {
        RuntimeValue = initialValue;
    }

    public void OnBeforeSerialize() { }
}
