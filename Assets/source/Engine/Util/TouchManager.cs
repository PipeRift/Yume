using UnityEngine;

public enum SwipeType
{
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

    private float fingerStartTime = 0.0f;
    private Vector2 fingerStartPos = Vector2.zero;

    private bool isSwipe = false;
    private float minSwipeDist = 50.0f;
    private float maxTouchDist = 10.0f;
    private float maxSwipeTime = 2f;

    public void Update()
    {

        if (Input.touchCount <= 0)
            return;

        foreach (Touch touch in Input.touches)
        {
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    /* this is a new touch */
                    isSwipe = true;
                    fingerStartTime = Time.time;
                    fingerStartPos = touch.position;
                    break;

                case TouchPhase.Canceled:
                    /* The touch is being canceled */
                    isSwipe = false;
                    break;

                case TouchPhase.Ended:

                    float gestureTime = Time.time - fingerStartTime;
                    float gestureDist = (touch.position - fingerStartPos).magnitude;

                    if (!isSwipe)
                        return;

                    Debug.Log(gestureDist);

                    if (gestureTime < maxSwipeTime && gestureDist > minSwipeDist)
                    {
                        Vector2 direction = touch.position - fingerStartPos;
                        Vector2 swipeType = Vector2.zero;

                        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                        {
                            // the swipe is horizontal:
                            swipeType = Vector2.right * Mathf.Sign(direction.x);
                        }
                        else {
                            // the swipe is vertical:
                            swipeType = Vector2.up * Mathf.Sign(direction.y);
                        }

                        if (swipeType.x != 0.0f)
                        {
                            if (swipeType.x > 0.0f)
                            {
                                // MOVE RIGHT
                                OnSwipe(SwipeType.RIGHT);
                            }
                            else {
                                // MOVE LEFT
                                OnSwipe(SwipeType.LEFT);
                            }
                        }

                        if (swipeType.y != 0.0f)
                        {
                            if (swipeType.y > 0.0f)
                            {
                                // MOVE UP
                                OnSwipe(SwipeType.UP);
                            }
                            else {
                                // MOVE DOWN
                                OnSwipe(SwipeType.DOWN);
                            }
                        }
                        return;
                    }

                    if(gestureDist < maxTouchDist)
                    {
                        OnTouch(touch.position);
                    }
                    break;
            }
        }
    }
}
