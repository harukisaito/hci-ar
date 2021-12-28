using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickDetector : MonoBehaviour
{
    public UnityAction<ClickData> OnClick;

    public void ClickUpdate()
    {
        if(!Input.GetMouseButtonDown(0)) {
            return;
        }

        Vector3 mousePos = Input.mousePosition;

        RaycastHit hit;
        Ray ray = InputManager.Instance.arCamera.ScreenPointToRay(mousePos);

        if(Physics.Raycast(ray, out hit)) {
            print(hit.point);
            SendTap(hit.point, hit.transform.rotation);
        }
    }

    private void SendTap(Vector3 pos, Quaternion rot)
    {
        ClickData clickData = new ClickData()
        {
            Position = pos,
            Rotation = rot
        };
        OnClick.Invoke(clickData);
    }
}

public struct ClickData
{
    public Vector3 Position;
    public Quaternion Rotation;
}