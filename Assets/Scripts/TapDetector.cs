using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;

public class TapDetector : MonoBehaviour
{
    public UnityAction<TapData> OnTap;

    [SerializeField] private ARRaycastManager raycastManager;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private bool spawnedObject;

    public void TapUpdate()
    {
        if(Input.touchCount == 0) {
            return;
        }

        Touch touch = Input.GetTouch(0);

        RaycastHit hit;
        Ray ray = InputManager.Instance.arCamera.ScreenPointToRay(touch.position);

        if(raycastManager.Raycast(touch.position, hits)) {
            if(touch.phase == TouchPhase.Began && !spawnedObject) {

                if(Physics.Raycast(ray, out hit)) {
                    SendTap(hits[0].pose.position, hits[0].pose.rotation);
                }

            }

            if(touch.phase == TouchPhase.Ended) {
                spawnedObject = false;
            }
        }
    }

    private void SendTap(Vector3 pos, Quaternion rot)
    {
        TapData tapData = new TapData()
        {
            Position = pos,
            Rotation = rot
        };
        OnTap.Invoke(tapData);
    }
}

public struct TapData
{
    public Vector3 Position;
    public Quaternion Rotation;
}
