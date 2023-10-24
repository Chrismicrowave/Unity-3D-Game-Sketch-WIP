using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SignalVarListener : MonoBehaviour
{
    public SignalVar signalVar;
    public UnityEvent signalVarEvent;

    public void OnSignalRaise()
    {
        signalVarEvent.Invoke();
    }

    private void OnEnable()
    {
        signalVar.RegListener(this);
    }

    private void OnDisable()
    {
        signalVar.DeRegListener(this);
    }
}
