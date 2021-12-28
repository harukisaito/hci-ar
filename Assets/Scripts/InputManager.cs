using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    public Camera arCamera;
    public SwipeDetector swipeDetector;
    public TapDetector tapDetector;
    public ClickDetector clickDetector;

    public Inputs inputType;

    private void Awake() {
        if(Instance == null) {
            Instance = this;
        }
        else {
            Destroy(this);
        }
    }

    private void Start() {
        inputType = Inputs.Click;
        SetInputUIText();
    }

    private void Update()
    {
        switch(inputType) {
            case Inputs.Tap:
                tapDetector.TapUpdate();
                break;
            case Inputs.Swipe: 
                swipeDetector.SwipeUpdate();
                break;
            case Inputs.Click: 
                clickDetector.ClickUpdate();
                break;
        }
    }

    // referenced in button
    public void ChangeInputType() {
        if(inputType == Inputs.Click) {
            inputType = (Inputs)0;
        }
        else {
            int inputIndex = (int)inputType;
            inputIndex++;
            inputType = (Inputs)inputIndex;
        }

        SetInputUIText();
    }

    private void SetInputUIText() {
        UIManager.Instance.SetInputText(inputType.ToString());
    }
}

public enum Inputs {
    Tap,
    Swipe,
    Click
}