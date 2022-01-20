using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public static ColorManager Instance;

    public Color currentColor;

    private void Awake() 
    {
        if(Instance == null) 
        {
            Instance = this;
        }
        else 
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        ChangeCurrentColor(new Color(0.6933962f, 1f, 0.937255f, 1f));
    }

    // ref on color picker (event listener)
    public void ChangeCurrentColor(Color color) 
    {
        currentColor = color;
    }

    private void ChangeColor(TapData data) 
    {
        if(!data.IsCube) 
        {
            return;
        }

        MeshRenderer mesh = data.ClickedObj.GetComponent<MeshRenderer>();
        mesh.material.color = currentColor;
    }

    private void ChangeColor(ClickData data) 
    {
        if(!data.IsCube) 
        {
            return;
        }

        MeshRenderer mesh = data.ClickedObj.GetComponent<MeshRenderer>();
        mesh.material.color = currentColor;
    }

    public void AddListeners() 
    {
        InputManager.Instance.tapDetector.OnTap += ChangeColor;
        InputManager.Instance.clickDetector.OnClick += ChangeColor;
    }

    public void RemoveListeners()
    {
        InputManager.Instance.tapDetector.OnTap -= ChangeColor;
        InputManager.Instance.clickDetector.OnClick -= ChangeColor;
    }
}