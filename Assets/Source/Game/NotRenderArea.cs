using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider))]
public class NotRenderArea : MonoBehaviour {
    
    public LayerMask layer;

    private HashSet<GameObject> objectsInside = new HashSet<GameObject>();

    void OnTriggerEnter(Collider col)
    {
        if (IsInLayerMask(col.gameObject, layer))
        {
            EnableRenderers(col.gameObject, false);
            objectsInside.Add(col.gameObject);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (objectsInside.Contains(col.gameObject))
        {
            EnableRenderers(col.gameObject, true);
            objectsInside.Remove(col.gameObject);
        }
    }

    public void EnableRenderers(GameObject go, bool enabled)
    {
        MeshRenderer[] renderers = go.GetComponents<MeshRenderer>();
        MeshRenderer[] childrenRenderers = go.GetComponentsInChildren<MeshRenderer>();
        for (int i = 0, len = renderers.Length; i < len; i++)
        {
            renderers[i].enabled = enabled;
        }
        for (int i = 0, len = childrenRenderers.Length; i < len; i++)
        {
            childrenRenderers[i].enabled = enabled;
        }
    }

    public static bool IsInLayerMask(GameObject obj, LayerMask mask)
    {
        return ((mask.value & (1 << obj.layer)) > 0);
    }
}
