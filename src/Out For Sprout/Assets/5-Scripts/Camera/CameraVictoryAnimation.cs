using System.Collections;
using UnityEngine;

public class CameraVictoryAnimation : MonoBehaviour
{
    public float animationTime;
    
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

            var alpha = timePassed / animationTime;
            //transform.rotation = Quaternion.Slerp(Quaternion.Euler(), )
        }
    }
}
