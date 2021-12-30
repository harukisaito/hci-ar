using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;

public class TapDetector : MonoBehaviour
{
    public UnityAction<TapData> OnTap;

    // [SerializeField] private ARRaycastManager raycastManager;

    // private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    // private bool spawnedObject;

    public void TapUpdate()
    {
        if(Input.touchCount == 0) {
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
