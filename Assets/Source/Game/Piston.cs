using UnityEngine;
using System.Collections;

public class Piston : MonoBehaviour
{
    public Color desiredColor = Color.white;
    public float speed = 1;

    private new Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        Color actualColor = renderer.material.GetColor("_Color");

        if (actualColor != desiredColor)
        {
            actualColor = Color.Lerp(actualColor, desiredColor, Time.deltaTime + speed);
            renderer.material.SetColor("_Color", actualColor);
        }
    }
}
