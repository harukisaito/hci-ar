using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Texts")]
    public TextMeshProUGUI inputText;
    public TextMeshProUGUI debugText;


    [Header("Buttons")]
    public Button createButton;
    public Button formButton;
    public Button colorButton;
    public Button eraseButton;
    public Button otherButton;


    [Header("Pop Ups")]
    public GameObject colorPopUp;


    [Header("Color")] 
    public Image colorCircle;



    private bool showColorPopUp;



    private void Awake() {
        if(Instance == null) {
            Instance = this;
        }
        else {
            Destroy(this);
        }
    }

    private void Start() {
        ShowColorPopUp(false);
        AddButtonListeners();
    }

    public void SetInputText(string text) {
        inputText.text = text;
    }

    public void SetDebugText(string[] text) {
        debugText.text = "";
        foreach(var line in text) {
            debugText.text += line + "\n";
        }
    }

    // ref for color button
    public void ShowOrHideColorPopUp() 
    {
        showColorPopUp = !showColorPopUp;
        ShowColorPopUp(showColorPopUp);
    }

    public void ShowColorPopUp(bool active) 
    {
        colorPopUp.SetActive(active);
        showColorPopUp = active;
    }

    // ref on color picker (event listener)
    public void ChangeColorCircle(Color color) {
        colorCircle.color = color;
    }

    private void AddButtonListeners() 
    {
        createButton.onClick.AddListener(ChangeToCreateTool);
        formButton.onClick.AddListener(ChangeToFormTool);
        colorButton.onClick.AddListener(ChangeToColorTool);
        eraseButton.onClick.AddListener(ChangeToEraseTool);
        otherButton.onClick.AddListener(ChangeToOtherTool);
    }

    private void ChangeToCreateTool() 
    {
        ToolManager.Instance.ChangeTool(ToolManager.Tools.Create);
    }
    private void ChangeToFormTool() 
    {
        ToolManager.Instance.ChangeTool(ToolManager.Tools.Form);
    }
    private void ChangeToColorTool() 
    {
        ToolManager.Instance.ChangeTool(ToolManager.Tools.Color);
    }
    private void ChangeToEraseTool() 
    {
        ToolManager.Instance.ChangeTool(ToolManager.Tools.Erase);
    }
    private void ChangeToOtherTool() 
    {
        ToolManager.Instance.ChangeTool(ToolManager.Tools.Other);
    }
}
