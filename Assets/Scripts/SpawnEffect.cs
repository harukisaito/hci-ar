using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEffect : MonoBehaviour
{
    public AnimationCurve scaleCurve;

    public void ApplyAnimationEffect(GameObject obj) {
        StartCoroutine(ChangeScale(obj));
    }

    private IEnumerator ChangeScale(GameObject obj) {
        float objScale = obj.transform.localScale.x;
        float timer = 0;
        while(timer < 1) {
            obj.transform.localScale = objScale * Vector3.one * scaleCurve.Evaluate(timer);

            timer += Time.deltaTime;
            yield return null;
        }
    }

    public void ApplyVibrationEffectAfterDelay(float delay) {
        Invoke("ApplyVibrationEffect", delay);
    }

    public void ApplyVibrationEffect() {
        Handheld.Vibrate();
    }
}
