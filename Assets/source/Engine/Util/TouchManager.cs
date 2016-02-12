using UnityEngine;

public enum SwipeType {
    LEFT,
    RIGHT,
    DOWN,
    UP
}

public class TouchManager
{
    public delegate void SwipeEvent(SwipeType type);
    public delegate void TouchEvent(Vector3 touchPosition);
    public SwipeEvent OnSwipe;
    public TouchEvent OnTouch;

    private Touch initialTouch = new Touch();
    private float distance = 0;
    private bool hasSwiped = false;


    public void Update()
    {
        for(int i = 0, len = Input.touches.Length; i < len; i++)
        {
            Touch t = Input.touches[i];

            if (t.phase == TouchPhase.Began)
            {
                initialTouch = t;
            }
            else if (t.phase == TouchPhase.Moved && !hasSwiped)
            {
                float deltaX = initialTouch.position.x - t.position.x;
                float deltaY = initialTouch.position.y - t.position.y;
                distance = Mathf.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
                bool swipedSideways = Mathf.Abs(deltaX) > Mathf.Abs(deltaY);

                if (distance > 100f)
                {
                    if (swipedSideways && deltaX > 0) //swiped left
                    {
                        if (OnSwipe != null)
                            OnSwipe(SwipeType.LEFT);
                    }
                    else if (swipedSideways && deltaX <= 0) //swiped right
                    {
                        if (OnSwipe != null)
                            OnSwipe(SwipeType.RIGHT);
                    }
                    else if (!swipedSideways && deltaY > 0) //swiped down
                    {
                        if (OnSwipe != null)
                            OnSwipe(SwipeType.DOWN);
                    }
                    else if (!swipedSideways && deltaY <= 0)  //swiped up
                    {
                        if (OnSwipe != null)
                            OnSwipe(SwipeType.UP);
                    }

                    hasSwiped = true;
                }
            }
            else if (t.phase == TouchPhase.Ended)
            {
                if (OnTouch != null)
                    OnTouch(t.position);
                initialTouch = new Touch();
                hasSwiped = false;
            }
        }
    }
    
}
