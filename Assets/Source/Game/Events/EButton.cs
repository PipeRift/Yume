using UnityEngine;
using UnityEngine.Events;

[ExecuteInEditMode]
public class EButton : Crab.Event {
    public enum ColorType {
        White,
        Red,
        Green,
        Blue,
        Black
    }

    public static Color GetColor(ColorType color)
    {
        switch (color)
        {
            case ColorType.White:
                return Color.white;
            case ColorType.Red:
                return Color.red;
            case ColorType.Green:
                return Color.green;
            case ColorType.Blue:
                return Color.blue;
            case ColorType.Black:
                return Color.black;
        }
        return default(Color);
    }


    public ColorType color;
    public ButtonClicked OnClicked;

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

    protected override void JustStarted()
    {
        OnClicked.Invoke(color);
    }

    void UpdateColor() {
        renderer.material.SetColor("_Color", GetColor(color));
    }


    [System.Serializable]
    public class ButtonClicked : UnityEvent<ColorType> { };
}
