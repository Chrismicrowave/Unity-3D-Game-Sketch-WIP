using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SignalVar : ScriptableObject
{
    public List<SignalVarListener> listeners = new List<SignalVarListener>();

    public void Raise()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnSignalRaise();
        }
    }

    public void RegListener(SignalVarListener listener)
    {
        listeners.Add(listener);
    }

    public void DeRegListener(SignalVarListener listener)
    {
        listeners.Remove(listener);
    }
}
