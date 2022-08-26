using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public event EventHandler<OnFingerDownEventArgs> OnFingerDown;
    public event EventHandler<OnFingerUpEventArgs> OnFingerUp;
    public event EventHandler<OnSwipeEventArgs> OnSwipe;

    public class OnFingerDownEventArgs : EventArgs
    {
        public bool isFingerDown;
        public int touchCount;
        public Vector2 screenSpacePosition;

    }

    public class OnFingerUpEventArgs : EventArgs
    {
        public bool isFingerDown;
        public Vector2 screenSpacePosition;
    }

    public class OnSwipeEventArgs : EventArgs
    {
        public bool isSwiping;
        public Vector2 screenSpacePosition;
        public Vector2 screenSpaceSwipeDisplacement;
        public Vector2 screenSpaceSwipeDisplacementRate;
    }

    private int touchCount = 0;
    private Vector2 initalTouchPosition;
    private Vector2 finalTouchPosition;
    private Vector2 currentTouchPosition;
    private Vector2 screenSwipeDisplacement;
    private Vector2 screenSwipeDisplacementRate;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchCount++;
            initalTouchPosition = Input.mousePosition;
            OnFingerDown?.Invoke(this, new OnFingerDownEventArgs { touchCount = touchCount, isFingerDown = true, screenSpacePosition = initalTouchPosition });
        }

        if (Input.GetMouseButtonUp(0))
        {
            finalTouchPosition = Input.mousePosition;
            OnFingerUp?.Invoke(this, new OnFingerUpEventArgs { isFingerDown = false, screenSpacePosition = finalTouchPosition });
        }

        if (Input.GetMouseButton(0))
        {
            currentTouchPosition = Input.mousePosition;
            screenSwipeDisplacement = currentTouchPosition - initalTouchPosition;
            screenSwipeDisplacementRate = new Vector2(screenSwipeDisplacement.x / Screen.width, screenSwipeDisplacement.y / Screen.height);
            OnSwipe?.Invoke(this, new OnSwipeEventArgs { isSwiping = true,screenSpacePosition=currentTouchPosition, screenSpaceSwipeDisplacement = screenSwipeDisplacement, screenSpaceSwipeDisplacementRate = screenSwipeDisplacementRate });
        }
    }
}
