using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using Crab.Controllers;

[RequireComponent (typeof(Collider))]
public class RotationVolume : MonoBehaviour {
	public bool requiresAction = false;

	void OnTriggerEnter(Collider col)
	{
		PlayerController player = col.GetComponent<PlayerController>();
		if (player) {
			float angleToTarget = Quaternion.LookRotation(player.transform.position - transform.position).eulerAngles.y;
			float yToPlayer = Mathf.Abs(angleToTarget - transform.eulerAngles.y);

			if (yToPlayer < 45)
			{
			}
			else
			{
				
			}
		}
	}


	

#if UNITY_EDITOR
	void OnDrawGizmos () {

		Handles.color = requiresAction? Color.red : Color.blue;
		Handles.ArrowCap(0, transform.position,
						 transform.rotation,
						 1);
		if (!requiresAction) {
			Handles.color = Color.cyan;
			Handles.ArrowCap(0, transform.position,
							 Quaternion.LookRotation(transform.right),
							 1);
		}
	}
#endif
}
