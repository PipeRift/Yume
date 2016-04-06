using UnityEngine;
using System.Collections;

[ExecuteInEditMode()]
public class Lothe : MonoBehaviour
{
    public Transform head;
    public float targetYawRotation;
    public float speed = 3;

    public void Rotate()
    {
        targetYawRotation -= 90;
    }

    void Update()
    {
        SetYaw(targetYawRotation, Application.isPlaying);
    }

    void SetYaw(float yaw, bool smooth)
    {
        if (!head) return;

        Vector3 rotation = head.localEulerAngles;
        if (smooth)
        {
            rotation.y = Mathf.LerpAngle(rotation.y, yaw, Time.deltaTime * speed);
        }
        else {
            rotation.y = yaw;
        }
        head.localEulerAngles = rotation;
    }

}
