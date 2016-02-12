using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    [Header("Position")]
    public float size = 3;
    public float height = 0;
    public float distance = 10;
    [Header("Speeds")]
    public float rotate = 150;
    public float zoom = 3;

    private float angle;
    [System.NonSerialized]
    public Camera cam;

    void Start() {
        angle = (int)GameStats.Get.rotationState;
        cam = GetComponentInChildren<Camera>();
        cam.orthographicSize = size;
    }

    void LateUpdate()
    {
        if (!cam)
            return;

        if (cam.orthographicSize != size)
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, size, Time.deltaTime * zoom);


        angle = IncrAngle(angle, (int)GameStats.Get.rotationState, Time.deltaTime * rotate);
        transform.rotation = Quaternion.Euler(0, angle, 0);

        cam.transform.localPosition = new Vector3(0, height, distance);
        cam.transform.LookAt(transform);
    }

    float IncrAngle(float a, float b, float t, float errorRange = 5f) {
        float angleDistance = Mathf.DeltaAngle(a, b);

        float dir = Mathf.Clamp(angleDistance, -1, 1);
        
        if (Mathf.Abs(angleDistance) < errorRange)
            return b;
        else
            return a + dir * t;
    }
}
