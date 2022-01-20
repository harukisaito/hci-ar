using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;

public class TapDetector : MonoBehaviour
{
    public UnityAction<TapData> OnTap;

    public void TapUpdate()
    {

        if(Input.touchCount == 0) {
            return;
        }
        
        if (IsPointerOverUIObject())
        {
            return;
        }

        Touch touch = Input.GetTouch(0);

        RaycastHit hit;
        Ray ray = InputManager.Instance.arCamera.ScreenPointToRay(touch.position);

        if(touch.phase == TouchPhase.Began) {
            if(Physics.Raycast(ray, out hit)) {
                SendTap
                (
                    hit.point,
                    hit.transform.rotation, 
                    hit.normal, 
                    hit.collider.gameObject
                );
            }
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

    private void SendTap(Vector3 pos, Quaternion rot, Vector3 normal, GameObject clickedObj)
    {
        TapData tapData = new TapData()
        {
            Position = pos,
            Rotation = rot,
            Normal = normal,
            ClickedObj = clickedObj,
            ClickedObjPosition = clickedObj.transform.position,
            IsCube = clickedObj.tag == "Cube"
        };
        OnTap.Invoke(tapData);
    }
}

public struct TapData
{
    public Vector3 Position;
    public Quaternion Rotation;
    public Vector3 Normal;
    public GameObject ClickedObj;
    public Vector3 ClickedObjPosition;
    public bool IsCube;
}
