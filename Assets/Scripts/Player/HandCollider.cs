using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HandCollider : MonoBehaviour
{
    UnityEvent<Collider> triggerEnterEvent;

    public void RegisterFunc(Action<Collider> unityEvent)
    {
        triggerEnterEvent = new UnityEvent<Collider>();
        triggerEnterEvent.AddListener(unityEvent.Invoke);
    }

    private void OnTriggerEnter(Collider other)
    {
        triggerEnterEvent.Invoke(other);
    }
}
