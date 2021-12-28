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

        InputManager.Instance.tapDetector.OnTap += SpawnPrefab;
        InputManager.Instance.swipeDetector.OnSwipe += SpawnAndThrowPrefab;
        InputManager.Instance.clickDetector.OnClick += SpawnPrefab;
    }

    // referenced by slider
    public void ChangeThrowForce(float value) {
        print (value);
        throwForce = value;
    }

    private void SpawnPrefab(TapData data) {
        GameObject obj = SpawnableObjectManager.Instance.SpawnObject(data.Position, data.Rotation);
        spawnEffect.ApplyAnimationEffect(obj);
        spawnEffect.ApplyVibrationEffectAfterDelay(0.25f);
    }

    private void SpawnPrefab(ClickData data) {
        GameObject obj = SpawnableObjectManager.Instance.SpawnObject(data.Position, data.Rotation);
        obj.transform.localScale = Vector3.one;
        spawnEffect.ApplyAnimationEffect(obj);
        spawnEffect.ApplyVibrationEffectAfterDelay(0.25f);
    }

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
}
