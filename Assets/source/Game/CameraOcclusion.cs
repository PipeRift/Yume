using UnityEngine;
using System.Collections.Generic;
using Crab.Utils;
using Crab.Components;

public class CameraOcclusion : MonoBehaviour {

	public float frequency = 3;

	private Delay delay;
	private HashSet<DissolveEffect> dissolvedMeshes = new HashSet<DissolveEffect>();
    private Transform cam;

	void Start() {
		delay = new Delay ((int)(1000/frequency));
        cam = GetComponent<CameraController>().cam.transform;
	}

	void Update()
    {
        if (delay.Over ()) {
			delay.Start();
            //Wait for frequency
            
            foreach (DissolveEffect dissolve in dissolvedMeshes) {
				dissolve.Constitute();
			}
			dissolvedMeshes.Clear();
            
            CMovement playerMove = Cache.Get.player.GetComponent<CMovement>();
            if (!playerMove.IsInside())
                return;

            RaycastHit[] hits;
			hits = Physics.RaycastAll(cam.position, playerMove.transform.position - cam.position, Vector3.Distance(playerMove.transform.position, cam.position));
            
			for (int i = 0; i < hits.Length; i++) {
				RaycastHit hit = hits[i];
				DissolveEffect dissolveMesh = hit.transform.GetComponent<DissolveEffect>();
				
				if (dissolveMesh) {
					dissolveMesh.Dissolve();
					dissolvedMeshes.Add (dissolveMesh);
				}
			}
		}
    }


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
