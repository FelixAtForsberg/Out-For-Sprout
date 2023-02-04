using System;
using System.Collections;
using UnityEngine;

public class CameraVictoryAnimation : MonoBehaviour
{
    public float animationTime;
    public AnimationCurve animationCurve;
    [SerializeField] private Transform victoryPositionReference;

    private void Start()
    {
        GameManager.Instance.OnPlayerWin.AddListener(PlayAnimation);
    }

    public void PlayAnimation()
    {
        StartCoroutine(RotateRoutine());
        StartCoroutine(PanToEndPoint());
    }

    private IEnumerator RotateRoutine()
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
    
    private IEnumerator PanToEndPoint()
    {
        while (true)
        {
            transform.position = Vector3.Lerp(transform.position, victoryPositionReference.transform.position, 0.9f * Time.unscaledDeltaTime);
        }
    }
}
