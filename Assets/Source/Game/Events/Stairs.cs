using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class Stairs : MonoBehaviour {

    public bool active = false;
    public float speed = 3;
    public float offset = 0;
    public float stairLength = 0.5f;
    public List<Transform> stairs = new List<Transform>();

	void Start () {
        for (int i = 0; i < stairs.Count; i++)
        {
            Transform stair = stairs[i];
            Vector3 position = stair.localPosition;
            position.x = (active ? offset + i * stairLength : 0);
            position.z = 0;
            stair.localPosition = position;
        }
	}

    void Update()
    {
        if (Application.isPlaying)
        {
            for (int i = 0; i < stairs.Count; i++)
            {
                Transform stair = stairs[i];
                Vector3 position = stair.localPosition;
                position.x = (active ? offset + i * stairLength : 0);
                position.z = 0;
                stair.localPosition = Vector3.Lerp(stair.localPosition, position, Time.deltaTime * speed);
            }
        }
        else {
            for (int i = 0; i < stairs.Count; i++)
            {
                Transform stair = stairs[i];
                Vector3 position = stair.localPosition;
                position.x = (active ? offset + i * stairLength : 0);
                position.z = 0;
                stair.localPosition = position;
            }
        }
    }

    public void Activate() {

    }

    public void Deactivate() {

    }
}
