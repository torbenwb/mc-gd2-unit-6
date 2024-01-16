using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Entity : MonoBehaviour
{
    public bool active = false;
    public UnityEvent onActivate = new UnityEvent();
    public UnityEvent onDeactivate = new UnityEvent();

    public void ToggleActive()
    {
        if (active) Deactivate();
        else Activate();
    }

    public void Activate()
    {
        active = true;
        onActivate.Invoke();
    }

    public void Deactivate()
    {
        active = false;
        onDeactivate.Invoke();
    }
}
