using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class EPressure : MonoBehaviour {
    public UnityEvent onEnter;
    public UnityEvent onExit;

    void OnTriggerEnter()
    {
        onEnter.Invoke();
    }

    void OnTriggerExit() {
        onExit.Invoke();
    }
}
