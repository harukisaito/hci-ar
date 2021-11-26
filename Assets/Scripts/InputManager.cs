using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    public Camera arCamera;
    public SwipeDetector swipeDetector;
    public TapDetector tapDetector;

    public Inputs inputType;

    private void Awake() {
        if(Instance == null) {
            Instance = this;
        }
        else {
            Destroy(this);
        }
    }

    void Update()
    {
        switch(inputType) {
            case Inputs.Tap:
                tapDetector.TapUpdate();
                break;
            case Inputs.Swipe: 
                swipeDetector.SwipeUpdate();
                break;
        }
    }

    // referenced in button
    public void ChangeInputType() {
        if(inputType == Inputs.Swipe) {
            inputType = (Inputs)0;
        }
        else {
            int inputIndex = (int)inputType;
            inputIndex++;
            inputType = (Inputs)inputIndex;
        }
    }
}

public enum Inputs {
    Tap,
    Swipe
}