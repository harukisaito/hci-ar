using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEffect : MonoBehaviour
{
    public AnimationCurve animationCurve;

    public void ApplyAnimationEffect(GameObject obj) {
        StartCoroutine(ChangeScale(obj));
    }

    private IEnumerator ChangeScale(GameObject obj) {
        float objScale = obj.transform.localScale.x;
        float timer = 0;
        while(timer < 0.2f) {
            obj.transform.localScale = objScale * Vector3.one * animationCurve.Evaluate(timer);

            timer += Time.deltaTime;
            yield return null;
        }

        obj.transform.localScale = objScale * Vector3.one * animationCurve.Evaluate(0.2f);
    }

    public void ApplyVibrationEffectAfterDelay(float delay) {
        Invoke("ApplyVibrationEffect", delay);
    }

    public void ApplyVibrationEffect() {
        Handheld.Vibrate();
    }
}
