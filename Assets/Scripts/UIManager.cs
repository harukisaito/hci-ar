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

    [Header("Button Sprites")]
    public Sprite selectedCreateSprite;
    public Sprite selectedFormSprite;
    public Sprite selectedEraseSprite;
    public Sprite unselectedCreateSprite;
    public Sprite unselectedFormSprite;
    public Sprite unselectedEraseSprite;


    [Header("Pop Ups")]
    public GameObject colorPopUp;
    public GameObject otherPopUp;


    [Header("Color")] 
    public Image colorCircle;


    private Image createImage;
    private Image formImage;
    private Image eraseImage;



    private bool showColorPopUp;
    private bool showOtherPopUp;



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
        ShowOtherPopUp(false);
        AddButtonListeners();
        GetImageComponentsOfButtons();
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

    public void ShowOrHideOtherPopUp() 
    {
        showOtherPopUp = !showOtherPopUp;
        ShowOtherPopUp(showOtherPopUp);
    }

    public void ShowOtherPopUp(bool active) 
    {
        otherPopUp.SetActive(active);
        showOtherPopUp = active;
    }

    // ref on color picker (event listener)
    public void ChangeColorCircle(Color color) {
        colorCircle.color = color;
    }

    public void ChangeCreateButtonToSelectedMode(bool selected) 
    {
        if(selected) 
        {
            createImage.sprite = selectedCreateSprite;
        }
        else 
        {
            createImage.sprite = unselectedCreateSprite;
        }
    }

    public void ChangeFormButtonToSelectedMode(bool selected) 
    {
        if(selected) 
        {
            formImage.sprite = selectedFormSprite;
        }
        else 
        {
            formImage.sprite = unselectedFormSprite;
        }
    }

    public void ChangeEraseButtonToSelectedMode(bool selected) 
    {
        if(selected) 
        {
            eraseImage.sprite = selectedEraseSprite;
        }
        else 
        {
            eraseImage.sprite = unselectedEraseSprite;
        }
    }

    private void GetImageComponentsOfButtons() 
    {
        createImage = createButton.gameObject.GetComponent<Image>();
        formImage = formButton.gameObject.GetComponent<Image>();
        eraseImage = eraseButton.gameObject.GetComponent<Image>();
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
