using UnityEngine;
using System.Collections.Generic;

public class cubepuzzle : MonoBehaviour
{
    public enum Phase
    {
        FIRST,
        SECOND
    }

    public EButton redButton;
    public EButton greenButton;
    public EButton blueButton;

    public Color color = Color.yellow;

    public List<Piston> first_pistons = new List<Piston>();
    public List<Piston> second_pistons = new List<Piston>();

    [System.NonSerialized]
    public Phase phase;

    [System.NonSerialized]
    public int clickCount = 0;

    void Start()
    {
        if (redButton)
            redButton.OnClicked.AddListener(ButtonPushed);
        if (greenButton)
            greenButton.OnClicked.AddListener(ButtonPushed);
        if (blueButton)
            blueButton.OnClicked.AddListener(ButtonPushed);
    }

    void Update()
    {

    }

    void ButtonPushed(EButton.ColorType color)
    {
        clickCount++;

        Debug.Log("YEY! " + color);

        if (phase == Phase.FIRST) {
            Piston piston = first_pistons[clickCount-1];
            if (piston) {
                piston.desiredColor = EButton.GetColor(color);
            }
        } else if (phase == Phase.SECOND)
        {
            Piston piston = second_pistons[clickCount - 1];
            if (piston)
            {
                piston.desiredColor = EButton.GetColor(color);
            }
        }

    }

    int Random01()
    {
        Random rand = new Random();

        return (Random.Range(0.0f, 1f) >= 0.5f) ? 1 : 0;
    }
}
