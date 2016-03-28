using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class EButton : Crab.Event {
    public enum ColorType {
        Red,
        Green,
        Blue
    }
    public ColorType color;

    private new Renderer renderer;

    protected override void OnGameStart(SceneScript scene)
    {
        renderer = GetComponent<Renderer>();
    }

    void Update() {
        if (!Application.isPlaying) {
            UpdateColor();
        }
    }


    void UpdateColor() {
        switch (color) {
            case ColorType.Red:
                renderer.material.SetColor("_Color", Color.red);
                break;
            case ColorType.Green:
                renderer.material.SetColor("_Color", Color.green);
                break;
            case ColorType.Blue:
                renderer.material.SetColor("_Color", Color.blue);
                break;
        }
    }
}
