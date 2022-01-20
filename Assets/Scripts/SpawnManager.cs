using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;

public class SpawnManager : MonoBehaviour
{   
    public SpawnEffect spawnEffect;
    private float throwForce;

    private void Start() {
        throwForce = 1f;
    }

    // referenced by slider
    public void ChangeThrowForce(float value) {
        print (value);
        throwForce = value;
    }

    // Tap
    private void SpawnPrefab(TapData data) {
        Vector3 spawnPos;
        if(data.IsCube) 
        {
            spawnPos = data.ClickedObjPosition + data.Normal * 0.1f; // because of scaling
        }
        else 
        {
            spawnPos = data.Position;
        }

        GameObject obj = SpawnableObjectManager.Instance.SpawnObject(spawnPos, data.Rotation);
        spawnEffect.ApplyAnimationEffect(obj);
    }



    // Click 
    private void SpawnPrefab(ClickData data) {
        Vector3 spawnPos;
        if(data.IsCube) 
        {
            spawnPos = data.ClickedObjPosition + data.Normal;
        }
        else
        {
            spawnPos = data.Position;
        }

        GameObject obj = SpawnableObjectManager.Instance.SpawnObject(spawnPos, data.Rotation);
        obj.transform.localScale = Vector3.one;
        spawnEffect.ApplyAnimationEffect(obj);
    }



    // Swipe
    private void SpawnAndThrowPrefab(SwipeData data) {
        GameObject obj = SpawnableObjectManager.Instance.SpawnObject(data.WorldPosition, Quaternion.identity);
        Rigidbody rigidbody = obj.AddComponent<Rigidbody>();
        obj.AddComponent<BoxCollider>();

        Quaternion yRotation = Quaternion.Euler(0, data.WorldRotation.eulerAngles.y, 0);
        Vector2 direction = data.StartPosition - data.EndPosition;
        Vector2 normalizedDirection = direction.normalized;
        Vector3 throwDirection = new Vector3(
            normalizedDirection.x, 
            normalizedDirection.y, 
            1
        );
        throwDirection = yRotation * throwDirection;
        rigidbody.velocity = throwDirection * throwForce;
    }

    private void EraseObject(TapData data) 
    {
        if(!data.IsCube) 
        {
            return;
        }

        SpawnableObjectManager.Instance.DestroyObject(data.ClickedObj.transform.parent.gameObject);
        // data.clickedobj is only the cube not the entire prefab
    }

    private void EraseObject(ClickData data) 
    {
        if(!data.IsCube) 
        {
            return;
        }

        SpawnableObjectManager.Instance.DestroyObject(data.ClickedObj.transform.parent.gameObject);
        // data.clickedobj is only the cube not the entire prefab
    }

    public void AddEraseListeners() 
    {
        InputManager.Instance.tapDetector.OnTap += EraseObject;
        InputManager.Instance.clickDetector.OnClick += EraseObject;
    }

    public void RemoveEraseListeners() 
    {
        InputManager.Instance.tapDetector.OnTap -= EraseObject;
        InputManager.Instance.clickDetector.OnClick -= EraseObject;
    }

    public void AddCreateListeners() 
    {
        InputManager.Instance.tapDetector.OnTap += SpawnPrefab;
        InputManager.Instance.swipeDetector.OnSwipe += SpawnAndThrowPrefab;
        InputManager.Instance.clickDetector.OnClick += SpawnPrefab;
    }

    public void RemoveCreateListeners() 
    {
        InputManager.Instance.tapDetector.OnTap -= SpawnPrefab;
        InputManager.Instance.swipeDetector.OnSwipe -= SpawnAndThrowPrefab;
        InputManager.Instance.clickDetector.OnClick -= SpawnPrefab;
    }

}
