using UnityEngine;
using System.Collections;

public class CameraOcclusion : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        DissolveEffect dissolve = other.GetComponent<DissolveEffect>();
        if (dissolve) {
            dissolve.Dissolve();
        }
    }

    void OnTriggerExit(Collider other)
    {
        DissolveEffect dissolve = other.GetComponent<DissolveEffect>();
        if (dissolve)
        {
            dissolve.Constitute();
        }
    }
}
