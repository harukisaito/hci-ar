using System;
using UnityEngine;
using UnityEngine.Events;

public class SwipeDetector : MonoBehaviour
{
    public UnityAction<SwipeData> OnSwipe;
    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;
    private Vector3 worldPosition;
    private Quaternion worldRotation;

    [SerializeField]
    private float minDistanceForSwipe = 20f;


    public void SwipeUpdate()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerUpPosition = touch.position;
                fingerDownPosition = touch.position;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                fingerDownPosition = touch.position;
                worldPosition = InputManager.Instance.arCamera.ScreenToWorldPoint(touch.position);
                worldRotation = InputManager.Instance.arCamera.transform.rotation;
                DetectSwipe();
            }
        }
    }

    private void DetectSwipe()
    {
        if (SwipeDistanceCheckMet())
        {
            SendSwipe();
            fingerUpPosition = fingerDownPosition;
        }
    }

    private bool SwipeDistanceCheckMet()
    {
        return Vector2.Distance(fingerDownPosition, fingerUpPosition) > minDistanceForSwipe;
    }

    private void SendSwipe()
    {
        SwipeData swipeData = new SwipeData()
        {
            WorldPosition = worldPosition,
            WorldRotation = worldRotation,
            StartPosition = fingerDownPosition,
            EndPosition = fingerUpPosition
        };
        
        OnSwipe.Invoke(swipeData);
    }
}

public struct SwipeData
{
    public Vector3 WorldPosition;
    public Quaternion WorldRotation;
    public Vector2 StartPosition;
    public Vector2 EndPosition;
}