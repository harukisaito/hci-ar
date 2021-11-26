using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;

public class SpawnManager : MonoBehaviour
{   
    private void Start() {
        InputManager.Instance.tapDetector.OnTap += SpawnPrefab;
        InputManager.Instance.swipeDetector.OnSwipe += SpawnAndThrowPrefab;
    }

    private void SpawnPrefab(TapData data) {
        SpawnableObjectManager.Instance.SpawnObject(data.Position, data.Rotation);
    }

    private void SpawnAndThrowPrefab(SwipeData data) {
        GameObject obj = SpawnableObjectManager.Instance.SpawnObject(data.WorldPosition, Quaternion.identity);
        
    }
}
