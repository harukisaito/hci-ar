using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;

public class SpawnManager : MonoBehaviour
{   
    [SerializeField] private Camera arCamera;
    [SerializeField] private ARRaycastManager raycastManager;
    // [SerializeField] private GameObject spawnablePrefab;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private bool spawnedObject;

    private void Start() {
        spawnedObject = false;
    }

    private void Update() {
        if(Input.touchCount == 0) {
            return;
        }

        Touch touch = Input.GetTouch(0);

        RaycastHit hit;
        Ray ray = arCamera.ScreenPointToRay(touch.position);

        if(raycastManager.Raycast(touch.position, hits)) {
            if(touch.phase == TouchPhase.Began && !spawnedObject) {

                if(Physics.Raycast(ray, out hit)) {
                    SpawnPrefab(hits[0].pose.position, hits[0].pose.rotation);
                }

            }

            if(touch.phase == TouchPhase.Ended) {
                spawnedObject = false;
            }
        }
    }

    private void SpawnPrefab(Vector3 position, Quaternion rotation) {
        SpawnableObjectManager.Instance.SpawnObject(position, rotation);
    }
}
