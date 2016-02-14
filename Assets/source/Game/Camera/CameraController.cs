using UnityEngine;
using System.Collections;
using Crab.Controllers;

public class CameraController : MonoBehaviour
{
    [Header("Position")]
    public float size = 3;
    public float height = 0;
    public float distance = 10;
    [Space()]
    public bool usePlayerY = false;
    public float yOffset = 0;
    [Header("Speeds")]
    public float rotate = 150;
    public float zoom = 3;
    public float move = 3;

    private float angle;
    public Camera cam {
        get { return _cam? _cam : _cam = GetComponentInChildren<Camera>(); }
    }
    private Camera _cam;

    void Start() {
        angle = (int)GameStats.Get.rotationState;
        cam.orthographicSize = size;

        //Set height
        PlayerController player = Cache.Get.player;
        Vector3 position = transform.position;
        position.y = player.transform.position.y;
        transform.position = position;
    }

    void LateUpdate()
    {
        if (!cam)
            return;

        if (cam.orthographicSize != size)
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, size, Time.deltaTime * zoom);

        //Update Height
        PlayerController player = Cache.Get.player;
        if (player && usePlayerY)
        {
            Vector3 position = transform.position;
            position.y = Incr(position.y, player.transform.position.y + yOffset, Time.deltaTime * move, 0.05f);
            transform.position = position;
        }



        angle = IncrAngle(angle, (int)GameStats.Get.rotationState, Time.deltaTime * rotate);
        transform.rotation = Quaternion.Euler(0, angle, 0);

        cam.transform.localPosition = new Vector3(0, height, distance);
        cam.transform.LookAt(transform);
    }



    float Incr(float a, float b, float t, float errorRange = 0.5f)
    {
        float distance = b-a;

        float dir = Mathf.Clamp(distance, -1, 1);

        if (Mathf.Abs(distance) < errorRange)
            return b;
        else
            return a + dir * t;
    }

    float IncrAngle(float a, float b, float t, float errorRange = 5f) {
        float distance = Mathf.DeltaAngle(a, b);

        float dir = Mathf.Clamp(distance, -1, 1);
        
        if (Mathf.Abs(distance) < errorRange)
            return b;
        else
            return a + dir * t;
    }
}
