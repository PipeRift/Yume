using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class Stairs : MonoBehaviour {

    [SerializeField]
    private bool m_active = false;
    public float speed = 3;
    public float offset = 0;
    public float stairLength = 0.5f;
    public List<Transform> stairs = new List<Transform>();
    public NavMeshObstacle obstacle;

    public bool active
    {
        get {
            return m_active;
        }

        set {
            m_active = value;
            if (obstacle)
                obstacle.enabled = !m_active;
        }
    }

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
                if (obstacle)
                    obstacle.enabled = !m_active;
            }
        }
    }

    public void Activate() {
        active = true;
    }

    public void Deactivate()
    {
        active = false;
    }
}
