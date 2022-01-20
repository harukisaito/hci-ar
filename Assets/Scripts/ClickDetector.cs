using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClickDetector : MonoBehaviour
{
    public UnityAction<ClickData> OnClick;

    public void ClickUpdate()
    {
        if(!Input.GetMouseButtonDown(0)) 
        {
            return;
        }

        if (IsPointerOverUIObject())
        {
            return;
        }

        Vector3 mousePos = Input.mousePosition;

        RaycastHit hit;
        Ray ray = InputManager.Instance.arCamera.ScreenPointToRay(mousePos);

        if(Physics.Raycast(ray, out hit)) 
        {
            SendClick
            (
                hit.point,
                hit.transform.rotation, 
                hit.normal, 
                hit.collider.gameObject
            );
        }
    }

    private bool IsPointerOverUIObject()
    {
        // get current pointer position and raycast it
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        // check if the target is in the UI
        foreach (RaycastResult r in results) {
            bool isUIClick = r.gameObject.transform.IsChildOf(UIManager.Instance.createButton.transform); 
            if (isUIClick) {
                return true;
            }
        }
        return false;
    }

    private void SendClick(Vector3 pos, Quaternion rot, Vector3 normal, GameObject clickedObj)
    {
        ClickData clickData = new ClickData()
        {
            Position = pos,
            Rotation = rot,
            Normal = normal,
            ClickedObj = clickedObj,
            ClickedObjPosition = clickedObj.transform.position,
            IsCube = clickedObj.tag == "Cube"
        };

        if(OnClick == null) 
        {
            return;
        }

        OnClick.Invoke(clickData);
    }
}

public struct ClickData
{
    public Vector3 Position;
    public Quaternion Rotation;
    public Vector3 Normal;
    public GameObject ClickedObj;
    public Vector3 ClickedObjPosition;
    public bool IsCube;
}