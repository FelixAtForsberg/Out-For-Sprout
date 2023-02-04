using System;
using System.Collections;
using UnityEngine;

public class CameraVictoryAnimation : MonoBehaviour
{
    public float animationTime;
    public AnimationCurve animationCurve;

    private void Start()
    {
        GameManager.Instance.OnPlayerDeath.AddListener(PlayAnimation);
    }

    public void PlayAnimation()
    {
        StartCoroutine(VictoryRoutine());
    }

    IEnumerator VictoryRoutine()
    {
        var timePassed = 0f;

        while (true)
        {
            yield return null;
            timePassed += Time.unscaledDeltaTime;

            var alpha = Mathf.Clamp01(timePassed / animationTime);
            var lerpTime = animationCurve.Evaluate(alpha);
            transform.rotation = Quaternion.Slerp(Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 180), lerpTime);
        }
    }
}
