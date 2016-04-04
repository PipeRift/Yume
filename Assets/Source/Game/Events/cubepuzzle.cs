using UnityEngine;
using System.Collections.Generic;
using Crab.Utils;
using UnityEngine.Events;

public class cubepuzzle : MonoBehaviour
{
    public enum Phase
    {
        FIRST,
        SECOND,
        ENDED
    }

    public ColorCombination firstCombination = new ColorCombination();
    public ColorCombination secondCombination = new ColorCombination();

    [Space()]
    public EButton redButton;
    public EButton greenButton;
    public EButton blueButton;

    public EButton.ColorType success_color = EButton.ColorType.Black;

    public List<Piston> firstPistons = new List<Piston>();
    public List<Piston> secondPistons = new List<Piston>();

    public UnityEvent OnFirstPhase;
    public UnityEvent OnSecondPhase;

    [System.NonSerialized]
    public Phase phase;

    [System.NonSerialized]
    public int clickCount = 0;
    private bool first_failed = false;
    private bool second_failed = false;
    private Delay resetDelay = new Delay(1000, false);

    void Start()
    {
        if (redButton)
            redButton.OnClicked.AddListener(ButtonPushed);
        if (greenButton)
            greenButton.OnClicked.AddListener(ButtonPushed);
        if (blueButton)
            blueButton.OnClicked.AddListener(ButtonPushed);
    }

    void ButtonPushed(EButton.ColorType color)
    {
        if (clickCount >= 3)
            return;

        clickCount++;

        if (phase == Phase.FIRST)
        {
            Piston piston = firstPistons[clickCount - 1];
            if (piston)
            {
                piston.color = color;
            }


            if (firstCombination[clickCount - 1] != color)
            {
                first_failed = true;
            }

            if (clickCount >= 3)
            {
                resetDelay.Start();
                if(!first_failed)
                    OnFirstPhase.Invoke();
            }
        }
        else if (phase == Phase.SECOND)
        {
            Piston piston = secondPistons[clickCount - 1];
            if (piston)
            {
                piston.color = color;
            }


            if (secondCombination[clickCount - 1] != color)
            {
                second_failed = true;
            }

            if (clickCount >= 3)
            {
                resetDelay.Start();
                if (!second_failed)
                    OnSecondPhase.Invoke();
            }
        }
    }

    void Reset(Phase nextPhase)
    {
        EButton.ColorType resetColor = EButton.ColorType.White;

        if (phase == Phase.FIRST)
        {
            for (int i = 0; i < 3; i++)
            {
                firstPistons[i].color = first_failed ? resetColor : success_color;
            }

            if (!first_failed)
            {
                phase = nextPhase;
            }
            else {
                first_failed = false;
            }

        }
        else if (phase == Phase.SECOND)
        {
            for (int i = 0; i < 3; i++)
            {
                secondPistons[i].color = second_failed ? resetColor : success_color;
            }

            if (!second_failed)
            {
                phase = nextPhase;
            }
            else {
                second_failed = false;
            }
        }
        clickCount = 0;
    }

    void Update()
    {
        if (resetDelay.Over())
        {
            if (phase == Phase.FIRST)
                Reset(Phase.SECOND);
            else if (phase == Phase.SECOND)
                Reset(Phase.ENDED);
            resetDelay.Reset();
        }
    }

    int Random01()
    {
        Random rand = new Random();
        return (Random.Range(0.0f, 1f) >= 0.5f) ? 1 : 0;
    }
}
